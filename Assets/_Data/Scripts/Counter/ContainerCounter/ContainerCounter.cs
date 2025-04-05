using UnityEngine;
public class ContainerCounter : BaseCounter
{
    [Header("Countainer Counter")]
    [SerializeField] private ContainerCounterAnimator containerAnimator;
    [SerializeField] private Sprite kitchenObjSprite;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadContainerCounterAnimator();
        this.LoadSprite();
    }

    private void LoadContainerCounterAnimator()
    {
        if (this.containerAnimator != null) return;

        this.containerAnimator = transform.GetComponentInChildren<ContainerCounterAnimator>();
        Debug.LogWarning(transform.name + ": LoadContainerCounterAnimator", gameObject);
    }

    private void LoadSprite()
    {
        if (this.kitchenObjSprite != null) return;

        this.kitchenObjSprite = transform.GetChild(0).GetChild(2).GetComponentInChildren<SpriteRenderer>().sprite;
        Debug.LogWarning(transform.name + ": LoadSprite", gameObject);
    }

    public override void Interact(PlayerCtrl playerCtrl)
    {
        //Player player = GameCtrl.Instance.PlayerCtrl.Player;
        if (playerCtrl.Player.HasObjInHand()) return;

        containerAnimator.OpenCloseAnimation();
        Transform playerHoldPoint = playerCtrl.HoldObjPoint;
        Transform kitchenPrefab = KitchenObjectSpawner.Instance.Spawn(kitchenObjSprite.name, transform.position, Quaternion.identity);
        KitchenObject kitchenObject = kitchenPrefab.GetComponent<KitchenObject>();
        kitchenObject.SetKitchenObjParent(playerHoldPoint);
    }
}
