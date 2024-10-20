using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float movementSpeed = 1f;
    public EnemyData enemyData;

    // Reference to the left and right boundary GameObjects
    public GameObject leftBoundary;  // Assign the GameObject with BoxCollider2D in the inspector
    public GameObject rightBoundary; // Assign the GameObject with BoxCollider2D in the inspector

    // Variables for different movement patterns
    private float circularRadius = 2f;   // Radius for circular movement
    private float elapsedTime = 0f;      // Time elapsed for movement calculations
    private bool isCircleComplete = false; // Track if the circular motion has completed
    private float swayAmplitude = 1f;     // Amplitude for sway movement
    private float swayFrequency = 2f;     // Frequency for sway movement

    private void Start()
    {
        if (enemyData != null)
        {
            movementSpeed = enemyData.shipSpeed; // Assign speed from EnemyData
        }
        else
        {
            Debug.LogError("EnemyData is not assigned!");
        }
    }

    private void FixedUpdate()
    {
        if (!GameManager.isGameOn) return; // Only move if the game is active
        Movement();
    }

    void Movement()
    {
        // Store the current position
        Vector2 pos = transform.position;

        // Determine the movement type from EnemyData and move accordingly
        switch (enemyData.movementType)
        {
            case EnemyData.MovementType.Straight:
                pos.y -= movementSpeed * Time.fixedDeltaTime; // Move straight down
                break;

            case EnemyData.MovementType.Circular:
                if (!isCircleComplete)
                {
                    elapsedTime += Time.fixedDeltaTime;

                    // Move in a circular pattern while descending
                    pos.x += Mathf.Cos(elapsedTime * movementSpeed) * circularRadius * Time.fixedDeltaTime; // Circular horizontal movement
                    pos.y += (Mathf.Sin(elapsedTime * movementSpeed) * circularRadius - movementSpeed) * Time.fixedDeltaTime; // Circular vertical movement and descent

                    // Rotate the sprite to ensure it faces upwards
                    float angle = Mathf.Atan2(Mathf.Sin(elapsedTime * movementSpeed), Mathf.Cos(elapsedTime * movementSpeed)) * Mathf.Rad2Deg + 90; // +90 degrees to face upwards
                    transform.rotation = Quaternion.Euler(0, 0, angle);

                    // Check if the circle is complete
                    if (elapsedTime >= 2 * Mathf.PI)
                    {
                        isCircleComplete = true; // Mark the circle as complete
                        elapsedTime = 0f; // Reset elapsedTime for potential future use
                    }
                }
                else
                {
                    // After completing the circle, move downward in a straight line
                    pos.y -= movementSpeed * Time.fixedDeltaTime;
                    // Reset rotation to face downwards after completing the circle
                    transform.rotation = Quaternion.Euler(0, 0, 180); // Reset rotation to face downwards
                }
                break;

            case EnemyData.MovementType.Sway:
                elapsedTime += Time.fixedDeltaTime;
                pos.x += Mathf.Sin(elapsedTime * swayFrequency) * swayAmplitude * Time.fixedDeltaTime; // Sway left and right
                pos.y -= movementSpeed * Time.fixedDeltaTime; // Move straight down
                break;
        }

        // Get the bounds of the left and right boundary GameObjects
        if (leftBoundary != null && rightBoundary != null)
        {
            // Calculate the left and right bounds based on the colliders
            float leftBound = leftBoundary.transform.position.x - (leftBoundary.GetComponent<BoxCollider2D>().size.x / 2);
            float rightBound = rightBoundary.transform.position.x + (rightBoundary.GetComponent<BoxCollider2D>().size.x / 2);

            // Clamp the x position between the bounds set by the GameObjects
            pos.x = Mathf.Clamp(pos.x, leftBound, rightBound);
        }

        // Ensure the ship doesn't move off the bottom of the screen
        pos.y = Mathf.Clamp(pos.y, -Camera.main.orthographicSize, float.MaxValue);

        // Update the position
        transform.position = pos;
    }
}
