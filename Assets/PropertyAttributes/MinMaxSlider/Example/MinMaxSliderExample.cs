using UnityEngine;

public class MinMaxSliderExample : MonoBehaviour
{
    [MinMaxSlider(0f, 10f)] 
    [SerializeField] Range rangeMinMax = new(4f, 5f);

    [SerializeField] Range rangeMinMax2 = new(1f, 11f);

    [MinMaxSlider(0f, 10f)]
    //[SerializeField] float floatVar = 123f;
}