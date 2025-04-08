/*
 * About:
 * Custom UnityEditor Property Drawer for [MiniButton] CustomAttribute
 * Draws an mini button at the end of a string property that allows user to select a folder as string
 * 
 * How To Use:
 * Use [Path] on a string variable
 * 
 */

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(PathAttribute))]
public class PathPropertyDrawer : PropertyDrawer
{
    const float ButtonWidth = 32f;
    const float Spacing = 2f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var targetObject = property.serializedObject.targetObject;

        if (property.propertyType != SerializedPropertyType.String)
        {
            Debug.LogError("Attribute Field must be string", targetObject);

            EditorGUIUtilities.DrawErrorField(position, property, label);

            return;
        }

        position.width -= ButtonWidth + Spacing / 2;

        EditorGUI.PropertyField(position, property, label);

        position.x = position.x + position.width + Spacing;
        position.width = ButtonWidth;

        GUIContent buttonContent = EditorGUIUtility.IconContent("d_FolderOpened Icon");
        if (GUI.Button(position, buttonContent))
        {
            string path = EditorUtility.OpenFolderPanel("Select a folder", "Assets", "");
            property.stringValue = path;
            property.serializedObject.ApplyModifiedProperties();

            // Exit Early, otherwise OpenFolderPanel will cause error due to how property draws is not compatible with the folder panel. 
            // Early out just avoids error message
            GUIUtility.ExitGUI();
        }
    }
}
#endif

public class PathAttribute : PropertyAttribute { }