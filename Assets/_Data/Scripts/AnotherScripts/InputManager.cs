using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance => instance;

    private PlayerInputAction inputActions;

    public event EventHandler OnInteractAction;

    public event EventHandler OnInteractAlternateAction;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Only one Input Manager allow exsit!");
            return;
        }

        InputManager.instance = this;

    }

    private void Start()
    {
        ActivePlayerInputAction();
    }

    private void ActivePlayerInputAction()
    {
        inputActions = new PlayerInputAction();
        inputActions.Player.Enable();
        inputActions.Player.Interact.performed += Interact_performed;
        inputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        this.OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        this.OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
