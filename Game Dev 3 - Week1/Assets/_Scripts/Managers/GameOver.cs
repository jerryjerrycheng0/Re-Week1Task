using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject[] uiElements;  // Array to hold the UI elements to activate
    [SerializeField] private AudioSource gameOverSound;  // Reference to the Game Over sound


    private PlayerLife playerLife;

     void Start()
    {
        // Initially disable all UI elements
        SetUIActive(false);
        playerLife = FindObjectOfType<PlayerLife>();
    }

    public void TriggerGameOver()
    {
        if (playerLife.isPlayerDed == true)
        {
            SetUIActive(true);

            // Play the Game Over sound
            if (gameOverSound != null)
            {
                gameOverSound.Play();
            }

            // Unlock the cursor and make it visible
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            // Pause the game
            Time.timeScale = 0; // Stop the game time
        }
    }

    private void SetUIActive(bool isActive)
    {
        // Set each UI element active or inactive
        foreach (GameObject uiElement in uiElements)
        {
            uiElement.SetActive(isActive);
        }
    }
}
