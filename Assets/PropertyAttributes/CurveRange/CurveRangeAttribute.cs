/*
 * About:
 * Custom UnityEditor Property Drawer for [CurveRange] CustomAttribute
 * Limits the Unity curve Selector to a the specified range
 * Sets custom curve color
 * 
 * How To Use:
 * Use [CurveRange] on the int or float variables
 * [CurveRange]
 * [CurveRange(ColorEnum.Aqua)]
 * [CurveRange(-1, -1, 1, 1)]
 * [CurveRange(-1, -1, 1, 1, ColorEnum.Aqua)]
 * 
 */

using UnityEngine;
using static ColorUtilities;

#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(CurveRangeAttribute))]
public class CurveRangePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var curveRangeAttribute = attribute as CurveRangeAttribute;
        var targetObject = property.serializedObject.targetObject;

        if (property.propertyType != SerializedPropertyType.AnimationCurve)
        {
            Debug.LogError("Supported Types Allowed: AnimationCurve", targetObject);

            EditorGUIUtilities.DrawErrorField(position, property, label);

            return;
        }

        var curveRanges = new Rect(
            curveRangeAttribute.Min.x,
            curveRangeAttribute.Min.y,
            curveRangeAttribute.Max.x - curveRangeAttribute.Min.x,
            curveRangeAttribute.Max.y - curveRangeAttribute.Min.y);

        EditorGUI.CurveField(position, property, GetColor(curveRangeAttribute.Color),
            curveRanges,
            label);
    }
}
#endif

public class CurveRangeAttribute : PropertyAttribute
{
    public readonly Vector2 Min;
    public readonly Vector2 Max;
    public readonly ColorEnum Color;

    public CurveRangeAttribute(float minX, float minY, float maxX, float maxY, ColorEnum color = ColorEnum.White)
    {
        Min = new Vector2(minX, minY);
        Max = new Vector2(maxX, maxY);
        Color = color;
    }

    public CurveRangeAttribute(ColorEnum color = ColorEnum.White)
    {
        Min = Vector2.zero;
        Max = Vector2.one;
        Color = color;
    }
}