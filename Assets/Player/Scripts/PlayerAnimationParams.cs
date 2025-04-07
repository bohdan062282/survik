using UnityEngine;

public static class PlayerAnimationParams
{
    public static readonly int isMove = Animator.StringToHash("IsMove");
    public static readonly int isFalling = Animator.StringToHash("IsFalling");

    public static readonly int forwardBackwardValue = Animator.StringToHash("ForwardBackward");
    public static readonly int leftRightValue = Animator.StringToHash("LeftCenterRight");
    public static readonly int sprintValue = Animator.StringToHash("SprintValue");
}
