using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float movementSpeed = 1f;
    public EnemyData enemyData;

    // Reference to the left and right boundary GameObjects
    public Collider2D leftBoundary;  // Assign the GameObject with BoxCollider2D in the inspector
    public Collider2D rightBoundary; // Assign the GameObject with BoxCollider2D in the inspector

    // Variables for different movement patterns
    private float circularRadius = 2f;   // Radius for circular movement
    private float elapsedTime = 0f;      // Time elapsed for movement calculations
    private bool isCircleComplete = false; // Track if the circular motion has completed
    private float swayAmplitude = 1f;     // Amplitude for sway movement
    private float swayFrequency = 2f;     // Frequency for sway movement


    private void FixedUpdate()
    {
        if (!GameManager.isGameOn) return; // Only move if the game is active
        //To move the ship
        Movement();
    }

    private void Start()
    {
        movementSpeed = enemyData.shipSpeed;
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
                pos = CircularMovement(pos);
                break;

            case EnemyData.MovementType.Sway:
                elapsedTime += Time.fixedDeltaTime;
                pos.x += Mathf.Sin(elapsedTime * swayFrequency) * swayAmplitude * Time.fixedDeltaTime; // Sway left and right
                pos.y -= movementSpeed * Time.fixedDeltaTime; // Move straight down
                break;
        }

        transform.position = pos;
        if (pos.y <= -20f)
        {
            Destroy(gameObject);
        }
    }

    private Vector2 CircularMovement(Vector2 pos)
    {
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

        return pos;
    }
}
