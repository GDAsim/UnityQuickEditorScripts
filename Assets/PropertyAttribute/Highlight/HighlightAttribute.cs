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
using System.Reflection;
using static HighlightAttribute;


#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(HighlightAttribute))]
public class HighlightPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        HighlightAttribute highlightAttribute = attribute as HighlightAttribute;

        bool doHighlight = true;

        // 1. Check for Validation method
        if (!string.IsNullOrEmpty(highlightAttribute.ValidateMethod))
        {
            // 1b. Get Class & Search for Method using String input (highlightAttribute.ValidateMethod)
            Type type = property.serializedObject.targetObject.GetType();
            MethodInfo methodInfo = type.GetMethod(highlightAttribute.ValidateMethod, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            if (methodInfo == null)
            {
                Debug.LogWarning($"Validation method \"{highlightAttribute.ValidateMethod}\" not found in script.", property.serializedObject.targetObject);
            }

            // 2. Check Parameter Lengh
            else if (methodInfo.GetParameters().Length != highlightAttribute.MethodParameters.Length)
            {
                Debug.LogWarning($"The HighlightAttribute has different number of parameters than the method \"{highlightAttribute.ValidateMethod}\" ", property.serializedObject.targetObject);
            }
            else
            {
                bool result = (bool)methodInfo.Invoke(property.serializedObject.targetObject, highlightAttribute.MethodParameters);
                doHighlight = result;
            }
        }

        if (doHighlight)
        {
            // Draw HighlightRect
            float padding = EditorGUIUtility.standardVerticalSpacing;
            float padding2x = padding * 2;
            Rect highlightRect = new(position.x - padding, position.y - padding,
                position.width + padding2x, position.height + padding2x);
            EditorGUI.DrawRect(highlightRect, GetColor(highlightAttribute.Color));

            // Draw Default
            EditorGUI.PropertyField(position, property, label);
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
}
#endif

public class HighlightAttribute : PropertyAttribute
{
    public readonly HighlightColor Color;
    public readonly string ValidateMethod;
    public readonly object[] MethodParameters;

    public readonly Action ValidateMethod2;

    public enum HighlightColor
    {
        Red,
        Pink,
        Orange,
        Yellow,
        Green,
        Blue,
        Violet,
        White
    }
     
    public HighlightAttribute(HighlightColor color = HighlightColor.Yellow, string validateMethod = null, params object[] args)
    {
        Color = color;
        ValidateMethod = validateMethod;
        MethodParameters = args;
    }

    public static Color GetColor(HighlightColor color)
    {
        switch (color)
        {
            case HighlightColor.Yellow:
                return new Color32(255, 255, 0, 255);
            case HighlightColor.Red:
                return new Color32(255, 0, 0, 255);
            case HighlightColor.Green:
                return new Color32(0, 255, 0, 255);
            case HighlightColor.Blue:
                return new Color32(0, 0, 255, 255);
            default:
                return new Color32(255, 255, 255, 255);
        }
    }
}