/*
 * About:
 * Provides a Custom UnityEditor Property Drawer for [Layer] on string or int var to have a dropdown select the avialable unity layers
 * 
 * How To Use:
 * Use [Layer] on string or int var
 * 
 */

using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;

[CustomPropertyDrawer(typeof(LayerAttribute))]
public class LayerPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var layers = InternalEditorUtility.layers;

        string[] displays = new string[layers.Length];
        for (int i = 0; i < displays.Length; i++)
        {
            displays[i] = $"{i}: {layers[i]}";
        }

        if(property.propertyType == SerializedPropertyType.String)
        {
            int index = Array.IndexOf(layers, property.stringValue);
            int newIndex = EditorGUI.Popup(position, label.text, index, displays);

            property.stringValue = layers[newIndex];
        }
        else if (property.propertyType == SerializedPropertyType.Integer)
        {
            int index = property.intValue;
            int newIndex = EditorGUI.Popup(position, label.text, index, displays);

            property.intValue = newIndex;
        }
    }
}
#endif

public class LayerAttribute : PropertyAttribute { }