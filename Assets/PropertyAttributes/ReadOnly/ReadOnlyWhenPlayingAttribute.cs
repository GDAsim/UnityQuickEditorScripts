/*
 * About:
 * Custom UnityEditor Property Drawer for [ReadOnlyWhenPlaying] CustomAttribute to disable the field in Inspector when Unity is in Play Mode
 * 
 * How To Use:
 * Use [ReadOnlyWhenPlaying] Attribute on variables
 * 
 */

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(ReadOnlyWhenPlayingAttribute))]
public class ReadOnlyWhenPlayingPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = !Application.isPlaying;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}
#endif

public class ReadOnlyWhenPlayingAttribute : PropertyAttribute { }