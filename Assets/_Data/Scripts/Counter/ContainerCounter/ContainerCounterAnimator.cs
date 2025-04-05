using UnityEngine;

public class ContainerCounterAnimator : GameBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";

    private Animator animator;

    protected override void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenCloseAnimation()
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
