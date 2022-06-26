using System.IO;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(BasicMovement))]
public class AnimancerMovement : MonoBehaviour
{

    [Header("Dynamic Direction")]
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float walkSpeed = 1.5f;
    [SerializeField] private float sprintSpeed = 5f;

    [Header("Ground Detected Parameters")]
    [Range(-30, -5), SerializeField] private float gravity = -9.81f;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float jumpHeight = 3.5f;

    private BasicCharacterSetting characterSetting;

    private bool canMove = true;
    private Vector3 velocity;
    private float targetSpeed;
    private float turnSmoothVelocity;
    private bool isGrounded;

    private CharacterController controller;
    private Transform mainCamera;
    private Transform character;
    private KeyManager manager;
    private BasicMovement movement;

    #region Reachable Properties

    public bool CanMove => canMove;

    #endregion

    #region Script Behaviour

    private void Awake()
    {
        characterSetting = Resources.Load<BasicCharacterSetting>(Path.Combine("Character", "3D", name, $"{name}_Setting"));
        
        mainCamera = Camera.main.transform;
        character = this.transform;
        controller = this.GetComponent<CharacterController>();
        movement = this.GetComponent<BasicMovement>();
        groundMask = 8;
    }

    private void Start()
    {
        manager = ObjectPool.GetStaff("KeyManager", "ScriptObject") as KeyManager;

        SetCharacterController();
        SetPlayerData();
    }

    private void Update()
    {
        Ground_Check();
        
        Move();
        
        Jump();
    }

    #endregion

    #region Movement

    private void Move()
    {
        Vector2 actionDirect = manager.ActionDirect;

        if(actionDirect == Vector2.zero || EnviromentManager.instance.Pause || !canMove) 
        { 
            movement.MoveState = Mathf.Abs(movement.MoveState) <= 0.1 ? 0 : movement.MoveState -= Time.deltaTime * 2.5f;
            movement.LandState = Mathf.Abs(movement.LandState) <= 0.1 ? 0 : movement.LandState -= Time.deltaTime * 2.5f;
            return; 
        }

        int isSprint = manager.SprintState ? 2 : 1;
        targetSpeed = manager.SprintState ? sprintSpeed : walkSpeed;

        float delta = (isSprint - movement.MoveState > 0 ? 1f : -1f) * Time.deltaTime * 2.5f; 

        movement.MoveState = Mathf.Abs(movement.MoveState - isSprint) <= 0.1f ? isSprint : movement.MoveState += delta;
        movement.LandState = Mathf.Abs(movement.LandState - isSprint) <= 0.1f ? isSprint : movement.LandState += delta;

        float horizontal = actionDirect.x;
        float vertical = actionDirect.y;

        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(character.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            character.transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * targetSpeed * Time.deltaTime);
        }
    }

    private void Ground_Check()
    {
        isGrounded = Physics.CheckSphere(character.position, groundDistance, groundMask);

        if (!isGrounded) { movement.Fall(); }

        if (isGrounded && velocity.y < 0) 
        {
            if (movement.isFalling) { movement.Landed(); }
            velocity.y = -2f; 
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if(EnviromentManager.instance.Pause || !canMove) { return; }

        if (manager.JumpState && isGrounded)
        {
            movement.Jump();
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    #endregion

    #region Setting

    private void SetCharacterController() 
    {
        controller.skinWidth = 0.0001f;
        controller.radius = characterSetting.radius;
        controller.height = characterSetting.height;
        controller.center = new Vector3(0f, controller.height / 2f, 0f);
    }

    private void SetPlayerData() 
    {
        walkSpeed = characterSetting.walkSpeed;
        sprintSpeed = characterSetting.sprintSpeed;
        jumpHeight = characterSetting.jumpHeight;
    }

    #endregion
}
