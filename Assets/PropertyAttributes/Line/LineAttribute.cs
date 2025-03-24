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

        var text = lineAttribute.Text;
        var textWidth = 0f;
        if (!string.IsNullOrEmpty(text))
        {
            textWidth = EditorStyles.label.CalcSize(new GUIContent(text)).x;

            if (lineAttribute.IsCenter)
            {

                var line1 = new Rect(position);
                line1.y += (GetHeight() - lineAttribute.Height) / 2f;
                line1.height = lineAttribute.Height;
                line1.width = (position.width - textWidth) / 2f;
                EditorGUI.DrawRect(line1, color);

                position.x = (position.width - textWidth) / 2f + lineAttribute.Spacing;
            }

            GUI.Label(position, text, new GUIStyle
            {
                normal = { textColor = color },
            });

            var line2 = new Rect(position);
            line2.y += (GetHeight() - lineAttribute.Height) / 2f;
            line2.height = lineAttribute.Height;
            line2.x += textWidth + lineAttribute.Spacing;
            line2.width -= textWidth + lineAttribute.Spacing;

            EditorGUI.DrawRect(line2, color);
        }
        else
        {
            var rect = new Rect(position);
            rect.y += (GetHeight() - lineAttribute.Height) / 2f;
            rect.height = lineAttribute.Height;
            rect.x += textWidth + lineAttribute.Spacing;
            rect.width -= textWidth + lineAttribute.Spacing;

            EditorGUI.DrawRect(rect, color);
        }
    }

    public override float GetHeight()
    {
        var lineAttribute = attribute as LineAttribute;
        return Mathf.Max(base.GetHeight(), lineAttribute.Height);
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
    public readonly bool IsCenter;

    public LineAttribute(float height, ColorUtilities.ColorEnum color = ColorUtilities.ColorEnum.Black,
        string text = "", float spacing = 2, bool isCenter = false)
    {
        Height = height;
        Color = color;

        Text = text;
        Spacing = spacing;
        IsCenter = isCenter;
    }
}