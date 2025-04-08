using UnityEngine;

public class AnimatorParamExample : MonoBehaviour
{
    public Animator SomeAnimator;

    [AnimatorParam(nameof(SomeAnimator))]
    public int AnimatorParamHash;

    [AnimatorParam(nameof(SomeAnimator))]
    public string AnimatorParamName;
}