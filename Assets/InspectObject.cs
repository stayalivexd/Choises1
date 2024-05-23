using UnityEngine;

public class InspectObject : MonoBehaviour
{
    public Transform objectToInspect; // The object to inspect
    public Transform inspectionView; // The position and rotation to inspect the object from
    public float rotationSpeed = 50f; // The speed of rotation when inspecting the object
    public GameObject torchLight; // The torch light to toggle

    private bool isInspecting = false; // Whether the player is currently inspecting the object
    private Player player; // Reference to the Player script
    private Vector3 originalCameraPosition; // Original position of the camera
    private Quaternion originalCameraRotation; // Original rotation of the camera
    private bool playerIsNear = false; // Whether the player is near the object

    void Start()
    {
        // Get a reference to the Player script
        player = GameObject.FindObjectOfType<Player>();
    }

    void Update()
    {
        // If the player presses E while not inspecting and they are near the object, start inspecting
        if (Input.GetKeyDown(KeyCode.E) && !isInspecting && playerIsNear)
        {
            StartInspecting();
        }
        // If the player presses E while inspecting, stop inspecting
        else if (Input.GetKeyDown(KeyCode.E) && isInspecting)
        {
            StopInspecting();
        }
        // If the player presses R while inspecting, destroy the object, stop inspecting and toggle the torch light
        else if (Input.GetKeyDown(KeyCode.R) && isInspecting)
        {
            Destroy(objectToInspect.gameObject);
            StopInspecting();
            torchLight.SetActive(!torchLight.activeSelf);
        }

        // If the player is inspecting the object, allow them to rotate it
        if (isInspecting)
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            objectToInspect.Rotate(Vector3.up, mouseX);
            objectToInspect.Rotate(Vector3.right, mouseY);
        }
    }

    void StartInspecting()
    {
        // Store the original position and rotation of the camera
        originalCameraPosition = Camera.main.transform.position;
        originalCameraRotation = Camera.main.transform.rotation;

        // Move the player to the inspection view
        Camera.main.transform.position = inspectionView.position;
        Camera.main.transform.rotation = inspectionView.rotation;

        isInspecting = true;

        // Tell the Player script that we're inspecting an object
        player.StartInspecting(this);

        // Toggle the torch light
        torchLight.SetActive(true);
    }

    void StopInspecting()
    {
        // Restore the original position and rotation of the camera
        Camera.main.transform.position = originalCameraPosition;
        Camera.main.transform.rotation = originalCameraRotation;

        isInspecting = false;

        // Tell the Player script that we've stopped inspecting an object
        player.StopInspecting();

        // Toggle the torch light
        torchLight.SetActive(false);
    }

    // Add a property to access the isInspecting variable
    public bool IsInspecting
    {
        get { return isInspecting; }
    }

    // Detect when the player enters the trigger collider
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            playerIsNear = true;
            player.EnterInspectableObject();
        }
    }

    // Detect when the player exits the trigger collider
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            playerIsNear = false;
            player.ExitInspectableObject();
        }
    }
}
