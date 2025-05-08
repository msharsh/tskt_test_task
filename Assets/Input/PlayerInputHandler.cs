using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Default.Enable();
    }

    public Vector2 GetMoveVector()
    {
        return inputActions.Default.Move.ReadValue<Vector2>().normalized;
    }

    public Vector2 GetLookDelta()
    {
        return inputActions.Default.Look.ReadValue<Vector2>();
    }
}
