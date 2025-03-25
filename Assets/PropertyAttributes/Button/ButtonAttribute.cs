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