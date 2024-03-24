using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    //Player Set Stat
    [Header("Stat")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxStamina;
    [SerializeField] private float staminaRegenRate;
    
    private float currentSpeed;
    private Vector3 moveDirection;
    
    //Stamina Cost
    [Header("")]
    [SerializeField] private float sprintStaminaCost;

    private float currentStamina;
    private bool isGrounded;
    private Rigidbody rb;
    private float horizontalMovement;
    private float verticalMovement;
    private bool isSprinting;

    [SerializeField] private Slider staminaSlider;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        currentStamina = maxStamina;
        currentSpeed = walkSpeed;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        /*UpdateStaminaUI();*/
    }

    private void Update()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
        
        //Convert Input to Vector3
        moveDirection = new Vector3(horizontalMovement, 0f, verticalMovement).normalized;
        //
        
        //Check object isGround
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && currentStamina >= sprintStaminaCost)
        {
            isSprinting = true;
            currentSpeed = sprintSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || currentStamina < sprintStaminaCost)
        {
            isSprinting = false;
            currentSpeed = walkSpeed;
        }
        
        MovePlayer();
        RotatePlayer();
    }

    private void FixedUpdate()
    {
        RegenerateStamina();
    }

    private void MovePlayer()
    {
        /// คำนวณตำแหน่งใหม่ของ Player โดยใช้ Vector3.forward และ Vector3.right ของกล้อง ////
        Vector3 camForward = Camera.main.transform.forward;
        Vector3 camRight = Camera.main.transform.right;

        camForward.y = 0; // !Move naew tang
        camRight.y = 0;

        Vector3 moveDirectionRelativeToCamera = moveDirection.x * camRight.normalized + moveDirection.z * camForward.normalized;
        moveDirectionRelativeToCamera.y = 0; // !Move naew tang
        
        rb.MovePosition(rb.position + moveDirectionRelativeToCamera * currentSpeed * Time.deltaTime);
        
        //////////////////////////////////////////////////////////////////////////
        /*Vector3 moveDirection = new Vector3(horizontalMovement, 0f, verticalMovement).normalized;*/
        if (isSprinting && currentStamina >= sprintStaminaCost)
        {
            currentStamina -= sprintStaminaCost * Time.deltaTime;
        }
        else
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
        }
        
        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        UpdateStaminaUI();
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void RegenerateStamina()
    {
        if (!isSprinting && currentStamina < maxStamina && !Input.GetKey(KeyCode.LeftShift))
        {
            currentStamina += staminaRegenRate * Time.fixedDeltaTime;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
            UpdateStaminaUI();
        }
    }

    private void UpdateStaminaUI()
    {
        staminaSlider.value = currentStamina / maxStamina;
    }
    
    
    private void RotatePlayer()
    {
        if (verticalMovement <= 0) return;
        Vector3 cameraDirection = Camera.main.transform.forward;
        Vector3 velocity = new Vector3();
        cameraDirection.y = 0;
        
        if (transform.forward.normalized == cameraDirection.normalized) return;
        transform.forward = Vector3.SmoothDamp(transform.forward,transform.forward.magnitude * cameraDirection.normalized, ref velocity, 0.03f);
    }
}