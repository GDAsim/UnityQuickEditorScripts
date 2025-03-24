/*
 * About:
 * Custom UnityEditor Property Drawer for [MaxValue] CustomAttribute
 * Limits a integer or float variable when adjusting in the inspector
 * 
 * Features:
 * Supports Eval from Static,Public,Private Method.
 * Supports Return types that can be converted to float/int
 * Supports Properties
 * 
 * How To Use:
 * Use [MaxValue] on the int or float variables
 * 
 */

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(MaxValueAttribute))]
public class MaxValuePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var maxValueAttribute = attribute as MaxValueAttribute;
        var targetObject = property.serializedObject.targetObject;

        // Set MinValue From Attribute Input Value or MethodCallback
        float maxValue = maxValueAttribute.Value;

        bool haveCallback = !string.IsNullOrEmpty(maxValueAttribute.ValueCallback);
        if (haveCallback)
        {
            // Check Method
            if (targetObject.TryGetMethodFromTarget(maxValueAttribute.ValueCallback, out var methodInfo))
            {
                if (methodInfo.ValidateMethodParameters(maxValueAttribute.MethodParameters))
                {
                    var result = methodInfo.Invoke(targetObject, maxValueAttribute.MethodParameters);
                    if (float.TryParse(result.ToString(), out var floatvalue))
                    {
                        maxValue = floatvalue;
                    }
                    else
                    {
                        Debug.LogError("The Method Return type needs to support converting to float", targetObject);

                        EditorGUIUtilities.DrawErrorField(position, property, label);

                        return;
                    }
                }
                else
                {
                    Debug.LogError($"The MinValueAttribute input parameters does not match Method\"{maxValueAttribute.ValueCallback}\" ", targetObject);

                    EditorGUIUtilities.DrawErrorField(position, property, label);

                    return;
                }
            }

            // Check Property
            else if (targetObject.TryGetPropertyFromTarget(maxValueAttribute.ValueCallback, out var propertyInfo))
            {
                var result = propertyInfo.GetValue(targetObject);
                if (float.TryParse(result.ToString(), out var floatvalue))
                {
                    maxValue = floatvalue;
                }
                else
                {
                    Debug.LogError("The Get Property type needs to support converting to float", targetObject);

                    EditorGUIUtilities.DrawErrorField(position, property, label);

                    return;
                }
            }
            else
            {
                Debug.LogWarning($"Method/Property \"{maxValueAttribute.ValueCallback}\" Not Found");

                EditorGUIUtilities.DrawErrorField(position, property, label);

                return;
            }
        }

        // Apply Min Clamp To supported Types
        if (property.propertyType == SerializedPropertyType.Integer)
        {
            if (property.intValue > maxValue)
            {
                property.intValue = (int)maxValue;
            }
        }
        else if (property.propertyType == SerializedPropertyType.Float)
        {
            if (property.floatValue > maxValue)
            {
                property.floatValue = maxValue;
            }
        }
        else if (property.propertyType == SerializedPropertyType.Vector2)
        {
            property.vector2Value = Vector2.Min(property.vector2Value, new Vector2(maxValue, maxValue));
        }
        else if (property.propertyType == SerializedPropertyType.Vector3)
        {
            property.vector3Value = Vector3.Min(property.vector3Value, new Vector3(maxValue, maxValue, maxValue));
        }
        else if (property.propertyType == SerializedPropertyType.Vector4)
        {
            property.vector4Value = Vector4.Min(property.vector4Value, new Vector4(maxValue, maxValue, maxValue, maxValue));
        }
        else if (property.propertyType == SerializedPropertyType.Vector2Int)
        {
            property.vector2IntValue = Vector2Int.Min(property.vector2IntValue, new Vector2Int((int)maxValue, (int)maxValue));
        }
        else if (property.propertyType == SerializedPropertyType.Vector3Int)
        {
            property.vector3IntValue = Vector3Int.Min(property.vector3IntValue, new Vector3Int((int)maxValue, (int)maxValue, (int)maxValue));
        }
        else
        {
            Debug.LogError("Supported Types Allowed: Int,Float,Vector,VectorInt", targetObject);

            EditorGUIUtilities.DrawErrorField(position, property, label);

            return;
        }

        // Draw default
        EditorGUI.PropertyField(position, property, label, true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (property.isExpanded)
        {
            return
                property.CountInProperty() *
                (EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing);
        }
        else
        {
            return base.GetPropertyHeight(property, label);
        }
    }
}
#endif

public class MaxValueAttribute : PropertyAttribute
{
    public readonly float Value;
    public readonly string ValueCallback;
    public readonly object[] MethodParameters;

    public MaxValueAttribute(float value)
    {
        Value = value;
        ValueCallback = null;
        MethodParameters = null;
    }

    public MaxValueAttribute(string valueCallback, params object[] args)
    {
        Value = -1;
        ValueCallback = valueCallback;
        MethodParameters = args;
    }
}