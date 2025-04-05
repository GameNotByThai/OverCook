public class TrashCounter : BaseCounter
{
    public override void Interact(PlayerCtrl playerCtrl)
    {
        //Player player = GameCtrl.Instance.PlayerCtrl.Player;
        if (!playerCtrl.Player.HasObjInHand()) return;

        KitchenObject objInHand = playerCtrl.HoldObjPoint.GetChild(0).GetComponent<KitchenObject>();
        if (objInHand is PlateKitchenObject)
        {
            PlateKitchenObject plate = objInHand as PlateKitchenObject;
            plate.SetupNewPlate();
        }
        objInHand.SetKitchenObjParent(KitchenObjectSpawner.Instance.GetHolderSpawner());
        KitchenObjectSpawner.Instance.Despawn(objInHand.transform);
    }
}
