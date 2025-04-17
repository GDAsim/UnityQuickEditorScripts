/*
 * About:
 * Provides hotkeys for quick actions in the heirachy window
 * 
 * Actions:
 * 
 * Move Selections Up - [Ctrl + Alt + W]
 * Move Selections Down - [Ctrl + Alt + S]
 * 
 */

#if UNITY_EDITOR

using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HierachyHotKeys
{
    // Arrow keys shortcut does not work in heirachy
    [MenuItem("EditorTools/Hotkeys/Hierachy/Move Up %&w")]
    static void MoveUp()
    {
        if (Selection.transforms.Length == 0) return;

        bool sameParent = true;
        Transform parent = Selection.transforms[0].parent;
        for (int i = 1; i < Selection.transforms.Length; i++)
        {
            if (Selection.transforms[i].parent != parent) sameParent = false;
        }

        if (!sameParent) return;

        bool isRootSelection = parent == null;
        if (isRootSelection)
        {
            Undo.RecordObjects(Selection.transforms, "Move Up In Hierachy");
        }
        else
        {
            Undo.RegisterChildrenOrderUndo(parent, "Move Up In Hierachy");
        }

        var orderedSelection = Selection.transforms.OrderBy(t => t.GetSiblingIndex()).ToArray();
        for (int i = 0; i < orderedSelection.Length; i++)
        {
            var sibilingIndex = orderedSelection[i].GetSiblingIndex();
            if (sibilingIndex == i) continue;

            orderedSelection[i].SetSiblingIndex(sibilingIndex - 1);
        }
    }

    // Arrow keys shortcut does not work in heirachy
    [MenuItem("EditorTools/Hotkeys/Hierachy/Move down %&s")]
    static void MoveDown()
    {
        if (Selection.activeGameObject == null) return;

        bool sameParent = true;
        Transform parent = Selection.transforms[0].parent;
        for (int i = 1; i < Selection.transforms.Length; i++)
        {
            if (Selection.transforms[i].parent != parent) sameParent = false;
        }

        if (!sameParent) return;

        int lastIndex = 0;
        bool isRootSelection = parent == null;
        if(isRootSelection)
        {
            Undo.RecordObjects(Selection.transforms, "Move Down In Hierachy");
            lastIndex = SceneManager.GetActiveScene().GetRootGameObjects().Length - 1;
        }
        else
        {
            Undo.RegisterChildrenOrderUndo(parent, "Move Down In Hierachy");
            lastIndex = parent.childCount - 1;
        }

        var orderedSelection = Selection.transforms.OrderByDescending(t => t.GetSiblingIndex()).ToArray();
        for (int i = 0; i < orderedSelection.Length; i++)
        {
            var sibilingIndex = orderedSelection[i].GetSiblingIndex();
            if (sibilingIndex == lastIndex - i) continue;

            orderedSelection[i].SetSiblingIndex(sibilingIndex + 1);
        }
    }
}

#endif