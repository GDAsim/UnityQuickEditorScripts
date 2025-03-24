/*
 * About:
 * Custom UnityEditor Decorator Drawer that draws a colored line
 * 
 * How To Use:
 * Use [Line] on a field
 * 
 */

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(LineAttribute))]
public class LineDecoratorDrawer : DecoratorDrawer
{
    public override void OnGUI(Rect position)
    {
        var lineAttribute = attribute as LineAttribute;
        var color = ColorUtilities.GetColor(lineAttribute.Color);
        Rect indentedPosition = EditorGUI.IndentedRect(position);

        string title = lineAttribute.Text;
        float textWidth = 0f;
        if (title != null)
        {
            GUIStyle textColor = new GUIStyle 
            { 
                normal = { textColor = color },
            };
            GUI.Label(indentedPosition, title, textColor);
            textWidth = EditorStyles.label.CalcSize(new GUIContent(title)).x + lineAttribute.Spacing;
        }

        Rect rect = new Rect(indentedPosition);
        rect.y += EditorGUIUtility.singleLineHeight / 2f - lineAttribute.Height;
        rect.height = lineAttribute.Height;

        rect.x += textWidth;
        rect.width -= textWidth;

        EditorGUI.DrawRect(rect, color);
    }
}

#endif

[System.AttributeUsage(System.AttributeTargets.Field, AllowMultiple = true)]
public class LineAttribute : PropertyAttribute
{
    public readonly float Height;
    public readonly ColorUtilities.ColorEnum Color;
    public readonly string Text;
    public readonly float Spacing;

    public LineAttribute(float height, ColorUtilities.ColorEnum color = ColorUtilities.ColorEnum.Black,
        string text = "", float spacing = 2)
    {
        Height = height;
        Color = color;

        Text = text;
        Spacing = spacing;
    }
}