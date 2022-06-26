using System.IO;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonMovement : MonoBehaviour
{
    #region Parameter Field


    [Header("Dynamic Direction")]
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float walkSpeed = 1.5f;
    [SerializeField] private float sprintSpeed = 8f;

    [Header("Ground Detected Parameters")]
    [Range(-30,-5), SerializeField] private float gravity = -9.81f;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float jumpHeight = 1.5f;

    private BasicCharacterSetting characterSetting;

    private bool canMove = true;
    private Vector3 velocity;
    private float targetSpeed;
    private float turnSmoothVelocity;
    private float blend;
    private bool isGrounded;

    private CharacterController controller;
    private Transform mainCamera;
    private Transform character;
    private KeyManager keyManager;
    private Animator anim;

    #region Reachable Properties

    public bool CanMove => canMove;

    #endregion

    #endregion

    #region Script Behaviour

    private void Awake()
    {
        characterSetting = Resources.Load<BasicCharacterSetting>(Path.Combine("Character", "3D", name, $"{name}_Setting"));
        
        mainCamera = Camera.main.transform;
        character = this.transform;
        controller = this.GetComponent<CharacterController>();
        groundMask = 8;
        anim = character.GetComponent<Animator>();
        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(Path.Combine("Character", "Animation", $"{name}_Animation"));
    }

    private void Start()
    {
        keyManager = ObjectPool.GetStaff("KeyManager", "ScriptObject") as KeyManager;

        SetCharacterController();
        SetPlayerData();
    }

    void Update()
    {
        Ground_Check();

        Move();

        Jump();
    }

    #endregion

    #region Basic Movement

    private void Move()
    {
        Vector2 actionDirect = keyManager.ActionDirect;

        if (actionDirect == Vector2.zero || EnviromentManager.instance.Pause || !canMove) 
        {
            blend = blend <= 0.1 ? 0 : blend -= Time.deltaTime * 2.5f;
            anim.SetFloat("Blend", blend);
            return; 
        }

        float horizontal = actionDirect.x;
        float vertical = actionDirect.y;

        float sprint = keyManager.SprintState ? 1f : 0f;

        if (sprint == 1f) { targetSpeed = sprintSpeed; }
        if (sprint == 0f) { targetSpeed = walkSpeed; }

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(character.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            character.transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * targetSpeed * Time.deltaTime);

            float currentBlend = anim.GetFloat("Blend");
            float targetBlend = 1f + sprint;
            float deltaBlend = Time.deltaTime * 2.5f * (currentBlend >= targetBlend ? -1f : 1f);
            blend = Mathf.Abs(currentBlend - targetBlend) <= 0.1 ? targetBlend : blend += deltaBlend;

            anim.SetFloat("Blend", blend);
        }
    }

    private void Ground_Check() 
    {
        isGrounded = Physics.CheckSphere(character.position, groundDistance, groundMask);

        if(!isGrounded) { anim.SetBool("Grounded", false); }

        if(isGrounded && velocity.y < 0) 
        { 
            velocity.y = -2f;
            anim.SetBool("Jump", false);
            anim.SetBool("Grounded", true);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (keyManager.JumpState && isGrounded) 
        {
            anim.SetBool("Jump", true);
            anim.SetBool("Grounded", false);
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