using UnityEngine;

public class HighlightExample : MonoBehaviour
{
    [Highlight(ColorUtilities.ColorEnum.Red)]
    [SerializeField] int NoValidationMethod;

    [Highlight(ColorUtilities.ColorEnum.Green, "isEven", 2)]
    [SerializeField] int ValidationMethodTrue;

    [Highlight(ColorUtilities.ColorEnum.Red, "isEven", 1)]
    [SerializeField] int ValidationMethodFalse;

    [Highlight(ColorUtilities.ColorEnum.Red, "ErrorMethod")]
    [SerializeField] int ValidationErrorMethod;

    [Highlight(ColorUtilities.ColorEnum.Red, "ErrorMethod2", 1, 2)]
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

