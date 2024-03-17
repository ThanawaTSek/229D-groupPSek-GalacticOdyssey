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
        
    }
}
