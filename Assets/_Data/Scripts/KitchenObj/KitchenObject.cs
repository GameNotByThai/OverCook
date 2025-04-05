using UnityEngine;

public class KitchenObject : GameBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    public KitchenObjectSO KitchenObjectSO => kitchenObjectSO;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadKitchenObjectSO();
    }

    private void LoadKitchenObjectSO()
    {
        if (this.kitchenObjectSO != null) return;

        string kitchenObjSOPath = "ScriptableObject/KitchenObject/" + transform.name;
        this.kitchenObjectSO = (KitchenObjectSO)Resources.Load(kitchenObjSOPath);
        Debug.LogWarning(transform.name + ": LoadKitchenObjectSO", gameObject);
    }

    public void SetKitchenObjParent(Transform parent)
    {
        transform.SetParent(parent);
        transform.localPosition = Vector3.zero;
    }
}
