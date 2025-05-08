using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    private PlayerInputHandler inputHandler;
    private CharacterController characterController;
    private Camera mainCamera;

    private void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        HandleMovement();
        HandleCameraMovement();
    }

    /// <summary>
    /// Handles player movement based on input
    /// </summary>
    private void HandleMovement()
    {
        Vector3 moveForward = transform.forward * inputHandler.GetMoveVector().y;
        Vector3 moveRight = transform.right * inputHandler.GetMoveVector().x;
        Vector3 moveVector = (moveForward + moveRight) * 10f;

        characterController.Move(moveVector * Time.deltaTime);
    }

    /// <summary>
    /// Handles camera movement based on input
    /// </summary>
    private void HandleCameraMovement()
    {
        Vector2 cameraRotateVector = inputHandler.GetMouseDelta();
        transform.Rotate(Vector3.up, cameraRotateVector.x);
    }
}
