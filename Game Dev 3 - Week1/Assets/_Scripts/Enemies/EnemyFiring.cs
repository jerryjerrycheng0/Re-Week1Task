using System.Collections;
using UnityEngine;

public class EnemyFiring : MonoBehaviour
{
    public Transform[] gunPositions;
    private int bulletHurt;
    public GameObject bulletPrefab;
    public GameObject muzzleFlashPrefab;
    public Vector2 bulletForce;

    [SerializeField] EnemyData enemyData;

    private float fireRate; // Time between shots in seconds
    public bool isFiring = false; // Track if the enemy is currently firing

    void Start()
    {
        fireRate = enemyData.fireRateEnemy;
        bulletHurt = enemyData.bulletDamage; // Assuming this is assigned in the inspector
    }

    void Update()
    {
        // If the game is off, it will not continue the code
        if (!GameManager.isGameOn) return;

        if (!isFiring)
        {
            StartCoroutine(Shoot());
        }
    }

    private IEnumerator Shoot()
    {
        isFiring = true; // Set firing flag to true

        // The for loop will make sure we can shoot from both locations at once
        for (int i = 0; i < gunPositions.Length; i++)
        {
            MuzzleFlash(i);
            Bullet(i);
        }

        
        // Wait for the specified fire rate before allowing another shot
        yield return new WaitForSeconds(fireRate);
        isFiring = false; // Reset firing flag to allow new shots


    }

    private void Bullet(int arrayIndexNumber)
    {
        // Spawns the bullet
        var spawnedBullet = Instantiate(bulletPrefab, gunPositions[arrayIndexNumber].position, Quaternion.identity);

        // Gets its rigidbody
        Rigidbody2D bulletRb = spawnedBullet.GetComponent<Rigidbody2D>();

        // Adds force to it so it can be actually yeeted away
        bulletRb.AddForce(bulletForce, ForceMode2D.Impulse);
    }

    private void MuzzleFlash(int arrayIndexNumber)
    {
        // To get a random value so we can use it to give the muzzle flash a random rotation
        float randomRotation = Random.Range(0, 360);
        // Spawns the muzzleflash and stores it into a variable 
        var muzzleFlash = Instantiate(muzzleFlashPrefab, gunPositions[arrayIndexNumber].transform.position, Quaternion.Euler(0, 0, randomRotation));
        // Destroys the muzzleflash game object since we do not need it anymore
        Destroy(muzzleFlash, 1f);
    }
}
