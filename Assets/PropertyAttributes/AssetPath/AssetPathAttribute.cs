/*
 * About:
 * Custom UnityEditor Property Drawer for [AssetPath] CustomAttribute. Assign Asset in Inspector but gets translated to string path of asset.
 * Use for AssetManagement Loading
 * 
 * How To Use:
 * Use [AssetPath] on Asset Refrence Varables
 * 
 */

using UnityEngine;
using Object = UnityEngine.Object;
using System;

#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(AssetPathAttribute))]
public class AssetPathPropertyDrawer : PropertyDrawer
{
    Object objectReference;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType != SerializedPropertyType.String)
        {
            GUI.Label(position, label);

            Rect contentPosition = position;
            contentPosition.x += EditorGUIUtility.labelWidth;
            contentPosition.width -= EditorGUIUtility.labelWidth;

            EditorGUI.HelpBox(contentPosition, $"Attribute valid only for String type {fieldInfo.FieldType.Name}", MessageType.Error);
        }
        else
        {
            var assetPathAttribute = attribute as AssetPathAttribute;
            Type objectType = assetPathAttribute.Type;

            if (objectReference == null && !string.IsNullOrEmpty(property.stringValue))
            {
                objectReference = AssetDatabase.LoadAssetAtPath(property.stringValue, objectType);
            }

            EditorGUI.BeginChangeCheck();
            {
                objectReference = EditorGUI.ObjectField(position, label, objectReference, objectType, false);

                if (EditorGUI.EndChangeCheck())
                {
                    if (objectReference != null)
                    {
                        property.stringValue = AssetDatabase.GetAssetPath(objectReference);
                    }
                    else
                    {
                        property.stringValue = string.Empty;
                    }
                }
            }
        }
    }
}
#endif

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public class AssetPathAttribute : PropertyAttribute
{
    public readonly Type Type;

    public AssetPathAttribute(Type type)
    {
        Type = type;
    }
}
