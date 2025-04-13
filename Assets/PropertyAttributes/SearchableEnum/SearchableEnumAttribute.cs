/*
 * About:
 * Adds a Searchable Popup field on selecting Enum in the inspector
 * 
 * How To Use:
 * Use [SearchableEnum] on enum variables
 * 
 * Source:
 * https://github.com/roboryantron/UnityEditorJunkie
 * 
 */

/// <summary>
/// Put this attribute on a public (or SerializeField) enum in a
/// MonoBehaviour or ScriptableObject to get an improved enum selector
/// popup. The enum list is scrollable and can be filtered by typing.
/// </summary>

using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

/// <summary>
/// Base class to easily create searchable enum types
/// </summary>
//public class SearchableEnumDrawer : PropertyDrawer
//{
//    private SearchableEnumAttributeDrawer _drawer;
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        if (_drawer == null) _drawer = new SearchableEnumAttributeDrawer();
//        GUIContent content = new GUIContent(property.displayName);
//        Rect drawerRect = EditorGUILayout.GetControlRect(true, _drawer.GetPropertyHeight(property, content));
//        _drawer.OnGUI(drawerRect, property, content);
//    }
//}

/// <summary>
/// Draws the custom enum selector popup for enum fields using the
/// SearchableEnumAttribute.
/// </summary>
[CustomPropertyDrawer(typeof(SearchableEnumAttribute))]
public class SearchableEnumAttributeDrawer : PropertyDrawer
{
    /// <summary>
    /// Cache of the hash to use to resolve the ID for the drawer.
    /// </summary>
    private int idHash;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var targetObject = property.serializedObject.targetObject;

        if (property.propertyType != SerializedPropertyType.Enum)
        {
            Debug.LogError("Only Enum is supported for [SearchableEnum] attribute", targetObject);

            EditorGUIUtilities.DrawErrorField(position, property, label);

            return;
        }

        // By manually creating the control ID, we can keep the ID for the
        // label and button the same. This lets them be selected together
        // with the keyboard in the inspector, much like a normal popup.
        if (idHash == 0) idHash = "SearchableEnumAttributeDrawer".GetHashCode();
        int id = GUIUtility.GetControlID(idHash, FocusType.Keyboard, position);

        label = EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, id, label);

        GUIContent buttonText = new GUIContent(property.enumDisplayNames[property.enumValueIndex]);
        if (DropdownButton(id, position, buttonText))
        {
            SearchablePopup.Show(position, property.enumDisplayNames, property.enumValueIndex, OnSelect);

            void OnSelect(int i)
            {
                property.enumValueIndex = i;
                property.serializedObject.ApplyModifiedProperties();
            }
        }

        EditorGUI.EndProperty();
    }

    /// <summary>
    /// A custom button drawer that allows for a controlID so that we can
    /// sync the button ID and the label ID to allow for keyboard
    /// navigation like the built-in enum drawers.
    /// </summary>
    private static bool DropdownButton(int id, Rect position, GUIContent content)
    {
        Event current = Event.current;
        switch (current.type)
        {
            case EventType.MouseDown:
                if (position.Contains(current.mousePosition) && current.button == 0)
                {
                    Event.current.Use();
                    return true;
                }

                break;
            case EventType.KeyDown:
                if (GUIUtility.keyboardControl == id && current.character == '\n')
                {
                    Event.current.Use();
                    return true;
                }

                break;
            case EventType.Repaint:
                EditorStyles.popup.Draw(position, content, id, false);
                break;
        }

        return false;
    }
}
#endif

public class SearchableEnumAttribute : PropertyAttribute { }