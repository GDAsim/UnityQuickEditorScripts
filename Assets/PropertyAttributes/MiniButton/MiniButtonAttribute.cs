/*
 * About:
 * Custom UnityEditor Property Drawer for [MiniButton] CustomAttribute
 * Draws an mini button at the end of the property field
 * Draws a fix icon if no name is provided for button
 * 
 * How To Use:
 * Use [MiniButton("Click Me!", nameof(ClickMeFunc))] on a variable field
 * 
 */

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(MiniButtonAttribute))]
public class MiniButtonPropertyDrawer : PropertyDrawer
{
    float ButtonWidth = 32f;
    const float Spacing = 2f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var miniButtonAttribute = attribute as MiniButtonAttribute;
        var targetObject = property.serializedObject.targetObject;

        if (string.IsNullOrEmpty(miniButtonAttribute.OnClickFunc) ||
            !targetObject.TryGetMethodFromTarget(miniButtonAttribute.OnClickFunc, out var methodInfo))
        {
            Debug.LogError($"Validation method \"{miniButtonAttribute.OnClickFunc}\" not found in script.", targetObject);

            EditorGUIUtilities.DrawErrorField(position, property, label);

            return;
        }

        GUIContent buttonContent;
        if (!string.IsNullOrEmpty(miniButtonAttribute.ButtonText))
        {
            var buttonSize = GUI.skin.button.CalcSize(new GUIContent(miniButtonAttribute.ButtonText));
            ButtonWidth = buttonSize.x;

            buttonContent = new GUIContent(miniButtonAttribute.ButtonText);
        }
        else
        {
            buttonContent = EditorGUIUtility.IconContent("UnityLogo");
        }

        position.width -= ButtonWidth + Spacing / 2;

        EditorGUI.PropertyField(position, property, label);

        position.x = position.x + position.width + Spacing;
        position.width = ButtonWidth;

        if (GUI.Button(position, buttonContent))
        {
            methodInfo.Invoke(targetObject, null);
        }
    }
}
#endif



public class MiniButtonAttribute : PropertyAttribute
{
    public readonly string OnClickFunc;
    public readonly string ButtonText;
    public MiniButtonAttribute(string onClickFunc)
    {
        OnClickFunc = onClickFunc;
        ButtonText = null;
    }
    public MiniButtonAttribute(string buttonText, string onClickFunc)
    {
        OnClickFunc = onClickFunc;
        ButtonText = buttonText;
    }
}