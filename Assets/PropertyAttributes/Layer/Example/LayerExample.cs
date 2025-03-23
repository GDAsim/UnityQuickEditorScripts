using UnityEngine;

[ExecuteInEditMode]
public class LayerExample : MonoBehaviour
{
    [Layer] public int LayerInt;

    [Layer] public string LayerString;


    void Update()
    {
        print("LayerInt: " + LayerInt);
        print("LayerString " + LayerString);
    }
}