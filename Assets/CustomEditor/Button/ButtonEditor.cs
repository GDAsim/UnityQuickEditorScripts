/*
 * About:
 * CustomEditor to draw buttons in inspector for methods with [Button] Attribute.
 * 
 * How To Use:
 * Add [Button] to methods
 * e.g [Button]
 * e.g [Button("Click Me")]
 * 
 * 
 * Notes:
 * This script extends the default monobehaviour and might have conflicts with other editor scripts that do the same
 * 
 * Sources: 
 * https://github.com/miguel12345/EditorButton
 * 
 */

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
            var attribute = membersWithButtonAttribute[i].GetCustomAttribute<ButtonAttribute>();
            var method = membersWithButtonAttribute[i] as MethodInfo;
            var buttonState = editorButtonStates[i];

            DrawButton(targets, method, attribute, buttonState);
        }
    }

    void DrawButton(object[] invokationTargets, MethodInfo methodInfo, ButtonAttribute buttonAttribute, EditorButtonState state)
    {
        EditorGUILayout.BeginHorizontal();
        {
            // 1. Draw Foldout
            var foldoutRect = EditorGUILayout.GetControlRect(GUILayout.Width(10.0f));
            if (state.Parameters.Length > 0) state.FoldoutOpen = EditorGUI.Foldout(foldoutRect, state.FoldoutOpen, "");

            // 2. Draw Button
            var buttonText = buttonAttribute.Text ?? methodInfo.GetMethodDisplayName(true);
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
        if (state.FoldoutOpen)
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

/// <summary>
/// internal Class to keep track of button state & extra variables required for button clicks
/// </summary>
internal class EditorButtonState
{
    public bool FoldoutOpen;
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