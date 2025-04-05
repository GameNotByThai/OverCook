using UnityEngine;

public class ClearCounter : BaseCounter
{
    [Header("Clear Counter")]
    [SerializeField] private Transform topPoint;
    public Transform TopPoint => topPoint;

    [SerializeField] protected KitchenObject kitchenObject;

    public KitchenObject KitchenObject => kitchenObject;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadTopPoint();
    }

    private void LoadTopPoint()
    {
        if (this.topPoint != null) return;

        this.topPoint = transform.Find("TopPoint");
        Debug.LogWarning(transform.name + ": LoadTopPoint", gameObject);
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
            }
        }
        else
        {
            if (!playerCtrl.Player.HasObjInHand()) return;

            this.DropKitchenObj(playerCtrl);
        }
    }

    protected bool HasObjInCounter()
    {
        return topPoint.childCount > 0;
    }

    protected void DropKitchenObj(PlayerCtrl playerCtrl)
    {
        kitchenObject = playerCtrl.HoldObjPoint.GetComponentInChildren<KitchenObject>();
        kitchenObject.SetKitchenObjParent(this.TopPoint);
    }

    protected void TakeKitchenObj(PlayerCtrl playerCtrl)
    {
        Transform playerHoldPoint = playerCtrl.HoldObjPoint;
        kitchenObject.SetKitchenObjParent(playerHoldPoint);
        kitchenObject = null;
    }
}
