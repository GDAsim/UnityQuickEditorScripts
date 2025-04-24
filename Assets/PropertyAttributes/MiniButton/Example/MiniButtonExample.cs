using UnityEngine;

public class MiniButtonExample : MonoBehaviour
{
    public int resourcesFolder;

    [MiniButton("Click Me!", nameof(ClickMeFunc))] public int resourcesFolder2;

    [MiniButton(nameof(ClickMeFunc))] public int resourcesFolder3;

    public void ClickMeFunc()
    {
        print("Hello World");
    }
}