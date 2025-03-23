/*
 * About:
 * Provides a Custom UnityEditor Property Drawer for [Tag] on string var to have a dropdown select the avialable unity tags
 * 
 * How To Use:
 * Use [Tag] on string var
 * 
 */

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(TagAttribute))]
public class TagPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var targetObject = property.serializedObject.targetObject;

        if (property.propertyType == SerializedPropertyType.String)
        {
            string[] tags = new string[1 + UnityEditorInternal.InternalEditorUtility.tags.Length];
            tags[0] = "Untagged";
            UnityEditorInternal.InternalEditorUtility.tags.CopyTo(tags, 1);

            string propertyString = property.stringValue;

            int index = 0;
            for (int i = 1; i < tags.Length; i++)
            {
                if (tags[i].Equals(propertyString))
                {
                    index = i;
                    break;
                }
            }

            int newIndex = EditorGUI.Popup(position, label.text, index, tags);
            property.stringValue = tags[newIndex];
        }
        else
        {
            Debug.LogError($"Tag attribute property is not of type \"String\"", targetObject);

            EditorGUIUtilities.DrawErrorField(position, property, label);

            return;
        }
    }
}
#endif

public class TagAttribute : PropertyAttribute { }