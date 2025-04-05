using UnityEngine;

public class PlayerCtrl : GameBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    public PlayerMovement PlayerMovement => playerMovement;

    [SerializeField] private PlayerLookAt playerLookAt;
    public PlayerLookAt PlayerLookAt => playerLookAt;

    [SerializeField] private PlayerInteract playerInteract;
    public PlayerInteract PlayerInteract => playerInteract;

    [SerializeField] private Player player;
    public Player Player => player;

    [SerializeField] private Transform holdObjPoint;
    public Transform HoldObjPoint => holdObjPoint;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPlayerMovement();
        this.LoadPlayerLookAt();
        this.LoadPlayerInteract();
        this.LoadPlayer();
        this.LoadHoldObjPoint();
    }

    private void LoadPlayerMovement()
    {
        if (this.playerMovement != null) return;

        this.playerMovement = transform.GetComponentInChildren<PlayerMovement>();
        Debug.LogWarning(transform.name + ": LoadPlayerMovement", gameObject);
    }

    private void LoadPlayerLookAt()
    {
        if (this.playerLookAt != null) return;

        this.playerLookAt = transform.GetComponentInChildren<PlayerLookAt>();
        Debug.LogWarning(transform.name + ": LoadPlayerLookAt", gameObject);
    }

    private void LoadPlayerInteract()
    {
        if (this.playerInteract != null) return;

        this.playerInteract = transform.GetComponentInChildren<PlayerInteract>();
        Debug.LogWarning(transform.name + ": LoadPlayerInteract", gameObject);
    }

    private void LoadPlayer()
    {
        if (this.player != null) return;

        this.player = transform.GetComponent<Player>();
        Debug.LogWarning(transform.name + ": LoadPlayer", gameObject);
    }

    private void LoadHoldObjPoint()
    {
        if (this.holdObjPoint != null) return;

        this.holdObjPoint = transform.Find("HoldObjPoint");
        Debug.LogWarning(transform.name + ": LoadHoldObjPoint", gameObject);
    }
}
