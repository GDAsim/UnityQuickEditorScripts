using UnityEngine;

public class MaxValueExample : MonoBehaviour
{
    [MaxValue(100)]
    public float floatvar;

    [MaxValue(100)]
    public Vector4 Vector4var;

    [MaxValue(100)]
    public Vector3Int Vector3Intvar;

    [MaxValue("MaxValueMethod")]
    public float floatCallbackMethod;

    [MaxValue("MaxValueProp")]
    public float floatCallbackProp;

    [MaxValue("MaxValueMethodArgs", 100000)]
    public float floatCallbackMethodArgs;

    [MaxValue("MaxValueErrorMethod")]
    public float floatCallbackMethodError;

    [MaxValue("MaxValueErrorProperty")]
    public float floatCallbackPropertyError;

    public float MaxValueMethod()
    {
        return 100;
    }

    public float MaxValueProp => 1000;

    public bool MaxValueErrorMethod()
    {
        return true;
    }

    public bool MaxValueErrorProperty => true;

    public float MaxValueMethodArgs(int a)
    {
        return a / 10f;
    }
}
