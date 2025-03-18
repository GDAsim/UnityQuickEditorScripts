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

[CustomPropertyDrawer(typeof(Range))]
[CustomPropertyDrawer(typeof(MinMaxSliderAttribute))]
public class RangePropertyDrawer : PropertyDrawer
{
    const float SliderHeight = 16f;
    const float RangeGap = 16f;

    bool noMinMax = false;
    bool drawDefault;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var type = property.GetFieldType();
        if (type != typeof(Range))
        {
            Debug.LogWarning($"This MinMaxSlider Attribute only not support this {nameof(Range)} data type", property.serializedObject.targetObject);

            // Draw Default
            drawDefault = true;
            EditorGUI.PropertyField(position, property, label);

            return;
        }

        if (property.serializedObject.isEditingMultipleObjects) return;

        var minProperty = property.FindPropertyRelative("min");
        var maxProperty = property.FindPropertyRelative("max");
        var rangeAttribute = attribute as MinMaxSliderAttribute;
        if (rangeAttribute == null)
        {
            rangeAttribute = new MinMaxSliderAttribute(minProperty.floatValue - 10, maxProperty.floatValue + 10);
            noMinMax = true;
        }

        // Start Drawing Range Property
        label = EditorGUI.BeginProperty(position, label, property);
        {
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Draw Range MinMax Property as FloatFields
            var fieldRect = position;
            fieldRect.height -= SliderHeight;

            var minField = new Rect(fieldRect.x, fieldRect.y, fieldRect.width / 2 - RangeGap, fieldRect.height);
            var maxField = new Rect(fieldRect.x + fieldRect.width - minField.width, fieldRect.y, minField.width, fieldRect.height);
            var mid = new Rect(minField.xMax + RangeGap / 2, fieldRect.y, RangeGap, fieldRect.height);
            minProperty.floatValue = Mathf.Clamp(EditorGUI.FloatField(minField, minProperty.floatValue), rangeAttribute.Min, maxProperty.floatValue);
            EditorGUI.LabelField(mid, " to ");
            maxProperty.floatValue = Mathf.Clamp(EditorGUI.FloatField(maxField, maxProperty.floatValue), minProperty.floatValue, rangeAttribute.Max);

            // Draw Slider
            var sliderRect = position;
            sliderRect.y += SliderHeight + 2f;

            var min = minProperty.floatValue;
            var max = maxProperty.floatValue;
            EditorGUI.MinMaxSlider(sliderRect, GUIContent.none, ref min, ref max, rangeAttribute.Min, rangeAttribute.Max);
            minProperty.floatValue = min;
            maxProperty.floatValue = max;
        }
        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (drawDefault) return base.GetPropertyHeight(property, label);

        if (property.serializedObject.isEditingMultipleObjects) return 0f;

        return base.GetPropertyHeight(property, label) + SliderHeight + 2;
    }
}
#endif

public class MinMaxSliderAttribute : PropertyAttribute
{
    public readonly float Min;
    public readonly float Max;

    public MinMaxSliderAttribute(float min, float max)
    {
        Min = min;
        Max = max;
    }
}

[Serializable]
public struct Range
{
    [SerializeField] float min;
    [SerializeField] float max;

    public float Min
    {
        get
        {
            return min;
        }
        set
        {
            min = value;
        }
    }

    public float Max
    {
        get
        {
            return max;
        }
        set
        {
            max = value;
        }
    }

    public Range(float min, float max)
    {
        this.min = min;
        this.max = max;
    }
}