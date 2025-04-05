using UnityEngine;

public class PlayerLookAt : PlayerAbstract
{
    [SerializeField] private float rotateSpeed = 10.0f;

    private void Update()
    {
        this.RotateToMoveDir();
    }

    public void RotateToMoveDir()
    {
        Vector3 moveDir = this.PlayerCtrl.Player.GetPlayerDiraction();
        if (moveDir == Vector3.zero) return;
        transform.parent.forward = Vector3.Slerp(transform.parent.forward, moveDir, rotateSpeed * Time.deltaTime);
    }
}
