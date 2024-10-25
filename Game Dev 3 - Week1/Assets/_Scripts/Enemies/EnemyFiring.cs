using System.Collections;
using UnityEngine;

public class EnemyFiring : MonoBehaviour
{
    public Transform[] gunPositions;          // Positions from where the enemy shoots
    public GameObject muzzleFlashPrefab;       // Muzzle flash effect prefab
    public Vector2 bulletForce;                // Force applied to the bullet when fired

    public EnemyData enemyData; // Reference to the enemy's data
    public GameObject bulletPrefab; // Bullet prefab assigned from EnemyData

    private float fireRate = 1f;                     // Time between shots in seconds
    private bool isFiring = false;              // Track if the enemy is currently firing

    [SerializeField] private AudioSource shootSound; // Audio source for shooting sound

    private void Start()
    {
        // Initialize bullet prefab and fire rate from EnemyData
        fireRate = enemyData.fireRateEnemy;
        bulletPrefab = enemyData.bulletPrefab;
        shootSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!GameManager.isGameOn) return; // Check if the game is running

        if (!isFiring)
        {
            StartCoroutine(Shoot()); // Start the shooting coroutine if not firing
        }
    }

    private IEnumerator Shoot()
    {
        isFiring = true; // Set firing flag to true
        shootSound.Play(); // Play shooting sound

        // Loop through all gun positions and fire bullets
        for (int i = 0; i < gunPositions.Length; i++)
        {
            MuzzleFlash(i); // Show muzzle flash
            Bullet(i);      // Spawn bullet
        }

        yield return new WaitForSeconds(fireRate); // Wait for the specified fire rate
        isFiring = false; // Reset firing flag to allow new shots
    }

    private void Bullet(int arrayIndexNumber)
    {
        // Spawn the bullet at the gun position
        var spawnedBullet = Instantiate(bulletPrefab, gunPositions[arrayIndexNumber].position, Quaternion.identity);
        Rigidbody2D bulletRb = spawnedBullet.GetComponent<Rigidbody2D>();
        bulletRb.AddForce(bulletForce, ForceMode2D.Impulse); // Apply force to bullet
    }

    private void MuzzleFlash(int arrayIndexNumber)
    {
        // Generate a random rotation for the muzzle flash
        float randomRotation = Random.Range(0, 360);
        var muzzleFlash = Instantiate(muzzleFlashPrefab, gunPositions[arrayIndexNumber].transform.position, Quaternion.Euler(0, 0, randomRotation));
        Destroy(muzzleFlash, 1f); // Destroy muzzle flash after 1 second
    }
}
