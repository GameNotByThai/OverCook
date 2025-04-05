using UnityEngine;

public class PlayerAbstract : GameBehaviour
{
    [Header("Player Abstract")]
    [SerializeField] private PlayerCtrl playerCtrl;
    public PlayerCtrl PlayerCtrl => playerCtrl;


    protected override void LoadComponent()
    {
        base.LoadComponent();
        this.LoadPlayerCtrl();
    }


    private void LoadPlayerCtrl()
    {
        if (this.playerCtrl != null) return;

        this.playerCtrl = transform.parent.GetComponent<PlayerCtrl>();
        Debug.LogWarning(transform.name + ": LoadPlayerCtrl", gameObject);
    }
}
