using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int bulletDamage;
    
    private void Start()
    {
        // Optionally set a lifespan for bullets
        Destroy(gameObject, 5f); // Destroy bullet after 5 seconds if it doesn't hit anything
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
