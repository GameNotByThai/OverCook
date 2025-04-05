public class BurnedState : IStoveState
{
    private StoveCounter counterControl;

    public BurnedState(StoveCounter stoveCounter)
    {
        this.counterControl = stoveCounter;
    }

    public void Enter()
    {
        counterControl.SizzlingPartiles.gameObject.SetActive(false);
        counterControl.StoveOffVisual.gameObject.SetActive(false);
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }
}