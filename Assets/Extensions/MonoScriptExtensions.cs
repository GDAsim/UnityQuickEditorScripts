#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

public static class MonoScriptExtensions
{
    public static bool IsMonoBehaviour(this MonoScript script)
    {
        System.Type scriptType = script.GetClass();
        if (scriptType == null) return false;

        if (typeof(MonoBehaviour).IsAssignableFrom(scriptType) == false) return false;

        return true;
    }
}

#endif