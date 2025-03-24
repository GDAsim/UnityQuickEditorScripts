using UnityEngine;

public class LineExample : MonoBehaviour
{
    [Line(1, ColorUtilities.ColorEnum.Red)]
    [Line(2, ColorUtilities.ColorEnum.DarkGreen)]
    [Line(3, ColorUtilities.ColorEnum.DarkKhaki, "DarkKhaki")]
    [Line(4, ColorUtilities.ColorEnum.CornflowerBlue, "CornflowerBlue")]
    public int Line1;
}