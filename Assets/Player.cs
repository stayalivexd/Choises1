using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5.0f;
    public float mouseSensitivity = 100.0f;
    public float gravity = -9.81f;

    private float xRotation = 0.0f;
    private Vector3 velocity;

    // Reference to the CharacterController component
    private CharacterController controller;

    // Add a reference to the InspectObject script
    private InspectObject inspectObject;

    private int inspectableObjectsNearCount = 0;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        // Hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // If the player is inspecting an object, don't allow them to move or look around
        if (inspectObject != null && inspectObject.IsInspecting)
        {
            return;
        }

        // Get the horizontal and vertical movement input (W, A, S, D by default)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Create a new vector of movement
        Vector3 move = transform.right * x + transform.forward * z;

        // Move the character
        controller.Move(move * speed * Time.deltaTime);

        // Apply gravity
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // This is to ensure the player stays on the ground
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

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

    // Call this method when the player starts inspecting an object
    public void StartInspecting(InspectObject inspectObject)
    {
        this.inspectObject = inspectObject;
        Cursor.lockState = CursorLockMode.None;
    }

    // Call this method when the player stops inspecting an object
    public void StopInspecting()
    {
        this.inspectObject = null;
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Call this method when the player enters the collider of an inspectable object
    public void EnterInspectableObject()
    {
        inspectableObjectsNearCount++;
    }

    // Call this method when the player exits the collider of an inspectable object
    public void ExitInspectableObject()
    {
        inspectableObjectsNearCount--;
    }

    // Add a property to check whether the player is near an inspectable object
    public bool IsNearInspectableObject
    {
        get { return inspectableObjectsNearCount > 0; }
    }
}
