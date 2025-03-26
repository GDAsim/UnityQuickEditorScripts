/*
 * About:
 * Custom UnityEditor Property Drawer for [AnimatorParam] CustomAttribute
 * Select Animator parameters from a dropdown in inspector and cache it directly into a Int or String Varaible.
 * This removes the need to keep track of index when using Animator.GetParameter(int index)
 * 
 * How To Use:
 * Use [AnimatorParam("fieldname")] on the int or string variables
 * 
 */

using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

using UnityEditor.Animations;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;

[CustomPropertyDrawer(typeof(AnimatorParamAttribute))]
public class AnimatorParamPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var animatorParamAttribute = attribute as AnimatorParamAttribute;
        var targetObject = property.serializedObject.targetObject;

        // Check for Animator Field using name
        var type = targetObject.GetType();
        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public);
        var targetField = fields.FirstOrDefault(field => field.Name == animatorParamAttribute.AnimatorFieldName);

        if (targetField == default)
        {
            Debug.LogError($"No Field of Name {animatorParamAttribute.AnimatorFieldName}", targetObject);

            EditorGUIUtilities.DrawErrorField(position, property, label);

            return;
        }

        var animator = targetField.GetValue(targetObject) as Animator;
        if (animator == null)
        {
            Debug.LogError($"Field {animatorParamAttribute.AnimatorFieldName} is not Animator", targetObject);

            EditorGUIUtilities.DrawErrorField(position, property, label);

            return;
        }

        // Check for Animator for animator controller
        var animatorController = animator.runtimeAnimatorController as AnimatorController;
        if (animatorController == null)
        {
            Debug.LogError("Target animator controller is null", targetObject);

            EditorGUIUtilities.DrawErrorField(position, property, label, "Target animator controller is null");

            return;
        }

        int parametersCount = animatorController.parameters.Length;
        var animatorParameters = new List<AnimatorControllerParameter>(parametersCount);
        for (int i = 0; i < parametersCount; i++)
        {
            var parameter = animatorController.parameters[i];
            animatorParameters.Add(parameter);
        }

        if (property.propertyType == SerializedPropertyType.Integer)
        {
            int currentIndex = animatorParameters.FindIndex(param => param.nameHash == property.intValue);
            if (currentIndex < 0) currentIndex = 0;

            string[] options = animatorParameters.Select(p => { return $"{p.name} ({p.type.ToString()})"; }).ToArray();

            int newIndex = EditorGUI.Popup(position, label.text, currentIndex, options);
            property.intValue = animatorParameters[newIndex].nameHash;
        }
        else if (property.propertyType == SerializedPropertyType.String)
        {
            int currentIndex = animatorParameters.FindIndex(param => param.name == property.stringValue);
            if (currentIndex < 0) currentIndex = 0;

            string[] options = animatorParameters.Select(p => { return $"{p.name} ({p.type.ToString()})"; }).ToArray();

            int newIndex = EditorGUI.Popup(position, label.text, currentIndex, options);
            property.stringValue = animatorParameters[newIndex].name;
        }
        else
        {
            Debug.LogError("Attribute Field must be an int or a string", targetObject);

            EditorGUIUtilities.DrawErrorField(position, property, label);

            return;
        }
    }
}
#endif

public class AnimatorParamAttribute : PropertyAttribute
{
    public readonly string AnimatorFieldName;

    public AnimatorParamAttribute(string animatorName)
    {
        AnimatorFieldName = animatorName;
    }
}