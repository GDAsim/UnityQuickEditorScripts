/*
 * About:
 * Wrapper for creating GUI Buttons for Editor stuff
 */

#if UNITY_EDITOR

using System;
using UnityEditor;
using UnityEngine;

public static class EditorGUIUtilities
{
    /// <summary>
    /// Draw a simple GUI button
    /// </summary>
    public static bool DrawButton(string text, string tooltip, GUIStyle style, float? width = null, float? height = null)
    {
        GUIContent content = new();
        content.text = text;
        content.tooltip = tooltip;

        GUIStyle buttonstyle = new(style);
        buttonstyle.fontStyle = FontStyle.Normal;
        buttonstyle.normal.textColor = Color.black;
        buttonstyle.wordWrap = true;

        if (width.HasValue)
        {
            buttonstyle.fixedWidth = width.Value;
        }
        else
        {
            buttonstyle.stretchHeight = true;
        }

        if (height.HasValue)
        {
            buttonstyle.fixedHeight = height.Value;
        }
        else
        {
            buttonstyle.stretchWidth = true;
        }

        return GUILayout.Button(content, buttonstyle);
    }

    /// <summary>
    /// Draw a simple GUI button
    /// </summary>
    public static bool DrawButton(string text, string tooltip, GUIStyle style, Color color, float? width = null, float? height = null)
    {
        Color colorref = GUI.color;
        GUI.color = color;
        bool buttonclick = DrawButton(text, tooltip, style, width, height);
        GUI.color = colorref;

        return buttonclick;
    }

    /// <summary>
    /// Creates a folder path textfield with a browse button. Opens the save folder panel.
    /// </summary>
    public static string FolderLabel(string name, float labelWidth, string path)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(name, GUILayout.MaxWidth(labelWidth));
        string filepath = EditorGUILayout.TextField(path);
        if (GUILayout.Button("Browse", GUILayout.MaxWidth(60)))
        {
            filepath = EditorUtility.SaveFolderPanel(name, path, "Folder");
        }
        EditorGUILayout.EndHorizontal();
        return filepath;
    }

    /// <summary>
    /// Creates a filepath textfield with a browse button. Opens the open file panel.
    /// </summary>
    public static string FileLabel(string name, float labelWidth, string path, string extension)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(name, GUILayout.MaxWidth(labelWidth));
        string filepath = EditorGUILayout.TextField(path);
        if (GUILayout.Button("Browse", GUILayout.MaxWidth(60)))
        {
            filepath = EditorUtility.OpenFilePanel(name, path, extension);
        }
        EditorGUILayout.EndHorizontal();
        return filepath;
    }

    /// <summary>
    /// Creates a toolbar that is filled in from an Enum. Useful for setting tool modes.
    /// </summary>
    public static T EnumToolbar<T>(T selected) where T : Enum
    {
        string[] toolbar = Enum.GetNames(selected.GetType());
        Array values = Enum.GetValues(selected.GetType());

        for (int i = 0; i < toolbar.Length; i++)
        {
            string toolname = toolbar[i];
            toolname = toolname.Replace("_", " ");
            toolbar[i] = toolname;
        }

        int selectedIndex = 0;
        while (selectedIndex < values.Length)
        {
            if (selected.ToString() == values.GetValue(selectedIndex).ToString())
            {
                break;
            }
            selectedIndex++;
        }
        selectedIndex = GUILayout.Toolbar(selectedIndex, toolbar);
        return (T)values.GetValue(selectedIndex);
    }
}

#endif