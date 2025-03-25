// Editor extension class

#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;

public static class UnityParameterDrawer
{
    public delegate object ParameterDrawer(ParameterInfo parameter, object val);

    public static Dictionary<Type, ParameterDrawer> ParameterDrawers = new()
    {
        {typeof(float),DrawFloatParameter},
        {typeof(int),DrawIntParameter},
        {typeof(string),DrawStringParameter},
        {typeof(bool),DrawBoolParameter},
        {typeof(Color),DrawColorParameter},
        {typeof(Vector3),DrawVector3Parameter},
        {typeof(Vector2),DrawVector2Parameter},
        {typeof(Quaternion),DrawQuaternionParameter}
    };

    static object DrawFloatParameter(ParameterInfo parameterInfo, object val)
    {
        //Since it is legal to define a float param with an integer default value (e.g void method(float p = 5);)
        //we must use Convert.ToSingle to prevent forbidden casts
        //because you can't cast an "int" object to float 
        //See for http://stackoverflow.com/questions/17516882/double-casting-required-to-convert-from-int-as-object-to-float more info
        return EditorGUILayout.FloatField(Convert.ToSingle(val));
    }

    static object DrawIntParameter(ParameterInfo parameterInfo, object val)
    {
        return EditorGUILayout.IntField((int)val);
    }

    static object DrawBoolParameter(ParameterInfo parameterInfo, object val)
    {
        return EditorGUILayout.Toggle((bool)val);
    }

    static object DrawStringParameter(ParameterInfo parameterInfo, object val)
    {
        return EditorGUILayout.TextField((string)val);
    }

    static object DrawColorParameter(ParameterInfo parameterInfo, object val)
    {
        return EditorGUILayout.ColorField((Color)val);
    }

    static object DrawVector2Parameter(ParameterInfo parameterInfo, object val)
    {
        return EditorGUILayout.Vector2Field("", (Vector2)val);
    }

    static object DrawVector3Parameter(ParameterInfo parameterInfo, object val)
    {
        return EditorGUILayout.Vector3Field("", (Vector3)val);
    }

    static object DrawQuaternionParameter(ParameterInfo parameterInfo, object val)
    {
        return Quaternion.Euler(EditorGUILayout.Vector3Field("", ((Quaternion)val).eulerAngles));
    }
}

#endif