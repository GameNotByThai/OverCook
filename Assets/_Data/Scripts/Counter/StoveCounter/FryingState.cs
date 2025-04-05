using UnityEngine;

public class FryingState : IStoveState
{
    private StoveCounter counterControl;
    private FryingRecipeSO fryingRecipeSO;
    private float fryingTime = 0;
    private float timer = 0;

    public FryingState(StoveCounter stoveCounter)
    {
        this.counterControl = stoveCounter;
    }

    public void Enter()
    {
        foreach (FryingRecipeSO fryingRecipe in counterControl.ListFryingRecipeSO)
        {
            if (fryingRecipe.beforeFrying.objName == counterControl.KitchenObject.KitchenObjectSO.objName)
            {
                fryingTime = fryingRecipe.fryingTime;
                fryingRecipeSO = fryingRecipe;
            }
        }
        counterControl.SizzlingPartiles.gameObject.SetActive(true);
        counterControl.StoveOffVisual.gameObject.SetActive(true);
    }

    public void Execute()
    {
        timer += Time.deltaTime;
        counterControl.RunProgressUI(timer / fryingTime);
        if (timer < fryingTime) return;
        timer = 0;

        counterControl.DespawnCurrentKO();
        counterControl.SpawnFriedKO(fryingRecipeSO);
        counterControl.KitchenObject.SetKitchenObjParent(counterControl.TopPoint);
        counterControl.ChangeState(new FriedState(counterControl));
    }

    public void Exit()
    {
    }
}