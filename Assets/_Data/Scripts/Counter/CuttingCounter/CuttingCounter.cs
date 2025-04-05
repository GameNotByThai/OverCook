using System;
using System.Collections.Generic;
using UnityEngine;
public class CuttingCounter : ClearCounter, IHasProgress
{
    [Header("Cutting Counter")]
    [SerializeField] private CuttingCounterAnimator cuttingAnimator;
    [SerializeField] private List<CuttingRecipeSO> cuttingRecipeSOs = new List<CuttingRecipeSO>();
    [SerializeField] private int cuttingProgress = 0;

    public event EventHandler<IHasProgress.OnHasProgressChangedEventArgs> OnHasProgressChanged;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadCuttingAnimation();
        this.LoadListCuttingRecipeSO();
    }

    private void LoadCuttingAnimation()
    {
        if (this.cuttingAnimator != null) return;

        this.cuttingAnimator = GetComponentInChildren<CuttingCounterAnimator>();
        Debug.LogWarning(transform.name + ": LoadCuttingAnimation", gameObject);
    }

    private void LoadListCuttingRecipeSO()
    {
        if (this.cuttingRecipeSOs.Count > 0) return;

        string path = "ScriptableObject/CuttingRecipe";
        CuttingRecipeSO[] allCuttingRecipeSO = Resources.LoadAll<CuttingRecipeSO>(path);

        for (int i = 0; i < allCuttingRecipeSO.Length; i++)
        {
            cuttingRecipeSOs.Add(allCuttingRecipeSO[i]);
        }
        Debug.LogWarning(transform.name + "Load: LoadListCuttingRecipeSO", gameObject);
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
                    cuttingProgress = 0;
                    this.OnHasProgressChanged?.Invoke(this, new IHasProgress.OnHasProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress
                    });
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
                cuttingProgress = 0;
                this.OnHasProgressChanged?.Invoke(this, new IHasProgress.OnHasProgressChangedEventArgs
                {
                    progressNormalized = (float)cuttingProgress
                });
            }
        }
        else
        {
            if (!playerCtrl.Player.HasObjInHand()) return;

            this.DropKitchenObj(playerCtrl);
        }
    }

    public override void InteractAlternate(PlayerCtrl playerCtrl)
    {
        //Player player = GameCtrl.Instance.PlayerCtrl.Player;

        if (!this.HasObjInCounter()) return;

        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOs)
        {
            if (cuttingRecipeSO.beforeCut.objName != this.kitchenObject.KitchenObjectSO.objName) continue;

            this.Cut(cuttingRecipeSO);
            if (cuttingProgress == cuttingRecipeSO.cuttingDone)
            {
                this.DespawnCurrentKO();
                this.SpawnCuttedKO(cuttingRecipeSO);
                kitchenObject.SetKitchenObjParent(this.TopPoint);
                cuttingProgress = 0;
                this.OnHasProgressChanged?.Invoke(this, new IHasProgress.OnHasProgressChangedEventArgs
                {
                    progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingDone
                });
            }
        }

    }

    private void Cut(CuttingRecipeSO cuttingRecipeSO)
    {
        cuttingProgress++;
        this.OnHasProgressChanged?.Invoke(this, new IHasProgress.OnHasProgressChangedEventArgs
        {
            progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingDone
        });
        cuttingAnimator.CutAnimation();
    }

    private void DespawnCurrentKO()
    {
        kitchenObject.SetKitchenObjParent(KitchenObjectSpawner.Instance.GetHolderSpawner());
        KitchenObjectSpawner.Instance.Despawn(kitchenObject.transform);
    }

    private void SpawnCuttedKO(CuttingRecipeSO cuttingRecipeSO)
    {
        string afterCutKOName = cuttingRecipeSO.afterCut.objName;
        Transform afterCut = KitchenObjectSpawner.Instance.Spawn(afterCutKOName, transform.position, Quaternion.identity);

        this.kitchenObject = afterCut.GetComponent<KitchenObject>();
    }
}
