using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    //Default life value
    private int enemyHp;
    //Reference to the player firing script
    private PlayerFiring firingScriptRef;
    //Ref to the enemy vfx script
    private EnemyVfx enemyVfx;

    private EnemyBulletShredder bulletShredder;

    public EnemyData enemyData;

    private void Start()
    {
        //Gets the data
        firingScriptRef = FindObjectOfType<PlayerFiring>();        
        enemyVfx = GetComponent<EnemyVfx>();
        enemyHp = enemyData.shipHP;
        bulletShredder = FindObjectOfType<EnemyBulletShredder>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            //Will flash
            StartCoroutine(enemyVfx.HitFlash());
            //Will destroy the bullet
            Destroy(collision.gameObject);
            //Will remove life based on the current damage thre player can do
            RemoveHp(firingScriptRef.damageValue);
        }

    }
    private void OnCollision2D(Collision2D other)
    {
        if (other.gameObject.tag == "Shredder")
        {
            //Will flash
            StartCoroutine(enemyVfx.HitFlash());
            //Will destroy the bullet
            Destroy(other.gameObject);
            //Will remove life based on the current damage thre player can do
            RemoveHp(bulletShredder.damageValue);
        }
    }

        public void RemoveHp(int hpToRemove)
    {
        //Destroys the enemyShip if the hit brings it tp 0 or below
        if ((enemyHp - hpToRemove) <= 0)
        {
            //You can add a timer to it by putting a comma and a float variable Example:Destroy(gameObject, 0.5f)
            Destroy(gameObject);
        }
        else
        {
            //Removes the required hp
            enemyHp -= hpToRemove;
        }
    }


}
