/*
 * About:
 * Custom UnityEditor Property Drawer for [EnumFlags] CustomAttribute
 * 
 * How To Use:
 * Use [EnumFlags] on enums to have unity inspector draw a custom GUI for enums
 * 
 */

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(EnumFlagAttribute))]
public class EnumFlagAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        int buttonsIntValue = 0;
        int enumLength = property.enumNames.Length;
        bool[] buttonPressed = new bool[enumLength];
        float buttonWidth = (position.width - EditorGUIUtility.labelWidth) / enumLength;

        EditorGUI.LabelField(new Rect(position.x, position.y, EditorGUIUtility.labelWidth, position.height), label);

        EditorGUI.BeginChangeCheck();

        // ignore/skip 0, because c# enum has a enum none = 0 for c# flags
        for (int i = 1; i < enumLength; i++)
        {
            // first Check value for button to be on or off
            if ((property.intValue & (1 << i-1)) != 0)
            {
                buttonPressed[i] = true;
            }

            Rect buttonPos = new Rect(position.x + EditorGUIUtility.labelWidth + buttonWidth * i, position.y, buttonWidth, position.height);

            buttonPressed[i] = GUI.Toggle(buttonPos, buttonPressed[i], property.enumNames[i], "Button");

            if (buttonPressed[i])
            {
                //Binary shift left 0,1,2,4
                buttonsIntValue += 1 << i - 1;
            }
        }

        if (EditorGUI.EndChangeCheck())
        {
            property.intValue = buttonsIntValue;
        }
    }
}

#endif

public class EnumFlagAttribute : PropertyAttribute {}