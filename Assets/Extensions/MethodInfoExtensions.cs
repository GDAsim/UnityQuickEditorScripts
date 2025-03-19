using System;
using System.Reflection;
using Unity.VisualScripting;

public static class MethodInfoExtensions
{
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
}
