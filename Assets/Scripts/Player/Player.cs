using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(NavMeshAgent))]
public class Player : MonoBehaviour
{
    public event EventHandler OnPlayerDamaged;
    public event EventHandler<OnHealthChangedEventArgs> OnHealthChanged;
    public class OnHealthChangedEventArgs : EventArgs
    {
        public float newHealth;
    }
    public event EventHandler OnPlayerDefeated;

    [Header("Camera")]
    [SerializeField] private Camera playerCamera;
    [Header("Player Stats")]
    [SerializeField] private float playerSpeed = 10f;
    [Tooltip("If enabled, player doesn't lose health.")]
    [SerializeField] private bool enableInvincibility = false;
    [SerializeField] private float maxHealth = 100f;

    private const float LOOK_MOVEMENT_MULTIPLIER = 0.1f;

    private PlayerInputHandler inputHandler;
    private CharacterController characterController;

    private float downwardSpeed;
    private float currentHealth;
    

    private void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        characterController = GetComponent<CharacterController>();

        inputHandler.OnKillEnemyPerformed += InputHandler_OnKillEnemyPerformed;
    }

    private void Start()
    {
        SetHealth(maxHealth);
    }

    private void Update()
    {
        HandleMovement();
        //HandleCameraMovement();
    }

    /// <summary>
    /// Handles player movement based on input.
    /// </summary>
    private void HandleMovement()
    {
        // Applying movement vector from input
        Vector3 moveForward = transform.forward * inputHandler.GetMoveVector().y;
        Vector3 moveRight = transform.right * inputHandler.GetMoveVector().x;
        Vector3 moveVector = (moveForward + moveRight) * 10f;

        // Gravity
        if (characterController.isGrounded)
        {
            downwardSpeed = 0f;
        }
        else
        {
            downwardSpeed -= 9.81f * Time.deltaTime;
        }
        moveVector.y = downwardSpeed;

        characterController.Move(moveVector * Time.deltaTime);

        if (!PauseManager.Instance.IsPaused())
        {
            // Applying rotation from input
            float rotationAngle = inputHandler.GetLookDelta().x * LOOK_MOVEMENT_MULTIPLIER;
            transform.Rotate(Vector3.up, rotationAngle);
        }
    }

    /// <summary>
    /// Handles camera movement based on input.
    /// </summary>
    private void HandleCameraMovement()
    {
        // Apply vertical camera rotation
        float rotationAngle = inputHandler.GetLookDelta().y * LOOK_MOVEMENT_MULTIPLIER;
        playerCamera.transform.RotateAround(transform.position, transform.right, -rotationAngle);
    }

    private void InputHandler_OnKillEnemyPerformed(object sender, EventArgs e)
    {
        Enemy closestEnemy = FindClosestEnemy();
        if (closestEnemy != null)
        {
            closestEnemy.KillEnemy();
        }
    }

    /// <summary>
    /// Returns reference to closest enemy, <c>null</c> if there are no enemies.
    /// </summary>
    /// <returns></returns>
    private Enemy FindClosestEnemy()
    {
        return FindObjectsByType<Enemy>(FindObjectsSortMode.None)
            .OrderBy(e => Vector3.Distance(transform.position, e.transform.position))
            .FirstOrDefault();
    }

    /// <summary>
    /// Applies damage to the player.
    /// </summary>
    /// <param name="damage"></param>
    public void ApplyDamage(float damage)
    {
        if (!enableInvincibility)
            SetHealth(Mathf.Max(currentHealth - damage, 0f));
        OnPlayerDamaged?.Invoke(this, EventArgs.Empty);

        if (currentHealth == 0f)
        {
            OnPlayerDefeated?.Invoke(this, EventArgs.Empty);
        }
    }

    private void SetHealth(float health)
    {
        currentHealth = health;
        OnHealthChanged?.Invoke(this, new OnHealthChangedEventArgs
        {
            newHealth = currentHealth
        });
    }

    public float GetMaxHealth() { return maxHealth; }

    private void OnDestroy()
    {
        inputHandler.OnKillEnemyPerformed -= InputHandler_OnKillEnemyPerformed;
    }
}
