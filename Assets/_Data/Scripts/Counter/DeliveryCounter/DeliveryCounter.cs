public class DeliveryCounter : BaseCounter
{
    public override void Interact(PlayerCtrl playerCtrl)
    {
        if (!playerCtrl.Player.HasObjInHand()) return;

        if (!playerCtrl.HoldObjPoint.GetChild(0).TryGetComponent<PlateKitchenObject>(out PlateKitchenObject plateKitchenObject)) return;

        GameCtrl.Instance.DeliveryManager.DeliverRecipe(plateKitchenObject);
        plateKitchenObject.SetupNewPlate();
        plateKitchenObject.SetKitchenObjParent(KitchenObjectSpawner.Instance.GetHolderSpawner());
        KitchenObjectSpawner.Instance.Despawn(plateKitchenObject.transform);
    }
}
