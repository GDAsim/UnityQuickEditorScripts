using System;
using System.Reflection;

#if UNITY_EDITOR
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

    /// <summary>
    /// Uses Reflection to search for method in target object (class)
    /// Primary used for Editor Attribute Drawers
    /// </summary>
    public static bool TryGetMethodFromTarget(this UnityEngine.Object targetObject, string methodName, out MethodInfo methodInfo)
    {
        var classType = targetObject.GetType();

        // Look for Static,Public,Private methods
        methodInfo = classType.GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic |  BindingFlags.Public);

        return methodInfo != null;
    }

    /// <summary>
    /// Uses Reflection to search for properties in target object (class)
    /// Primary used for Editor Attribute Drawers
    /// </summary>
    public static bool TryGetPropertyFromTarget(this UnityEngine.Object targetObject, string propertyName, out PropertyInfo propertyInfo)
    {
        var classType = targetObject.GetType();
        propertyInfo = classType.GetProperty(propertyName);

        return propertyInfo != null;
    }
}
#endif