/*
 * About:
 * Custom UnityEditor Property Drawer for [KeyCodeSelect] CustomAttribute
 * Changes KeyCode variable inspector to be a clickable button which will prompt you to press the button you want for the key
 * 
 * How To Use:
 * Use [KeyCodeSelect] on KeyCode variables
 * 
 */

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(KeyCodeSelectAttribute))]
public class KeyCodeSelectPropertyDrawer : PropertyDrawer
{
    float ButtonWidth = 50f;
    const float Spacing = 2f;
    Color selectedColor = new Color(0.4f, 0.65f, 1f, 1f);

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var targetObject = property.serializedObject.targetObject;

        if (property.GetFieldType() != typeof(KeyCode))
        {
            Debug.LogError("Only KeyCode Type is allowed for this Attribute");

            EditorGUIUtilities.DrawErrorField(position, property, label);

            return;
        }

        int id = GUIUtility.GetControlID((int)position.x, FocusType.Keyboard, position);

        bool isKeyboardMode = GUIUtility.keyboardControl == id;

        position = EditorGUI.PrefixLabel(position, label);

        position.width -= ButtonWidth + Spacing / 2;

        if (!isKeyboardMode)
        {
            var keycode = (KeyCode)property.enumValueIndex;
            property.enumValueIndex = (int)(KeyCode)EditorGUI.EnumPopup(position, keycode);
        }
        else
        {
            var prevColor = GUI.color;
            GUI.color = selectedColor;
            if (GUI.Button(position, "[Press Any Key]", EditorStyles.popup))
            {

            }
            GUI.color = prevColor;
        }

        position.x = position.x + position.width + Spacing;
        position.width = ButtonWidth;

        if (isKeyboardMode)
        {
            if (GUI.Button(position, "Cancel"))
            {
                Event.current.Use();
                GUIUtility.keyboardControl = -1;
            }
        }
        else
        {
            if (GUI.Button(position, "Select"))
            {
                GUIUtility.keyboardControl = id;
                Event.current.Use();
            }
        }

        if (isKeyboardMode)
        {
            if (Event.current.type == EventType.KeyUp)
            {
                property.enumValueIndex = (int)Event.current.keyCode;

                Event.current.Use();
                GUIUtility.keyboardControl = -1;
            }
        }
    }

    public int KeyCodeToEnum(SerializedProperty keyCodeProperty, KeyCode keyCode)
    {
        string[] keyCodeNames = keyCodeProperty.enumNames;
        for (int i = 0; i < keyCodeNames.Length; i++)
        {
            if (keyCodeNames[i].CompareTo(keyCode.ToString()) == 0) return i;
        }

        return 0;
    }
}
#endif

public class KeyCodeSelectAttribute : PropertyAttribute { }