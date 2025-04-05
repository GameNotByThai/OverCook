using System;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : ClearCounter, IHasProgress
{
    [SerializeField] private List<FryingRecipeSO> fryingRecipeSOs = new List<FryingRecipeSO>();
    public List<FryingRecipeSO> ListFryingRecipeSO => fryingRecipeSOs;

    [SerializeField] private List<BurningRecipeSO> burningRecipeSOs = new List<BurningRecipeSO>();
    public List<BurningRecipeSO> ListBurningRecipeSO => burningRecipeSOs;

    [SerializeField] private Transform stoveOnVisual;
    public Transform StoveOffVisual => stoveOnVisual;

    [SerializeField] private Transform sizzlingPartiles;
    public Transform SizzlingPartiles => sizzlingPartiles;

    private IStoveState currentState;

    public event EventHandler<IHasProgress.OnHasProgressChangedEventArgs> OnHasProgressChanged;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadListFryingRecipeSO();
        this.LoadListBurningRecipeSO();
        this.LoadStoveOnVisual();
        this.LoadSizzlingParticles();
    }

    private void LoadListFryingRecipeSO()
    {
        if (this.fryingRecipeSOs.Count > 0) return;

        string path = "ScriptableObject/FryingRecipe";
        FryingRecipeSO[] allFryingRecipeSO = Resources.LoadAll<FryingRecipeSO>(path);

        for (int i = 0; i < allFryingRecipeSO.Length; i++)
        {
            fryingRecipeSOs.Add(allFryingRecipeSO[i]);
        }

        Debug.LogWarning(transform.name + ": LoadListFryingRecipeSO", gameObject);
    }

    private void LoadListBurningRecipeSO()
    {
        if (this.burningRecipeSOs.Count > 0) return;

        string path = "ScriptableObject/BurningRecipe";
        BurningRecipeSO[] allBurningRecipeSO = Resources.LoadAll<BurningRecipeSO>(path);

        for (int i = 0; i < allBurningRecipeSO.Length; i++)
        {
            burningRecipeSOs.Add(allBurningRecipeSO[i]);
        }

        Debug.LogWarning(transform.name + ": LoadListBurningRecipeSO", gameObject);
    }

    private void LoadStoveOnVisual()
    {
        if (this.stoveOnVisual != null) return;

        this.stoveOnVisual = transform.Find("Model").Find("StoveOnVisual");
        Debug.LogWarning(transform.name + ": LoadStoveOnVisual", gameObject);
    }

    private void LoadSizzlingParticles()
    {
        if (this.sizzlingPartiles != null) return;

        this.sizzlingPartiles = transform.Find("Model").Find("SizzlingParticles");
        Debug.LogWarning(transform.name + ": LoadSizzlingParticles", gameObject);
    }

    protected override void Start()
    {
        base.Start();
        ChangeState(new IdleState(this));
    }

    public override void Interact(PlayerCtrl playerCtrl)
    {
        //Player player = GameCtrl.Instance.PlayerCtrl.Player;
        if (this.HasObjInCounter())
        {
            if (playerCtrl.Player.HasObjInHand())
            {
                if (playerCtrl.HoldObjPoint.GetChild(0).TryGetComponent<PlateKitchenObject>(out PlateKitchenObject plate))
                {
                    if (!plate.TryAddToPlate(kitchenObject)) return;

                    this.kitchenObject.SetKitchenObjParent(KitchenObjectSpawner.Instance.GetHolderSpawner());
                    KitchenObjectSpawner.Instance.Despawn(kitchenObject.transform);
                    this.kitchenObject = null;
                    this.ChangeState(new IdleState(this));
                }

                if (kitchenObject is PlateKitchenObject)
                {
                    PlateKitchenObject plateKitchenObject = kitchenObject as PlateKitchenObject;
                    KitchenObject kitchenObjInHand = playerCtrl.HoldObjPoint.GetChild(0).GetComponent<KitchenObject>();
                    if (!plateKitchenObject.TryAddToPlate(kitchenObjInHand)) return;

                    kitchenObjInHand.SetKitchenObjParent(KitchenObjectSpawner.Instance.GetHolderSpawner());
                    KitchenObjectSpawner.Instance.Despawn(kitchenObjInHand.transform);
                }
            }
            else
            {
                this.TakeKitchenObj(playerCtrl);
                this.ChangeState(new IdleState(this));

            }
        }
        else
        {
            if (!playerCtrl.Player.HasObjInHand()) return;
            if (!CheckKOCanFry(playerCtrl.HoldObjPoint.GetChild(0))) return;

            this.DropKitchenObj(playerCtrl);
            this.ChangeState(new FryingState(this));

        }
    }

    private void Update()
    {
        //Debug.Log(currentState.ToString());
        currentState.Execute();
    }

    private bool CheckKOCanFry(Transform objInPlayerHand)
    {
        KitchenObject kitchenObjectInPlayer = objInPlayerHand.GetComponent<KitchenObject>();
        foreach (FryingRecipeSO fryingRecipe in fryingRecipeSOs)
        {
            if (kitchenObjectInPlayer.KitchenObjectSO.objName == fryingRecipe.beforeFrying.objName) return true;
        }
        return false;
    }

    public void ChangeState(IStoveState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState.Enter();
    }

    public void DespawnCurrentKO()
    {
        kitchenObject.SetKitchenObjParent(KitchenObjectSpawner.Instance.GetHolderSpawner());
        KitchenObjectSpawner.Instance.Despawn(kitchenObject.transform);
    }

    public void SpawnFriedKO(FryingRecipeSO fryingRecipeSO)
    {
        string afterFryingKOName = fryingRecipeSO.afterFrying.objName;
        Transform afterFrying = KitchenObjectSpawner.Instance.Spawn(afterFryingKOName, transform.position, Quaternion.identity);

        this.kitchenObject = afterFrying.GetComponent<KitchenObject>();
    }

    public void SpawnBurnedKO(BurningRecipeSO burningRecipeSO)
    {
        string afterFriedKOName = burningRecipeSO.afterBurning.objName;
        Transform afterFried = KitchenObjectSpawner.Instance.Spawn(afterFriedKOName, transform.position, Quaternion.identity);

        this.kitchenObject = afterFried.GetComponent<KitchenObject>();
    }

    public void RunProgressUI(float progress)
    {
        this.OnHasProgressChanged?.Invoke(this, new IHasProgress.OnHasProgressChangedEventArgs
        {
            progressNormalized = progress,
        });
    }
}
