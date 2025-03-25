using System;
using System.Linq;
using UnityEngine;
using System.Collections;
using System.Reflection;

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
        EditorGUILayout.BeginHorizontal();
        {
            // 1. Draw Foldout
            var foldoutRect = EditorGUILayout.GetControlRect(GUILayout.Width(10.0f));
            if (state.Parameters.Length > 0) state.Opened = EditorGUI.Foldout(foldoutRect, state.Opened, "");

            // 2. Draw Button
            var buttonText = methodInfo.GetMethodDisplayName(true);
            if (GUILayout.Button(buttonText, GUILayout.ExpandWidth(true)))
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
        EditorGUILayout.EndHorizontal();

        // 3. Draw Extras when Foldout Open
        if (state.Opened)
        {
            EditorGUI.indentLevel++;
            var parameters = methodInfo.GetParameters();
            for (int i = 0; i < parameters.Length; i++)
            {
                var parameterInfo = parameters[i];
                object currentVal = state.Parameters[i];
                state.Parameters[i] = EditorGUIUtilities.UnityParameterDrawer.DrawParameter(parameterInfo, currentVal);
            }
            EditorGUI.indentLevel--;
        }
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