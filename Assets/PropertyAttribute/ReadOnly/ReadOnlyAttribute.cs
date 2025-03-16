/*
 * About:
 * Custom UnityEditor Property Drawer for [ReadOnly] CustomAttribute to disable the field in Inspector
 * 
 * How To Use:
 * Use [ReadOnly] Attribute on variables
 * 
 */

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}
#endif

public class ReadOnlyAttribute : PropertyAttribute { }