using System.Collections.Generic;
using UnityEngine;

public class PlateIconUI : GameBehaviour
{
    [SerializeField] private PlateKitchenObject plate;

    private void Start()
    {
        plate.OnKitchenObjInPlateChanged += Plate_OnKitchenObjInPlateChanged;
    }

    private void Plate_OnKitchenObjInPlateChanged(object sender, PlateKitchenObject.OnKitchenObjInPlateChangedEventArgs e)
    {
        UpdateVisual(e.kitchenObject);
    }

    private void UpdateVisual(KitchenObject kitchenObject)
    {
        this.DespawnOldIcons();
        this.SpawnNewIcons();
    }

    private void DespawnOldIcons()
    {
        if (transform.childCount == 0) return;

        List<Transform> children = new List<Transform>();
        foreach (Transform child in transform)
        {
            children.Add(child);
        }

        foreach (Transform child in children)
        {
            PlateIconSingleUI icon = child.GetComponent<PlateIconSingleUI>();
            PlateIconSpawner.Instance.Despawn(child);
            icon.SetKitchenObjParent(PlateIconSpawner.Instance.GetHolderSpawner());
        }
    }

    private void SpawnNewIcons()
    {
        //int index = 0;
        foreach (KitchenObject kitchenObject in plate.KitchenObjectList)
        {
            //index++;
            //Debug.Log("Spawn: " + index + kitchenObject.KitchenObjectSO.objName);
            Transform newIcon = PlateIconSpawner.Instance.Spawn(PlateIconSpawner.IconTemplateName, transform.position, Quaternion.identity);
            PlateIconSingleUI icon = newIcon.GetComponent<PlateIconSingleUI>();
            icon.SetKitchenObjParent(transform);
            icon.SetIconImage(kitchenObject);
        }
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPlateKitchenObject();
    }

    private void LoadPlateKitchenObject()
    {
        if (this.plate != null) return;

        this.plate = transform.parent.GetComponent<PlateKitchenObject>();
        Debug.LogWarning(transform.name + ": LoadPlateKitchenObject", gameObject);
    }
}
