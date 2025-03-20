/*
 * About:
 * [Info] Attribute & Decorator Drawer to Draw a Message Box
 * Because this is a decorator drawer, Message Box appears Above the property field.
 * 
 * How To Use:
 * [Info("Info")]
 * [Info("Warning", messageType: HelpBoxMessageType.Warning)]
 * [Info("Error", fontSize: 24, messageType: HelpBoxMessageType.Error)]
 * 
 * Improvements:
 * 1. Add String Callback, to show info only when string is not null or empty
 * 
 */

using UnityEngine;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(InfoAttribute))]
public class InfoDecoratorDrawer : DecoratorDrawer
{
    const float Padding = 8f;

    float estimateHeightWithText;
    public override void OnGUI(Rect position)
    {
        var infoAttribute = attribute as InfoAttribute;

        position.y += Padding;
        position.height -= Padding;

        EditorGUI.HelpBox(position, infoAttribute.Text, GetMessageType(infoAttribute.MessageType));

        // Calculate height is done here because we are using GUI methods which can only be done in unity onGUI
        if (string.IsNullOrEmpty(infoAttribute.Text))
        {
            estimateHeightWithText = 0;
        } 
        else
        {
            var helpBoxStyle = GUI.skin.GetStyle("helpbox");
            helpBoxStyle.fontSize = infoAttribute.FontSize;
            estimateHeightWithText = helpBoxStyle.CalcHeight(new GUIContent(infoAttribute.Text), EditorGUIUtility.currentViewWidth);
        }
    }

    public override float GetHeight()
    {
        var guiHeight = estimateHeightWithText + Padding;

        return guiHeight;
    }

    MessageType GetMessageType(HelpBoxMessageType helpBoxMessageType)
    {
        switch (helpBoxMessageType)
        {
            default:
            case HelpBoxMessageType.None: return MessageType.None;
            case HelpBoxMessageType.Info: return MessageType.Info;
            case HelpBoxMessageType.Warning: return MessageType.Warning;
            case HelpBoxMessageType.Error: return MessageType.Error;
        }
    }
}

#endif

public class InfoAttribute : PropertyAttribute
{
    public readonly string Text;
    public readonly HelpBoxMessageType MessageType; // Need to use this type because MessageType is UnityEditor only
    public readonly int FontSize;

    public InfoAttribute(string text, HelpBoxMessageType messageType = HelpBoxMessageType.Info)
    {
        Text = text;
        MessageType = messageType;
    }
    public InfoAttribute(string text, int fontSize, HelpBoxMessageType messageType = HelpBoxMessageType.Info)
    {
        Text = text;
        MessageType = messageType;
        FontSize = fontSize;
    }
}