using System;
using System.Collections.Generic;
using UnityEngine;
public class PlateKitchenObject : KitchenObject
{
    [SerializeField] private List<KitchenObject> kitchenObjectList = new List<KitchenObject>();
    public List<KitchenObject> KitchenObjectList => kitchenObjectList;

    [SerializeField] private PlateComplete plateComplete;

    public event EventHandler<OnKitchenObjInPlateChangedEventArgs> OnKitchenObjInPlateChanged;

    public class OnKitchenObjInPlateChangedEventArgs
    {
        public KitchenObject kitchenObject;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPlateComplete();
    }

    private void LoadPlateComplete()
    {
        if (this.plateComplete != null) return;

        this.plateComplete = transform.GetComponentInChildren<PlateComplete>();
        Debug.LogWarning(transform.name + ": LoadPlateComplete", gameObject);
    }



    public bool TryAddToPlate(KitchenObject newKitchenObject)
    {
        //Debug.Log(newKitchenObject.KitchenObjectSO.objName);
        foreach (KitchenObject validKO in plateComplete.ValidKitchenObjectList)
        {
            //Debug.Log(validKO.KitchenObjectSO.objName);
            if (validKO.KitchenObjectSO.objName != newKitchenObject.KitchenObjectSO.objName) continue;

            if (kitchenObjectList.Contains(newKitchenObject)) return false;

            kitchenObjectList.Add(newKitchenObject);
            this.OnKitchenObjInPlateChanged?.Invoke(this, new OnKitchenObjInPlateChangedEventArgs
            {
                kitchenObject = newKitchenObject,
            });
            return true;
        }
        return false;
    }

    public void SetupNewPlate()
    {
        kitchenObjectList.Clear();

        foreach (Transform child in plateComplete.transform)
        {
            child.gameObject.SetActive(false);
        }

        this.OnKitchenObjInPlateChanged?.Invoke(this, new OnKitchenObjInPlateChangedEventArgs
        {
            kitchenObject = null,
        });
    }
}
