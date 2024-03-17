using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed,sprintSpeed,jumpFore,staminaMax,staminaRegenRate;
    
    private float currentStamita;
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentStamita = staminaMax;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
        Sprint();
        Jump();
    }

    private void PlayerMove()
    {
        float moaveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moaveHorizontal, 0f, moveVertical);

        if (movement != Vector3.zero)
        {
            if (Input.GetKey(KeyCode.LeftShift) && currentStamita > 0)
            {
                rb.velocity = movement * sprintSpeed;
                currentStamita -= Time.deltaTime * 10f;
            }
            else
            {
                rb.velocity = movement * walkSpeed;
            }

            if (currentStamita < staminaMax)
            {
                currentStamita += staminaRegenRate;
                currentStamita = Mathf.Clamp(currentStamita, 0f, staminaMax);
            }
        }
    }

    private void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && currentStamita > 0)
        {
            walkSpeed = sprintSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || currentStamita <= 0)
        {
            walkSpeed = walkSpeed;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGround())
            {
                rb.AddForce(Vector3.up * jumpFore, ForceMode.Impulse);
            }
        }
    }

    private bool IsGround()
    {
        RaycastHit hit;
        float distance = 1f;
        Vector3 dir = Vector3.down;

        if (Physics.Raycast(transform.position,dir, out hit , distance))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
