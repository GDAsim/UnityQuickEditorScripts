using UnityEngine;

public class AnimatorParamExample : MonoBehaviour
{
    public Animator SomeAnimator;

    [AnimatorParam("SomeAnimator")]
    public int hash0;

    [AnimatorParam("SomeAnimator")]
    public string name0;
}