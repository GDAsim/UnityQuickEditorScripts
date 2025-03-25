/*
 * About:
 * [Button] CustomAttribute.
 * Currently being used for Tagging purpose for ButtonEditor
 * See ButtonEditor.cs
 * 
 */

using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Method)]
public class ButtonAttribute : PropertyAttribute 
{
    public readonly string Text;

    public ButtonAttribute(string text = null)
    {
        Text = text;
    }
}