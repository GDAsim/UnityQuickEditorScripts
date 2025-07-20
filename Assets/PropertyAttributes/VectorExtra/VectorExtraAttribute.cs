/*
 * About:
 * Gives Extra Buttons to Vector types in the Inspector
 * 
 * How To Use:
 * Add attribute [VectorExtra] to Vector variables
 * 
 */

using UnityEngine;
using Random = UnityEngine.Random;

#if UNITY_EDITOR

using UnityEditor;

[CustomPropertyDrawer(typeof(VectorExtraAttribute))]
public class VectorExtraPropertyDrawer : PropertyDrawer
{
    const float ButtonWidth = 50;
    const float RandomRange = 100;
    const int RandomRangeInt = 100;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var targetObject = property.serializedObject.targetObject;

        if (property.propertyType == SerializedPropertyType.Vector2)
        {
            EditorGUI.PropertyField(position, property, label);

            var height = EditorGUI.GetPropertyHeight(property);
            EditorGUILayout.Space(height - EditorGUIUtility.singleLineHeight);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            if (EditorGUIUtilities.DrawButton("0", "Zero", EditorStyles.miniButtonLeft, Color.white, ButtonWidth))
            {
                property.vector2Value = Vector2.zero;
            }
            if (EditorGUIUtilities.DrawButton("1", "One", EditorStyles.miniButtonMid, Color.white, ButtonWidth))
            {
                property.vector2Value = Vector2.one;
            }
            if (EditorGUIUtilities.DrawButton("R", "Random", EditorStyles.miniButtonRight, Color.white, ButtonWidth))
            {
                var random = new Vector2(Random.Range(-RandomRange, RandomRange), Random.Range(-RandomRange, RandomRange));
                property.vector2Value = random;
            }
            EditorGUILayout.EndHorizontal();
        }
        else if (property.propertyType == SerializedPropertyType.Vector2Int)
        {
            EditorGUI.PropertyField(position, property, label);

            var height = EditorGUI.GetPropertyHeight(property);
            EditorGUILayout.Space(height - EditorGUIUtility.singleLineHeight);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            if (EditorGUIUtilities.DrawButton("0", "Zero", EditorStyles.miniButtonLeft, Color.white, ButtonWidth))
            {
                property.vector2IntValue = Vector2Int.zero;
            }
            if (EditorGUIUtilities.DrawButton("1", "One", EditorStyles.miniButtonMid, Color.white, ButtonWidth))
            {
                property.vector2IntValue = Vector2Int.one;
            }
            if (EditorGUIUtilities.DrawButton("R", "Random", EditorStyles.miniButtonRight, Color.white, ButtonWidth))
            {
                var random = new Vector2Int(Random.Range(-RandomRangeInt, RandomRangeInt), Random.Range(-RandomRangeInt, RandomRangeInt));
                property.vector2IntValue = random;
            }
            EditorGUILayout.EndHorizontal();
        }
        else if (property.propertyType == SerializedPropertyType.Vector3)
        {
            EditorGUI.PropertyField(position, property, label);

            var height = EditorGUI.GetPropertyHeight(property);
            EditorGUILayout.Space(height - EditorGUIUtility.singleLineHeight);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            if (EditorGUIUtilities.DrawButton("0", "Zero", EditorStyles.miniButtonLeft, Color.white, ButtonWidth))
            {
                property.vector3Value = Vector2.zero;
            }
            if (EditorGUIUtilities.DrawButton("1", "One", EditorStyles.miniButtonMid, Color.white, ButtonWidth))
            {
                property.vector3Value = Vector2.one;
            }
            if (EditorGUIUtilities.DrawButton("R", "Random", EditorStyles.miniButtonRight, Color.white, ButtonWidth))
            {
                var random = new Vector3(Random.Range(-RandomRange, RandomRange), Random.Range(-RandomRange, RandomRange), Random.Range(-RandomRange, RandomRange));
                property.vector3Value = random;
            }
            EditorGUILayout.EndHorizontal();
        }
        else if (property.propertyType == SerializedPropertyType.Vector3Int)
        {
            EditorGUI.PropertyField(position, property, label);

            var height = EditorGUI.GetPropertyHeight(property);
            EditorGUILayout.Space(height - EditorGUIUtility.singleLineHeight);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            if (EditorGUIUtilities.DrawButton("0", "Zero", EditorStyles.miniButtonLeft, Color.white, ButtonWidth))
            {
                property.vector3IntValue = Vector3Int.zero;
            }
            if (EditorGUIUtilities.DrawButton("1", "One", EditorStyles.miniButtonMid, Color.white, ButtonWidth))
            {
                property.vector3IntValue = Vector3Int.one;
            }
            if (EditorGUIUtilities.DrawButton("R", "Random", EditorStyles.miniButtonRight, Color.white, ButtonWidth))
            {
                var random = new Vector3Int(Random.Range(-RandomRangeInt, RandomRangeInt), Random.Range(-RandomRangeInt, RandomRangeInt), Random.Range(-RandomRangeInt, RandomRangeInt));
                property.vector3IntValue = random;
            }
            EditorGUILayout.EndHorizontal();
        }
        else if (property.propertyType == SerializedPropertyType.Vector4)
        {
            EditorGUI.PropertyField(position, property, label, true);

            //// height when expanded includes children which is 20px each
            var height = EditorGUI.GetPropertyHeight(property);
            if (property.isExpanded)
            {
                EditorGUILayout.Space(height - EditorGUIUtility.singleLineHeight - 20 * 4);
            }
            else
            {
                EditorGUILayout.Space(height - EditorGUIUtility.singleLineHeight);
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Space();
            if (EditorGUIUtilities.DrawButton("0", "Zero", EditorStyles.miniButtonLeft, Color.white, ButtonWidth))
            {
                property.vector4Value = Vector4.zero;
            }
            if (EditorGUIUtilities.DrawButton("1", "One", EditorStyles.miniButtonMid, Color.white, ButtonWidth))
            {
                property.vector4Value = Vector4.one;
            }
            if (EditorGUIUtilities.DrawButton("R", "Random", EditorStyles.miniButtonRight, Color.white, ButtonWidth))
            {
                var random = new Vector4(Random.Range(-RandomRange, RandomRange), Random.Range(-RandomRange, RandomRange), Random.Range(-RandomRange, RandomRange), Random.Range(-RandomRange, RandomRange));
                property.vector4Value = random;
            }
            EditorGUILayout.EndHorizontal();
        }
        else
        {
            Debug.LogError($"VectorExtra attribute property is not of type \"Vector\"", targetObject);

            EditorGUIUtilities.DrawErrorField(position, property, label);

            return;
        }
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

public class VectorExtraAttribute : PropertyAttribute { }
