/*
 * About:
 * Provides a Custom UnityEditor Property Drawer for [SortingLayer] on string or int var to have a dropdown select the avialable unity layers
 * 
 * How To Use:
 * Use [SortingLayer] on string or int var
 * 
 */

using System;
using UnityEngine;
using System.Reflection;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditorInternal;

[CustomPropertyDrawer(typeof(SortingLayerAttribute))]
public class SortingLayerPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var targetObject = property.serializedObject.targetObject;

        PropertyInfo sortingLayersProperty = typeof(InternalEditorUtility).GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
        var layers = (string[])sortingLayersProperty.GetValue(null, new object[0]);

        if (property.propertyType == SerializedPropertyType.String)
        {
            int index = Array.IndexOf(layers, property.stringValue);
            if (index < 0) index = 0;
            int newIndex = EditorGUI.Popup(position, label.text, index, layers);

            property.stringValue = layers[newIndex];
        }
        else if (property.propertyType == SerializedPropertyType.Integer)
        {
            int index = property.intValue;
            int newIndex = EditorGUI.Popup(position, label.text, index, layers);

            property.intValue = newIndex;
        }
        else
        {
            Debug.LogError($"Tag attribute property is not of type \"String\" or \"Integer\"", targetObject);

            EditorGUIUtilities.DrawErrorField(position, property, label);

            return;
        }
    }
}
#endif

public class SortingLayerAttribute : PropertyAttribute { }