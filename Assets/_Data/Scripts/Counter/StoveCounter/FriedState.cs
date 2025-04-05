using UnityEngine;
public class FriedState : IStoveState
{
    private StoveCounter counterControl;
    private BurningRecipeSO burningRecipeSO;
    private float burningTimer = 0;
    private float timer = 0;

    public FriedState(StoveCounter stoveCounter)
    {
        this.counterControl = stoveCounter;
    }

    public void Enter()
    {
        foreach (BurningRecipeSO burningRecipe in counterControl.ListBurningRecipeSO)
        {
            if (burningRecipe.beforeBurning.objName == counterControl.KitchenObject.KitchenObjectSO.objName)
            {
                burningTimer = burningRecipe.burningTime;
                burningRecipeSO = burningRecipe;
            }
        }

        counterControl.SizzlingPartiles.gameObject.SetActive(true);
        counterControl.StoveOffVisual.gameObject.SetActive(true);
    }

    public void Execute()
    {
        timer += Time.deltaTime;
        counterControl.RunProgressUI(timer / burningTimer);
        if (timer < burningTimer) return;
        timer = 0;

        counterControl.DespawnCurrentKO();
        counterControl.SpawnBurnedKO(burningRecipeSO);
        counterControl.KitchenObject.SetKitchenObjParent(counterControl.TopPoint);
        counterControl.ChangeState(new BurnedState(counterControl));
    }

    public void Exit()
    {

    }
}