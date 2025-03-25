using System;
using System.Linq;
using UnityEngine;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using static UnityParameterDrawer;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(MonoBehaviour), true)]
[CanEditMultipleObjects]
public class ButtonEditor : Editor
{
    EditorButtonState[] editorButtonStates;

    public override void OnInspectorGUI()
    {
        // Draw Default
        base.OnInspectorGUI();

        var mono = target as MonoBehaviour;
        var members = mono.GetType()
            .GetMembers(BindingFlags.Instance | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        var membersWithButtonAttribute = members.Where(o => Attribute.IsDefined(o, typeof(ButtonAttribute))).ToArray();

        editorButtonStates ??= EditorButtonState.CreateStates(membersWithButtonAttribute);

        // Draw Buttons for each button attribute
        for (int i = 0; i < membersWithButtonAttribute.Length; i++)
        {
            var method = membersWithButtonAttribute[i] as MethodInfo;
            var buttonState = editorButtonStates[i];

            DrawButton(targets, method, buttonState);
        }
    }

    void DrawButton(object[] invokationTargets, MethodInfo methodInfo, EditorButtonState state)
    {
        bool buttonClicked;
        EditorGUILayout.BeginHorizontal();
        {
            // 1. Draw Foldout
            var foldoutRect = EditorGUILayout.GetControlRect(GUILayout.Width(10.0f));
            if (state.Parameters.Length > 0) state.Opened = EditorGUI.Foldout(foldoutRect, state.Opened, "");

            // 2. Draw Button
            buttonClicked = GUILayout.Button(MethodDisplayName(methodInfo), GUILayout.ExpandWidth(true));
        }
       
        EditorGUILayout.EndHorizontal();

        if (state.Opened)
        {
            EditorGUI.indentLevel++;
            int paramIndex = 0;
            foreach (ParameterInfo parameterInfo in methodInfo.GetParameters())
            {
                object currentVal = state.Parameters[paramIndex];
                state.Parameters[paramIndex] = DrawParameterInfo(parameterInfo, currentVal);
                paramIndex++;
            }
            EditorGUI.indentLevel--;
        }

        if (buttonClicked)
        {
            foreach (var invokationTarget in invokationTargets)
            {
                var monoTarget = invokationTarget as MonoBehaviour;
                object returnVal = methodInfo.Invoke(monoTarget, state.Parameters);

                if (returnVal is IEnumerator)
                {
                    monoTarget.StartCoroutine((IEnumerator)returnVal);
                }
                else if (returnVal != null)
                {
                    Debug.Log("Method call result -> " + returnVal);
                }
            }
        }
    }

    readonly Dictionary<Type, string> typeDisplayName = new Dictionary<Type, string> {

        {typeof(float),"float"},
        {typeof(int),"int"},
        {typeof(string),"string"},
        {typeof(bool),"bool"},
        {typeof(Color),"Color"},
        {typeof(Vector3),"Vector3"},
        {typeof(Vector2),"Vector2"},
        {typeof(Quaternion),"Quaternion"}
    };
    object GetDefaultValue(ParameterInfo parameter)
    {
        bool hasDefaultValue = !DBNull.Value.Equals(parameter.DefaultValue);

        if (hasDefaultValue)
            return parameter.DefaultValue;

        Type parameterType = parameter.ParameterType;
        if (parameterType.IsValueType)
            return Activator.CreateInstance(parameterType);

        return null;
    }

    object DrawParameterInfo(ParameterInfo parameterInfo, object currentValue)
    {
        object paramValue = null;

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(parameterInfo.Name);

        ParameterDrawer drawer = GetParameterDrawer(parameterInfo);
        if (currentValue == null) currentValue = GetDefaultValue(parameterInfo);
        paramValue = drawer.Invoke(parameterInfo, currentValue);

        EditorGUILayout.EndHorizontal();

        return paramValue;
    }

    ParameterDrawer GetParameterDrawer(ParameterInfo parameter)
    {
        Type parameterType = parameter.ParameterType;

        if (typeof(UnityEngine.Object).IsAssignableFrom(parameterType))
            return DrawUnityEngineObjectParameter;

        if (ParameterDrawers.TryGetValue(parameterType, out ParameterDrawer drawer))
        {
            return drawer;
        }

        return null;
    }

    static object DrawUnityEngineObjectParameter(ParameterInfo parameterInfo, object val)
    {
        return EditorGUILayout.ObjectField((UnityEngine.Object)val, parameterInfo.ParameterType, true);
    }



    string MethodDisplayName(MethodInfo method)
    {
        var sb = new StringBuilder();
        sb.Append(method.Name + "(");

        var methodParams = method.GetParameters();
        foreach (ParameterInfo parameter in methodParams)
        {
            sb.Append(MethodParameterDisplayName(parameter));
            sb.Append(",");
        }

        if (methodParams.Length > 0)
        {
            sb.Remove(sb.Length - 1, 1);
        }

        sb.Append(")");
        return sb.ToString();
    }

    string MethodParameterDisplayName(ParameterInfo parameterInfo)
    {
        if (!typeDisplayName.TryGetValue(parameterInfo.ParameterType, out string parameterTypeDisplayName))
        {
            parameterTypeDisplayName = parameterInfo.ParameterType.ToString();
        }

        return parameterTypeDisplayName + " " + parameterInfo.Name;
    }
}

internal class EditorButtonState
{
    public bool Opened;
    public object[] Parameters;
    public EditorButtonState(int numberOfParameters)
    {
        Parameters = new object[numberOfParameters];
    }

    public static EditorButtonState[] CreateStates(MemberInfo[] memberInfos)
    {
        var editorButtonStates = new EditorButtonState[memberInfos.Length];
        for (int i = 0; i < memberInfos.Length; i++)
        {
            var method = (MethodInfo)memberInfos[i];
            editorButtonStates[i] = new EditorButtonState(method.GetParameters().Length);
        }

        return editorButtonStates;
    }
}

#endif


[AttributeUsage(AttributeTargets.Method)]
public class ButtonAttribute : PropertyAttribute { }