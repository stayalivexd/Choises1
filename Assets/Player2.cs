using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public float speed = 5.0f;
    public float mouseSensitivity = 100.0f;

    private float xRotation = 0.0f;

    // Reference to the CharacterController component
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        // Hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get the horizontal and vertical movement input (W, A, S, D by default)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Create a new vector of movement
        Vector3 move = transform.right * x + transform.forward * z;

        // Move the character
        controller.Move(move * speed * Time.deltaTime);

        // Get the mouse movement input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Calculate the new rotation of the camera
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply the rotation to the camera
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
}
