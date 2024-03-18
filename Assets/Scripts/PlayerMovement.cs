using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
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
        rb = GetComponent<Rigidbody>();
        currentStamina = maxStamina;
        currentSpeed = walkSpeed;
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
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || currentStamina < sprintStaminaCost)
        {
            isSprinting = false;
        }
        
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
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
        
        rb.MovePosition(rb.position + moveDirectionRelativeToCamera * currentSpeed);
        
        //////////////////////////////////////////////////////////////////////////
        /*Vector3 moveDirection = new Vector3(horizontalMovement, 0f, verticalMovement).normalized;*/

        if (isSprinting && currentStamina >= sprintStaminaCost)
        {
            rb.MovePosition(rb.position + moveDirectionRelativeToCamera * sprintSpeed * Time.fixedDeltaTime);
            currentStamina -= sprintStaminaCost * Time.fixedDeltaTime;
        }
        else
        {
            rb.MovePosition(rb.position + moveDirection * walkSpeed * Time.fixedDeltaTime);
            currentStamina += staminaRegenRate * Time.fixedDeltaTime;
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
}