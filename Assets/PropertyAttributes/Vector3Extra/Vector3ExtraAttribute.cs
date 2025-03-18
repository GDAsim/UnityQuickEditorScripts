/*
 * About:
 * Gives Extra Buttons to Vector3 in the Inspector
 * 
 * How To Use:
 * Add attribute [Vector3Extra] to Vector3 variables
 * 
 */

using UnityEngine;
using Random = UnityEngine.Random;

#if UNITY_EDITOR

using UnityEditor;

[CustomPropertyDrawer(typeof(Vector3ExtraAttribute))]
public class Vector3ExtraPropertyDrawer : PropertyDrawer
{
    const float ButtonWidth = 50;
    const float RandomRange = 100;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PropertyField(position, property, label);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.Space();
        if (EditorGUIUtilities.DrawButton("0", "Zero", EditorStyles.miniButtonLeft, Color.white, ButtonWidth))
        {
            property.vector3Value = Vector3.zero;
        }
        if (EditorGUIUtilities.DrawButton("1", "One", EditorStyles.miniButtonMid, Color.white, ButtonWidth))
        {
            property.vector3Value = Vector3.one;
        }
        if (EditorGUIUtilities.DrawButton("R", "Random", EditorStyles.miniButtonRight, Color.white, ButtonWidth))
        {
            property.vector3Value = new Vector3(Random.Range(-RandomRange, RandomRange), Random.Range(-RandomRange, RandomRange), Random.Range(-RandomRange, RandomRange));
        }
        EditorGUILayout.EndHorizontal();
    }
}

#endif

public class Vector3ExtraAttribute : PropertyAttribute { }
