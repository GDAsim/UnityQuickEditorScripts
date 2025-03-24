/*
 * About:
 * Custom Attribute to enforces the Assignment of Objects in the inspector and Alerts when you try to enter PlayMode in Editor, and ends playmode.
 * 
 * How To Use:
 * Add [Required] on variables you want to enforce. Works on RefrencedObjects
 * 
 */

using UnityEngine;
using System.Collections.Generic;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(RequiredAttribute))]
public class RequiredPropertyDrawer : PropertyDrawer
{
    static HashSet<(Object, string)> nullObjects = new();
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var isPropertyValueNull = property.propertyType == SerializedPropertyType.ObjectReference &&
            property.objectReferenceValue == null;

        if (isPropertyValueNull)
        {
            EditorGUIUtilities.DrawColoredBGField(position, property, label, Color.red);
            var t = (property.serializedObject.targetObject, property.name);
            nullObjects.Add(t);
        }
        else
        {
            // Draw Default
            EditorGUI.PropertyField(position, property, label);
        }
    }

    [InitializeOnLoadMethod]
    static void ExecuteOnLoad()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            if (nullObjects.Count == 0) return;

            Debug.LogError("You have null Object Refrences in some of your script variables that have the [NonNull] Attribute! \n Assign or Remove [NonNull] Attribute from these variables");

            foreach ((Object gameObject, string variableName) in nullObjects)
            {
                Debug.LogError($"The variable: {variableName} is not suppose to have null Object Refrence in (GameObject){gameObject.name}, (Component){gameObject.GetType().Name}", gameObject);
            }

            EditorApplication.ExitPlaymode();
        }
    }
}
#endif

public class RequiredAttribute : PropertyAttribute { }