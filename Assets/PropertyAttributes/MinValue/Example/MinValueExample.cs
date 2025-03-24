using UnityEngine;

public class MinValueExample : MonoBehaviour
{
    [MinValue(10)]
    public float floatvar;

    [MinValue(10)]
    public Vector4 Vector4var;

    [MinValue(10)]
    public Vector3Int Vector3Intvar;

    [MinValue(10)]
    public float[] floatlist;

    [MinValue("MinValueMethod")]
    public float floatCallbackMethod;

    [MinValue("MinValueProp")]
    public float floatCallbackProp;

    [MinValue("MinValueMethodArgs", 100000)]
    public float floatCallbackMethodArgs;

    [MinValue("MinValueErrorMethod")]
    public float floatCallbackMethodError;

    [MinValue("MinValueErrorProperty")]
    public float floatCallbackPropertyError;

    public float MinValueMethod()
    {
        return 100;
    }

    public float MinValueProp => 1000;

    public bool MinValueErrorMethod()
    {
        return true;
    }

    public bool MinValueErrorProperty => true;

    public float MinValueMethodArgs(int a)
    {
        return a / 10f;
    }
}