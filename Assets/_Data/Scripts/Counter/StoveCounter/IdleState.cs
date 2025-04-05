public class IdleState : IStoveState
{
    private StoveCounter counterControl;

    public IdleState(StoveCounter stoveCounter)
    {
        this.counterControl = stoveCounter;
    }

    public void Enter()
    {
        counterControl.SizzlingPartiles.gameObject.SetActive(false);
        counterControl.StoveOffVisual.gameObject.SetActive(false);
        counterControl.RunProgressUI(0);
    }

    public void Execute()
    {

    }

    public void Exit()
    {
    }
}