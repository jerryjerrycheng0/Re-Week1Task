using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float movementSpeed = 1f;
    public EnemyData enemyData;

    // Reference to the left and right boundary GameObjects
    public Collider2D leftBoundary;  // Assign the boundaries' colliders
    public Collider2D rightBoundary; // Same as above

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
        movementSpeed = enemyData.shipSpeed; //Determines how fast the ship moves
    }

    void Movement()
    {
        // Store the current position
        Vector2 pos = transform.position;

        // Determine the movement type from EnemyData and move accordingly
        switch (enemyData.movementType)
        {
            case EnemyData.MovementType.Straight:
                pos.y -= movementSpeed * Time.fixedDeltaTime; // Moves straight down
                break;

            case EnemyData.MovementType.Circular: //Spins a few times before moving down
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

            // Moves in a circular pattern while descending
            pos.x += Mathf.Cos(elapsedTime * movementSpeed) * circularRadius * Time.fixedDeltaTime;
            pos.y += (Mathf.Sin(elapsedTime * movementSpeed) * circularRadius - movementSpeed) * Time.fixedDeltaTime;

            // Rotate the sprite to ensure the ship's head faces downwards
            float angle = Mathf.Atan2(Mathf.Sin(elapsedTime * movementSpeed), Mathf.Cos(elapsedTime * movementSpeed)) * Mathf.Rad2Deg + 90;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            // Check if the circle is complete
            if (elapsedTime >= 2 * Mathf.PI)
            {
                isCircleComplete = true; // Mark the circle as complete
                elapsedTime = 0f; // Reset elapsedTime
            }
        }
        else
        {
            // After completing the circle, move downward in a straight line
            pos.y -= movementSpeed * Time.fixedDeltaTime;
            // Reset rotation to face downwards after completing the circle
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }

        return pos; //Returns the position values to Movement()
    }
}
