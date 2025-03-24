/*
 * About:
 * Custom UnityEditor Property Drawer for [EnumFlags] CustomAttribute
 * 
 * How To Use:
 * Use [EnumFlags] on enums to have unity inspector draw a custom GUI for enums
 * e.g [Highlight(HighlightColor.Green]
 * e.g [Highlight(HighlightColor.Green, "ValidateHighlight", PlayerState.Moving)]
 * 
 * References:
 * https://gist.github.com/LotteMakesStuff
 * 
 */

using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(HighlightAttribute))]
public class HighlightPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var highlightAttribute = attribute as HighlightAttribute;
        var targetObject = property.serializedObject.targetObject;

        var doHighlight = true;

        bool haveCallback = !string.IsNullOrEmpty(highlightAttribute.ValueCallback);
        if (haveCallback)
        {
            if (!targetObject.TryGetMethodFromTarget(highlightAttribute.ValueCallback, out var methodInfo))
            {
                Debug.LogError($"Validation method \"{highlightAttribute.ValueCallback}\" not found in script.", targetObject);

                EditorGUIUtilities.DrawErrorField(position, property, label);

                return;
            }
            else if (!methodInfo.ValidateMethodParameters(highlightAttribute.MethodParameters))
            {
                Debug.LogError($"The HighlightAttribute input parameters does not match Validation Method\"{highlightAttribute.ValueCallback}\" ", targetObject);

                EditorGUIUtilities.DrawErrorField(position, property, label);

                return;
            }
            else
            {
                var result = methodInfo.Invoke(targetObject, highlightAttribute.MethodParameters);
                if (bool.TryParse(result.ToString(), out var boolvalue))
                {
                    doHighlight = boolvalue;
                }
                else
                {
                    Debug.LogError("The Method Return type needs to be bool");

                    EditorGUIUtilities.DrawErrorField(position, property, label);

                    return;
                }
            }
        }

        if (doHighlight)
        {
            // Draw HighlightRect
            var padding = EditorGUIUtility.standardVerticalSpacing / 2f;
            var padding2x = padding * 2;
            Rect highlightRect = new(position.x - padding, position.y - padding,
                position.width + padding2x, position.height + padding2x);
            EditorGUI.DrawRect(highlightRect, ColorUtilities.GetColor(highlightAttribute.Color));

            // Draw Default
            EditorGUI.PropertyField(position, property, label);
        }
        else
        {
            // Draw Default
            EditorGUI.PropertyField(position, property, label);
        }
    }
}
#endif

public class HighlightAttribute : PropertyAttribute
{
    public readonly ColorUtilities.ColorEnum Color;
    public readonly string ValueCallback;
    public readonly object[] MethodParameters;

    public readonly Action ValidateMethod2;

    public HighlightAttribute(ColorUtilities.ColorEnum color = ColorUtilities.ColorEnum.Yellow, string validateMethod = null, params object[] args)
    {
        Color = color;
        ValueCallback = validateMethod;
        MethodParameters = args;
    }
}