using UnityEngine;

public class LineExample : MonoBehaviour
{
    [Line(1, ColorUtilities.ColorEnum.Red)]
    [Line(2, ColorUtilities.ColorEnum.DarkGreen)]
    [Line(3, ColorUtilities.ColorEnum.DarkKhaki, "DarkKhaki", 10)]
    [Line(4, ColorUtilities.ColorEnum.CornflowerBlue, "CornflowerBlue", 10)]
    [Line(5, ColorUtilities.ColorEnum.Chocolate, "Chocolate",10, isCenter: true)]
    public int Line1;
}