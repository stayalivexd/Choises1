using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInteract2 : MonoBehaviour
{
    public Player2 player; // Reference to the Player2 script
    public GameObject deathMessage; // The UI message to display when the player is "dead"
    public Transform newPlayerPosition; // The new position to move the player to

    private bool playerIsNear = false; // Whether Player2 is near the button

    void Start()
    {
        // Initially hide the death message
        deathMessage.SetActive(false);
    }

    void Update()
    {
        // If Player2 is near the button and presses E
        if (playerIsNear && Input.GetKeyDown(KeyCode.E))
        {
            // Generate a random number between 0 and 100
            int randomNumber = Random.Range(0, 100);

            // If the number is less than 50, move the player to the new position
            if (randomNumber < 50)
            {
                player.GetComponent<CharacterController>().enabled = false;
                player.transform.position = newPlayerPosition.position;
                player.GetComponent<CharacterController>().enabled = true;
            }
            // Otherwise, display the death message
            else
            {
                deathMessage.SetActive(true);
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
