using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteract : MonoBehaviour
{
    public Transform door; // The door to open
    public float doorOpenAngle = 90f; // The angle to rotate the door when opening
    public Transform cage; // The cage to teleport the player to
    public Player2 player; // Reference to the Player2 script

    private bool playerIsNear = false; // Whether Player2 is near the button

    void Update()
    {
        // If Player2 is near the button and presses E
        if (playerIsNear && Input.GetKeyDown(KeyCode.E))
        {
            // Generate a random number between 0 and 100
            int randomNumber = Random.Range(0, 100);

            // If the number is less than 70, open the door
            if (randomNumber < 70)
            {
                door.Rotate(0, doorOpenAngle, 0);
            }
            // Otherwise, teleport Player2 to the cage
            else
            {
                Debug.Log("Teleporting player to cage");
                player.GetComponent<CharacterController>().enabled = false;
                player.transform.position = cage.position;
                player.GetComponent<CharacterController>().enabled = true;
               
            }
        }
    }

    // Detect when Player2 enters the trigger collider
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            playerIsNear = true;
        }
    }

    // Detect when Player2 exits the trigger collider
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            playerIsNear = false;
        }
    }
}
