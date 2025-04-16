/*
 * About:
 * Provides a fast way to open Unity Related Directories
 * 
 */

#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.IO;
using System.Diagnostics;

public class OpenFolders
{
    [MenuItem("EditorTools/Open Folder/Persistent data")]
    static void OpenPersistentData()
    {
        string path = Application.persistentDataPath;
        Process.Start(path);
    }

    [MenuItem("EditorTools/Open Folder/Console Log")]
    static void OpenConsoleLog()
    {
        string path = Application.consoleLogPath;
        Process.Start(path);
    }

    [MenuItem("EditorTools/Open Folder/Editor root")]
    static void OpenEditorRoot()
    {
        string path = Directory.GetParent(EditorApplication.applicationPath).ToString();
        Process.Start(path);
    }

    [MenuItem("EditorTools/Open Folder/Script Templates")]
    static void OpenScriptTemplate()
    {
        string path = Directory.GetParent(EditorApplication.applicationPath) + "/Data/Resources/ScriptTemplates/";
        Process.Start(path);
    }
}

#endif