using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera playerCamera;

    private PlayerInputHandler inputHandler;
    private CharacterController characterController;

    private const float LOOK_MOVEMENT_MULTIPLIER = 0.1f;

    private void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        characterController = GetComponent<CharacterController>();
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
        // Applying movement vector from input
        Vector3 moveForward = transform.forward * inputHandler.GetMoveVector().y;
        Vector3 moveRight = transform.right * inputHandler.GetMoveVector().x;
        Vector3 moveVector = (moveForward + moveRight) * 10f;

        characterController.Move(moveVector * Time.deltaTime);

        // Applying rotation from input
        float rotationAngle = inputHandler.GetLookDelta().x * LOOK_MOVEMENT_MULTIPLIER;
        transform.Rotate(Vector3.up, rotationAngle);
    }

    /// <summary>
    /// Handles camera movement based on input
    /// </summary>
    private void HandleCameraMovement()
    {
        // Apply vertical camera rotation
        float rotationAngle = inputHandler.GetLookDelta().y * LOOK_MOVEMENT_MULTIPLIER;
        playerCamera.transform.RotateAround(transform.position, transform.right, -rotationAngle);
    }
}
