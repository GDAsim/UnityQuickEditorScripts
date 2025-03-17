/*
 * About:
 * Custom UnityEditor Property Drawer for [ReadOnly] CustomAttribute to disable the field in Inspector
 * 
 * How To Use:
 * Use [ReadOnly] Attribute on variables
 * 
 * References:
 * https://gist.github.com/LotteMakesStuff
 * 
 */

using UnityEngine;
using static StatsBarAttribute;

#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(StatsBarAttribute))]
public class StatsBarDrawer : PropertyDrawer
{
    readonly float Height = EditorGUIUtility.singleLineHeight;
    readonly float Padding = EditorGUIUtility.standardVerticalSpacing;

    bool drawDefault = false;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var statsBarAttribute = attribute as StatsBarAttribute;
        var valueMaxProperty = property.serializedObject.FindProperty(statsBarAttribute.ValueMax);

        float fillPercentage;
        string barLabel;
        switch (property.propertyType)
        {
            case SerializedPropertyType.Integer:
                if (valueMaxProperty == null)
                {
                    fillPercentage = 1;
                    barLabel = $"[{property.name}] {property.intValue}";
                }
                else if (valueMaxProperty.propertyType != SerializedPropertyType.Integer)
                {
                    Debug.LogError($"Mismatch datatype type for {statsBarAttribute.ValueMax} Expecting Integer but got {valueMaxProperty.propertyType}", property.serializedObject.targetObject);
                    return;
                }
                else
                {
                    fillPercentage = (float)property.intValue / valueMaxProperty.intValue;
                    barLabel = $"[{property.name}] {property.intValue}/{valueMaxProperty.intValue}";
                }
                break;
            case SerializedPropertyType.Float:
                if (valueMaxProperty == null)
                {
                    fillPercentage = 1;
                    barLabel = $"[{property.name}] {property.floatValue}";
                }
                else if (valueMaxProperty.propertyType != SerializedPropertyType.Float)
                {
                    Debug.LogError($"Mismatch datatype type for {statsBarAttribute.ValueMax} Expecting Float but got {valueMaxProperty.propertyType}", property.serializedObject.targetObject);
                    return;
                }
                else
                {
                    fillPercentage = property.floatValue / valueMaxProperty.floatValue;
                    barLabel = $"[{property.name}] {property.floatValue}/{valueMaxProperty.floatValue}";
                }
                break;
            default:
                // Draw Default
                drawDefault = true;
                EditorGUI.PropertyField(position, property, label);
                return;
        }

        var barRect = new Rect(position.position.x, position.position.y, position.size.x, Height);
        var barColor = GetColor(statsBarAttribute.Color);
        DrawStatsBar(barRect, fillPercentage, barLabel, barColor);

        var propertyRect = new Rect(position.position.x, position.position.y + Height + Padding, position.size.x, Height);
        EditorGUI.PropertyField(propertyRect, property);
    }

    void DrawStatsBar(Rect position, float fillPercent, string label, Color barColor)
    {
        if (Event.current.type != EventType.Repaint) return;

        // Draw BG
        EditorGUI.DrawRect(position, new Color(0.1f, 0.1f, 0.1f));

        // Draw Bar
        var fillRect = new Rect(position.x, position.y, position.width * Mathf.Clamp01(fillPercent), position.height);
        EditorGUI.DrawRect(fillRect, barColor);

        // Draw Label
        var labelRect = new Rect(position.x, position.y - 3, position.width, position.height);
        EditorGUI.DropShadowLabel(labelRect, label);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (drawDefault) return EditorGUIUtility.singleLineHeight;

        var newHeight = (EditorGUIUtility.singleLineHeight * 2) + EditorGUIUtility.standardVerticalSpacing;
        return newHeight;
    }
}

#endif

public class StatsBarAttribute : PropertyAttribute
{
    public readonly string ValueMax;
    public readonly StatsBarColor Color;

    public enum StatsBarColor
    {
        Yellow,
        Red,
        Green,
        Blue,
    }

    public StatsBarAttribute(string valueMax = null, StatsBarColor color = StatsBarColor.Red)
    {
        ValueMax = valueMax;
        Color = color;
    }

    public static Color GetColor(StatsBarColor color)
    {
        return color switch
        {
            StatsBarColor.Yellow => new Color32(255, 255, 0, 255),
            StatsBarColor.Red => new Color32(255, 0, 0, 255),
            StatsBarColor.Green => new Color32(0, 255, 0, 255),
            StatsBarColor.Blue => new Color32(0, 0, 255, 255),
            _ => new Color32(255, 255, 255, 255),
        };
    }
}

