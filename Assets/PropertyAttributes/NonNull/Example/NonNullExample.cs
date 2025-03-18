using UnityEngine;

public class NonNull : MonoBehaviour
{
    [NonNull] public Camera go1;

    [NonNull] public GameObject[] go2;

    [NonNull] public int integer1;

    [NonNull] public int[] integer2;
}