/*
 * About:
 * Custom UnityEditor Property Drawer for [Label] CustomAttribute
 * Replaces the variables name to a custom name in Unity inspector
 * 
 * How To Use:
 * Use [Label] on variables
 * 
 */

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(LabelAttribute))]
public class LabelPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var labelAttribute = attribute as LabelAttribute;
        var targetObject = property.serializedObject.targetObject;

        label.text = labelAttribute.Label;

        // Draw default
        EditorGUI.PropertyField(position, property, label, true);
    }
}
#endif
public class LabelAttribute : PropertyAttribute
{
    public readonly string Label;

    public LabelAttribute(string label)
    {
        Label = label;
    }
}