using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

public static class MethodInfoExtensions
{
    /// <summary>
    /// Names of the different types in string, cant use parameterInfo.ParameterType.Name, as this gives different string
    /// As of now, theres no alternatives
    /// </summary>
    public static Dictionary<Type, string> TypeDisplayNames = new()
    {
        {typeof(float),"float"},
        {typeof(int),"int"},
        {typeof(string),"string"},
        {typeof(bool),"bool"},
        {typeof(Color),"Color"},
        {typeof(Vector3),"Vector3"},
        {typeof(Vector2),"Vector2"},
        {typeof(Quaternion),"Quaternion"}
    };

    /// <summary>
    /// Check if method parameter has correct matching parameters
    /// </summary>
    public static bool ValidateMethodParameters(this MethodInfo methodInfo, in object[] args)
    {
        var parameters = methodInfo.GetParameters();

        if (parameters.Length != args.Length) return false;

        for (int i = 0; i < parameters.Length; i++)
        {
            var param = parameters[i];
            if (param.ParameterType != args[i].GetType()) return false;
        }

        return true;
    }

    public static string GetMethodDisplayName(this MethodInfo method, bool withParameter)
    {
        var sb = new StringBuilder();
        sb.Append(method.Name);

        if (withParameter)
        {
            sb.Append("(");
            var methodParams = method.GetParameters();
            foreach (ParameterInfo parameter in methodParams)
            {
                sb.Append(GetParameterDisplayName(parameter));
                sb.Append(",");
            }
            if (methodParams.Length > 0) sb.Remove(sb.Length - 1, 1);
            sb.Append(")");
        }

        return sb.ToString();
    }

    /// <summary>
    /// Returns the display name of the parameter type
    /// e.g float => "float"
    /// </summary>
    public static string GetParameterDisplayName(this ParameterInfo parameterInfo, bool withVarName = true)
    {
        if (!TypeDisplayNames.TryGetValue(parameterInfo.ParameterType, out string typeDisplayName))
        {
            typeDisplayName = parameterInfo.ParameterType.Name;
        }

        var parameterDisplayName = withVarName ? $"{typeDisplayName} {parameterInfo.Name}" : typeDisplayName;
        return parameterDisplayName;
    }
}