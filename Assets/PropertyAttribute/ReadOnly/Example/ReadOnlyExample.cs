using UnityEngine;

public class ReadOnlyExample : MonoBehaviour
{
    [ReadOnly]
    [SerializeField] float readOnly;

    [ReadOnlyWhenPlaying]
    [SerializeField] float readOnlyWhenPlaying;
}