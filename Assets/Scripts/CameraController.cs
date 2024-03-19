using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float sensitivity;
    [SerializeField] private Transform playerBody;
    
    [SerializeField] private Transform cameraTransform;
    
    private float xRotation = 0f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        /*transform.LookAt(cameraTransform);*/
        
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(xRotation,0f,0f);
        playerBody.Rotate(Vector3.up * mouseX );
        
        float cameraRotationY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        cameraTransform.Rotate(Vector3.left * cameraRotationY);
        
        
    }
}
