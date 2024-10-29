using System.Collections;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public int playerHp;

    private PlayerVfx playerVfx;

    public AudioSource playerDed;

    public bool isPlayerDed = false;

    public AudioSource playerHurt;

    GameManager gameManager;


    private void Start()
    {
        // Get the VFX component
        playerVfx = FindObjectOfType<PlayerVfx>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision is with an enemy bullet
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            // Disable the bullet's collider and Rigidbody to prevent endless damage per frame
            Collider2D bulletCollider = collision.gameObject.GetComponent<Collider2D>();
            Rigidbody2D bulletRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (bulletCollider != null) bulletCollider.enabled = false;
            if (bulletRb != null) bulletRb.simulated = false;

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

        // Deduct health and check if the player is dead, then plays a hurt sound
        playerHp -= damageValue;
        playerHurt.Play();

        if (playerHp <= 0)
        {
            isPlayerDed = true;
            gameManager.GameOver();

            // Ensure the death sequence is delayed slightly to finish all actions
            StartCoroutine(HandlePlayerDeath());
        }
    }

    private IEnumerator HandlePlayerDeath()
    {
        // Short delay for good measure
        yield return new WaitForSeconds(0.1f);

        // Stop the game after the brief delay
        GameManager.isGameOn = false;
        isPlayerDed = true;
        playerDed.Play();
        Time.timeScale = 0; //Stops the timer to prevent the enemies from further spawning
    }
}
