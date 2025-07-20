/*
 * About:
 * Custom UnityEditor Property Drawer for [Foldout] CustomAttribute
 * Allows ScriptableObject to be able to expand in the inspector and modify and edit
 * 
 * How To Use:
 * Use [Foldout] on the ScriptableObject variable
 * 
 * Source:
 * https://github.com/PixeyeHQ/InspectorFoldoutGroup
 * 
 */

using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

#endif

public class FoldoutAttribute : PropertyAttribute
{
    public string name;
    public bool foldEverything;

    public FoldoutAttribute(string name, bool foldEverything = false)
    {
        this.foldEverything = foldEverything;
        this.name = name;
    }
}