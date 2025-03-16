using UnityEngine;

public partial class HighlightExample : MonoBehaviour
{
    [Highlight(HighlightAttribute.HighlightColor.Red)]
    [SerializeField] int NoValidationMethod;

    [Highlight(HighlightAttribute.HighlightColor.Green, "isEven", 1)]
    [SerializeField] int ValidationMethodTrue;

    [Highlight(HighlightAttribute.HighlightColor.Red, "isEven", 2)]
    [SerializeField] int ValidationMethodFalse;
}


#if UNITY_EDITOR
public partial class HighlightExample
{
    bool isEven(int a)
    {
        return a % 2 == 0;
    }
}
#endif