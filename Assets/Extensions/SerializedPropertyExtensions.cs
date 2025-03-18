using System;
using System.Reflection;
using UnityEditor;

public static class SerializedPropertyExtensions
{
    /// <summary>
    /// Get system.type from Unity SerializedProperty
    /// </summary>
    public static Type GetFieldType(this SerializedProperty property)
    {
        if (property == null) return null;

        Type targetType = property.serializedObject.targetObject.GetType();
        FieldInfo field = targetType.GetField(property.propertyPath, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

        return field?.FieldType;
    }
}