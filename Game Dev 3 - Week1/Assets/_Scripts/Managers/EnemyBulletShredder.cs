using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletShredder : MonoBehaviour
{

    public int damageValue = 99999;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyBullet" || other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }
}
