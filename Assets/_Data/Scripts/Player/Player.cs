using UnityEngine;

public class Player : GameBehaviour
{
    [SerializeField] private float playerHeight = 2f;
    public float PlayerHeight => playerHeight;

    [SerializeField] private float playerSize = 0.7f;
    public float PlayerSize => playerSize;

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

        this.playerCtrl = transform.GetComponent<PlayerCtrl>();
        Debug.LogWarning(transform.name + ": LoadPlayerCtrl", gameObject);
    }

    public Vector3 GetPlayerDiraction()
    {
        Vector2 moveInputVector = InputManager.Instance.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(moveInputVector.x, 0, moveInputVector.y);
        return moveDir;
    }

    public bool HasObjInHand()
    {
        return this.PlayerCtrl.HoldObjPoint.childCount > 0;
    }
}
