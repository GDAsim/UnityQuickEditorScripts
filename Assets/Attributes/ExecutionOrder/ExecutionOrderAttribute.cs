/*
 * About:
 * Custom Attribute for Monoscript/Classes To automatically set Script Exeuction Order
 * 
 * How To Use:
 * Use [ExecutionOrder] on Classes
 * 
 */

using System;
using System.Collections.Generic;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
public class ExecutionOrderAttribute : Attribute
{
    public readonly int ExecutionOrder = 0;

    public ExecutionOrderAttribute(int executionOrder)
    {
        ExecutionOrder = executionOrder;
    }

#if UNITY_EDITOR
    [InitializeOnLoadMethod]
    static void ExecuteOnLoad()
    {
        var type = typeof(ExecutionOrderAttribute);
        var scriptsWithAttribute = new Dictionary<MonoScript, ExecutionOrderAttribute>();

        // 1. Loop though all Classess within the same Assembly namespace
        foreach (var item in type.Assembly.GetTypes())
        {
            // 2. Check if the class uses Attributes and that is of type ExecutionOrderAttribute
            var attributes = item.GetCustomAttributes(type, false);
            if (attributes.Length == 0) continue;
            var executionOrderAttribute = attributes[0] as ExecutionOrderAttribute;


            // Find the Matching Asset of type:script (file) using name (as unity forces class name and file name to be the same)
            var asset = "";
            var files = AssetDatabase.FindAssets(string.Format("{0} t:script", item.Name));

            if (files.Length > 1)
            {
                foreach (var file in files)
                {
                    var assetPath = AssetDatabase.GUIDToAssetPath(file);
                    var filename = Path.GetFileNameWithoutExtension(assetPath);
                    if (filename == item.Name)
                    {
                        asset = file;
                        break;
                    }
                }
            }
            else if (files.Length == 1)
            {
                asset = files[0];
            }
            else
            {
                return;
            }

            var script = AssetDatabase.LoadAssetAtPath<MonoScript>(AssetDatabase.GUIDToAssetPath(asset));

            scriptsWithAttribute.Add(script, executionOrderAttribute);
        }

        var changed = false;

        foreach (var item in scriptsWithAttribute)
        {
            if (MonoImporter.GetExecutionOrder(item.Key) != item.Value.ExecutionOrder)
            {
                changed = true;
                break;
            }
        }

        if (!changed) return;

        foreach (var item in scriptsWithAttribute)
        {
            if (MonoImporter.GetExecutionOrder(item.Key) != item.Value.ExecutionOrder)
            {
                // This Function Triggers InitializeOnLoadMethod
                MonoImporter.SetExecutionOrder(item.Key, item.Value.ExecutionOrder);
            }
        }
    }
#endif
}