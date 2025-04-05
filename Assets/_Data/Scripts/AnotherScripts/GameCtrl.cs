using UnityEngine;

public class GameCtrl : GameBehaviour
{
    private static GameCtrl instance;
    public static GameCtrl Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.LogError("Only 1 GameCtrl allow exsit");
        }

        instance = this;
    }

    [SerializeField] private PlayerCtrl playerCtrl;
    public PlayerCtrl PlayerCtrl => playerCtrl;

    [SerializeField] private DeliveryManager deliveryManager;
    public DeliveryManager DeliveryManager => deliveryManager;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPlayerCtrl();
        this.LoadDeliveryCtrl();
    }

    private void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;

        this.playerCtrl = GameObject.FindFirstObjectByType<PlayerCtrl>();
        Debug.LogWarning(transform.name + ": LoadPlayerCtrl", gameObject);
    }

    private void LoadDeliveryCtrl()
    {
        if (this.deliveryManager != null) return;

        this.deliveryManager = GameObject.FindFirstObjectByType<DeliveryManager>();
        Debug.LogWarning(transform.name + ": LoadDeliveryCtrl", gameObject);
    }
}
