/*
 * About:
 * Provides a Custom UnityEditor Property Drawer for [Range] and [MinMaxSlider] CustomAttribute for "Range" Datatype
 * 
 * How To Use:
 * Declaring a Range datatype will have unity inspector draw a custom GUI
 * Use [MinMaxSlider(0f, 10f)] on the Range datatype will set the Limit for Min and Max Value
 * 
 */

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(Range))]
[CustomPropertyDrawer(typeof(MinMaxSliderAttribute))]
public class RangePropertyDrawer : PropertyDrawer
{
    const float SliderHeight = 16f;
    const float RangeGap = 16f;

    bool drawDefault;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var type = property.GetFieldType();
        if (type != typeof(Range))
        {
            Debug.LogWarning($"This MinMaxSlider Attribute only not support this {nameof(Range)} data type", property.serializedObject.targetObject);

            drawDefault = true;
            EditorGUIUtilities.DrawErrorField(position, property, label);

            return;
        }

        if (property.serializedObject.isEditingMultipleObjects) return;

        var rangeAttribute = attribute as MinMaxSliderAttribute;
        var minProperty = property.FindPropertyRelative("Min");
        var maxProperty = property.FindPropertyRelative("Max");
        if (rangeAttribute == null)
        {
            rangeAttribute = new MinMaxSliderAttribute(minProperty.floatValue - 10, maxProperty.floatValue + 10);
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