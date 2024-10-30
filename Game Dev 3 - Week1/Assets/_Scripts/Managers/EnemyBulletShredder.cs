using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletShredder : MonoBehaviour
{

    public int damageValue = 99999; //Ensure the enemy is always destroyed in one hit when passing the shredder
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyBullet" || collision.tag == "Enemy")
        {
            Destroy(collision.gameObject); //Ensure the enemy is destroyed to prevent overload
        }
    }
}
