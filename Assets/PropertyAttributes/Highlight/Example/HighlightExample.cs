using UnityEngine;

public class HighlightExample : MonoBehaviour
{
    [Highlight(HighlightAttribute.HighlightColor.Red)]
    [SerializeField] int NoValidationMethod;

    [Highlight(HighlightAttribute.HighlightColor.Green, "isEven", 2)]
    [SerializeField] int ValidationMethodTrue;

    [Highlight(HighlightAttribute.HighlightColor.Red, "isEven", 2)]
    [SerializeField] int ValidationMethodFalse;

    [Highlight(HighlightAttribute.HighlightColor.Red, "ErrorMethod")]
    [SerializeField] int ValidationErrorMethod;

    [Highlight(HighlightAttribute.HighlightColor.Red, "ErrorMethod2", 1, 2)]
    [SerializeField] int ValidationErrorMethod2;

    bool isEven(int a)
    {
        return a % 2 == 0;
    }

    string ErrorMethod()
    {
        return "error";
    }

    bool ErrorMethod2(int a)
    {
        return a % 2 == 0;
    }
}

