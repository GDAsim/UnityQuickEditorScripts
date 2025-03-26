using UnityEngine;
using static ColorUtilities;

public class CurveRangeExample : MonoBehaviour
{
    [CurveRange]
    public AnimationCurve Curve1;

    [CurveRange(ColorEnum.Aqua)]
    public AnimationCurve Curve2;

    [CurveRange(-1, -1, 1, 1)]
    public AnimationCurve Curve3;

    [CurveRange(-2, -2, 1, 1, ColorEnum.Aqua)]
    public AnimationCurve Curve4;
}