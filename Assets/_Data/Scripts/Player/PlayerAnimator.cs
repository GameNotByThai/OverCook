using UnityEngine;

public class PlayerAnimator : PlayerAbstract
{
    private const string IS_MOVING = "IsMoving";

    private Animator animator;

    protected override void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        this.CheckIsMoving();
    }

    private void CheckIsMoving()
    {
        animator.SetBool(IS_MOVING, this.PlayerCtrl.PlayerMovement.IsMoving);
    }
}
