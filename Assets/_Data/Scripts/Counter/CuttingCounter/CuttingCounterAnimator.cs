using UnityEngine;

public class CuttingCounterAnimator : GameBehaviour
{
    private const string CUT = "Cut";

    private Animator animator;

    protected override void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void CutAnimation()
    {
        animator.SetTrigger(CUT);
    }
}
