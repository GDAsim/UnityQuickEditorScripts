using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class EditorGUIExample : MonoBehaviour
{
    [EditorGUIExample] public int a;
}

public enum Fruits
{
    None,
    Orange,
    Potato
}

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(EditorGUIExampleAttribute))]
public class EditorGUIExamplePropertyDrawer : PropertyDrawer
{
    string folderpath;
    string filepath;
    Fruits f;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        folderpath = EditorGUIUtilities.FolderLabel(nameof(folderpath), 100, folderpath);
        filepath = EditorGUIUtilities.FileLabel(nameof(filepath), 100, filepath, "txt");
        f = EditorGUIUtilities.EnumToolbar(f);
    }
}
#endif

public class EditorGUIExampleAttribute : PropertyAttribute { }