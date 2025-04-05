using UnityEngine;

public class PlayerMovement : PlayerAbstract
{
    [SerializeField] private float moveSpeed = 7.0f;
    [SerializeField] private bool isMoving = false;
    public bool IsMoving => isMoving;

    private void Update()
    {
        this.HandleMovement();
    }

    private void HandleMovement()
    {
        Vector3 moveDir = this.PlayerCtrl.Player.GetPlayerDiraction();

        isMoving = moveDir != Vector3.zero;

        if (!this.CanMove(moveDir)) moveDir = this.TryMoveDirX(moveDir);
        transform.parent.position += moveDir * moveSpeed * Time.deltaTime;
    }

    private bool CanMove(Vector3 direction)
    {
        Vector3 headPoint = transform.parent.position + Vector3.up * this.PlayerCtrl.Player.PlayerHeight;
        float playerSize = this.PlayerCtrl.Player.PlayerSize;
        float maxDis = moveSpeed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.parent.position, headPoint, playerSize, direction, maxDis);
        return canMove;
    }

    private Vector3 TryMoveDirX(Vector3 moveDir)
    {
        Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
        if (!CanMove(moveDirX)) return this.TryMoveDirZ(moveDir);
        return moveDirX;
    }

    private Vector3 TryMoveDirZ(Vector3 moveDir)
    {
        Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
        if (!CanMove(moveDirZ)) return Vector3.zero;
        return moveDirZ;
    }
}