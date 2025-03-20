using UnityEngine;

public class MinMaxSliderExample : MonoBehaviour
{
    [MinMaxSlider(0f, 10f)]
    public Range rangeMinMax = new(4f, 5f);

    public Range rangeMinMax2 = new(1f, 11f);

    [MinMaxSlider(0f, 10f)]
    public float floatVar = 123f;
}