using System;
using UnityEngine;

public class PlayerInteract : PlayerAbstract
{
    private Vector3 lastInteract = Vector3.zero;
    private BaseCounter selectedCounter;

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    public class OnSelectedCounterChangedEventArgs
    {
        public BaseCounter selectedCounter;
    }

    private void Start()
    {
        InputManager.Instance.OnInteractAction += InputManger_OnInteractAction;
        InputManager.Instance.OnInteractAlternateAction += InputManager_OnInteractAlternateAction;
    }

    private void InputManager_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (selectedCounter == null) return;
        selectedCounter.InteractAlternate(this.PlayerCtrl);
    }

    private void InputManger_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter == null) return;
        selectedCounter.Interact(this.PlayerCtrl);
    }

    private void Update()
    {
        this.HandleInteract();
    }

    private void HandleInteract()
    {
        Vector3 moveDir = this.PlayerCtrl.Player.GetPlayerDiraction();
        if (moveDir != Vector3.zero) lastInteract = moveDir;

        float interactDistance = 2f;
        if (!Physics.Raycast(transform.position, lastInteract, out RaycastHit raycastHit, interactDistance))
        {
            SetSelectedCounter(null);
            return;
        }

        if (!raycastHit.transform.parent.TryGetComponent(out BaseCounter counter))
        {
            SetSelectedCounter(null);
            return;
        }

        if (counter != selectedCounter) SetSelectedCounter(counter);
    }

    private void SetSelectedCounter(BaseCounter counter)
    {
        selectedCounter = counter;
        //Debug.Log(selectedCounter?.ToString());
        this.OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }
}
