using System;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public event EventHandler OnKillEnemyPerformed;

    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Default.Enable();

        inputActions.Default.KillEnemy.performed += KillEnemy_performed;
    }

    public Vector2 GetMoveVector()
    {
        return inputActions.Default.Move.ReadValue<Vector2>().normalized;
    }

    public Vector2 GetLookDelta()
    {
        return inputActions.Default.Look.ReadValue<Vector2>();
    }

    private void KillEnemy_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnKillEnemyPerformed?.Invoke(this, EventArgs.Empty);
    }
}
