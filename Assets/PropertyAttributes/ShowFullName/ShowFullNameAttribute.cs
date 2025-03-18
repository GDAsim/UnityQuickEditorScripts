/*
 * About:
 * Custom Attribute to have display of fullname of the variable in the inspector.
 * Normally the name gets hidden when the window is small
 * This however means the Property Input GUI will be hidden instead.
 * 
 * How To Use:
 * Add [ShowFullName] on variables that have a long important name
 * 
 * Note:
 * Works on Array child elements but does not work on Arrays itself.
 */

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(ShowFullNameAttribute))]
public class ShowFullNamePropertyDrawer : PropertyDrawer
{
    Vector2 propertyLabelSize;

    const float INDENT_WIDTH = 16;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (Event.current.type == EventType.Repaint)
        {
            propertyLabelSize = EditorStyles.label.CalcSize(label);
        }

        EditorGUIUtility.labelWidth = propertyLabelSize.x + (EditorGUI.indentLevel * INDENT_WIDTH) + 5;
        EditorGUI.PropertyField(position, property, label);
    }
}

#endif

public class ShowFullNameAttribute : PropertyAttribute { }
