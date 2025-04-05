using System.Collections.Generic;
using UnityEngine;

public class PlateComplete : GameBehaviour
{
    [SerializeField] private List<KitchenObject> validKitchenObjectList = new List<KitchenObject>();
    public List<KitchenObject> ValidKitchenObjectList => validKitchenObjectList;

    [SerializeField] private PlateKitchenObject plate;

    private void Start()
    {
        this.plate.OnKitchenObjInPlateChanged += Plate_OnKitchenObjInPlateChanged;
    }

    private void Plate_OnKitchenObjInPlateChanged(object sender, PlateKitchenObject.OnKitchenObjInPlateChangedEventArgs e)
    {
        if (e.kitchenObject == null) return;

        foreach (KitchenObject validKO in validKitchenObjectList)
        {
            if (validKO.KitchenObjectSO.objName != e.kitchenObject.KitchenObjectSO.objName) continue;

            validKO.gameObject.SetActive(true);
        }
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPlateKitchenObject();
        this.LoadValidKitchenObjectList();
    }

    private void LoadValidKitchenObjectList()
    {
        if (this.validKitchenObjectList.Count > 0) return;

        foreach (Transform kitchenObjTrans in transform)
        {
            KitchenObject kitchenObj = kitchenObjTrans.GetComponent<KitchenObject>();
            validKitchenObjectList.Add(kitchenObj);
            kitchenObj.gameObject.SetActive(false);
        }

        Debug.LogWarning(transform.name + ": LoadValidKitchenObjectList", gameObject);
    }

    private void LoadPlateKitchenObject()
    {
        if (this.plate != null) return;

        this.plate = transform.parent.GetComponent<PlateKitchenObject>();
        Debug.LogWarning(transform.name + ": LoadPlateKitchenObject", gameObject);
    }


}
