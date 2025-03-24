/*
 * About:
 * Custom UnityEditor Property Drawer for [MinValue] CustomAttribute. Similar to Unity's [Min] Attribute
 * Limits a integer or float variable when adjusting in the inspector
 * 
 * Features:
 * Supports Eval from Static,Public,Private Method.
 * Supports Return types that can be converted to float/int
 * Supports Properties
 * 
 * How To Use:
 * Use [MinValue] on the int or float variables
 * 
 */

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(MinValueAttribute))]
public class MinValuePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var minValueAttribute = attribute as MinValueAttribute;
        var targetObject = property.serializedObject.targetObject;

        // Set MinValue From Attribute Input Value or MethodCallback
        float minValue = minValueAttribute.Value;

        bool haveCallback = !string.IsNullOrEmpty(minValueAttribute.ValueCallback);
        if (haveCallback)
        {
            // Check Method
            if (targetObject.TryGetMethodFromTarget(minValueAttribute.ValueCallback, out var methodInfo))
            {
                if (methodInfo.ValidateMethodParameters(minValueAttribute.MethodParameters))
                {
                    var result = methodInfo.Invoke(targetObject, minValueAttribute.MethodParameters);
                    if (float.TryParse(result.ToString(), out var floatvalue))
                    {
                        minValue = floatvalue;
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
                    Debug.LogError($"The MinValueAttribute input parameters does not match Method\"{minValueAttribute.ValueCallback}\" ", targetObject);

                    EditorGUIUtilities.DrawErrorField(position, property, label);

                    return;
                }
            }

            // Check Property
            else if (targetObject.TryGetPropertyFromTarget(minValueAttribute.ValueCallback, out var propertyInfo))
            {
                var result = propertyInfo.GetValue(targetObject);
                if (float.TryParse(result.ToString(), out var floatvalue))
                {
                    minValue = floatvalue;
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
                Debug.LogWarning($"Method/Property \"{minValueAttribute.ValueCallback}\" Not Found");

                EditorGUIUtilities.DrawErrorField(position, property, label);

                return;
            }
        }

        // Apply Min Clamp To supported Types
        if (property.propertyType == SerializedPropertyType.Integer)
        {
            if (property.intValue < minValue)
            {
                property.intValue = (int)minValue;
            }
        }
        else if (property.propertyType == SerializedPropertyType.Float)
        {
            if (property.floatValue < minValue)
            {
                property.floatValue = minValue;
            }
        }
        else if (property.propertyType == SerializedPropertyType.Vector2)
        {
            property.vector2Value = Vector2.Max(property.vector2Value, new Vector2(minValue, minValue));
        }
        else if (property.propertyType == SerializedPropertyType.Vector3)
        {
            property.vector3Value = Vector3.Max(property.vector3Value, new Vector3(minValue, minValue, minValue));
        }
        else if (property.propertyType == SerializedPropertyType.Vector4)
        {
            property.vector4Value = Vector4.Max(property.vector4Value, new Vector4(minValue, minValue, minValue, minValue));
        }
        else if (property.propertyType == SerializedPropertyType.Vector2Int)
        {
            property.vector2IntValue = Vector2Int.Max(property.vector2IntValue, new Vector2Int((int)minValue, (int)minValue));
        }
        else if (property.propertyType == SerializedPropertyType.Vector3Int)
        {
            property.vector3IntValue = Vector3Int.Max(property.vector3IntValue, new Vector3Int((int)minValue, (int)minValue, (int)minValue));
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

public class MinValueAttribute : PropertyAttribute
{
    public readonly float Value;
    public readonly string ValueCallback;
    public readonly object[] MethodParameters;

    public MinValueAttribute(float value)
    {
        Value = value;
        ValueCallback = null;
        MethodParameters = null;
    }

    public MinValueAttribute(string valueCallback, params object[] args)
    {
        Value = -1;
        ValueCallback = valueCallback;
        MethodParameters = args;
    }
}