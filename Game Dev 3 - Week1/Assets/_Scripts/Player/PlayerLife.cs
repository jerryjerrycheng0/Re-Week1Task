using System.Collections;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] int playerHp = 500;

    private PlayerVfx playerVfx;

    private void Start()
    {
        // Get the VFX component
        playerVfx = FindObjectOfType<PlayerVfx>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an enemy bullet
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            // Disable the bullet's collider and Rigidbody to prevent further interaction
            Collider2D bulletCollider = collision.gameObject.GetComponent<Collider2D>();
            Rigidbody2D bulletRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (bulletCollider != null) bulletCollider.enabled = false;
            if (bulletRb != null) bulletRb.simulated = false;

            // Log that the player was hit and by which bullet

            // Trigger the hit flash VFX
            StartCoroutine(playerVfx.HitFlash());

            // Apply damage to the player
            DealDamage(collision.gameObject.GetComponent<EnemyBullet>().bulletDamage);

            // Destroy the bullet
            Destroy(collision.gameObject);
        }
    }

    public void DealDamage(int damageValue)
    {

        // Deduct health and check if the player is dead
        playerHp -= damageValue;

        if (playerHp <= 0)
        {
            Debug.Log("Player has " + playerHp + " HP left. Player is dead.");

            // Ensure the death sequence is delayed slightly to finish all actions
            StartCoroutine(HandlePlayerDeath());
        }
        else if (playerHp > 0)
        {
            Debug.Log("Player has " + playerHp + " HP left.");
        }
    }

    private IEnumerator HandlePlayerDeath()
    {
        // Delay briefly to ensure all logs and VFX can happen before stopping the game
        yield return new WaitForSeconds(0.1f);

        // Stop the game after the brief delay
        GameManager.isGameOn = false;
        Time.timeScale = 0;
    }
}
