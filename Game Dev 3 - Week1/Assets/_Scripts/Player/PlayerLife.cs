using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] int playerHp;

    private PlayerVfx playerVfx;



    private void Start()
    {
        //Gets the data
        playerVfx = FindObjectOfType<PlayerVfx>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            //Will flash
            StartCoroutine(playerVfx.HitFlash());
            //Will destroy the bullet
            Destroy(collision.gameObject);
            //Will remove life based on the current damage each type of ship can do
            DealDamage(collision.gameObject.GetComponent<EnemyBullet>().bulletDamage);
        }
    }
    public void DealDamage(int damageValue)
    {
        //To dead damege to the player or kill them
        // if the hp are at 0 or less
        if ((playerHp - damageValue) <= 0)
        {
            Debug.Log("Player has " + playerHp + "HP left, Player is ded");
            //By making this bool false the game will stop
            GameManager.isGameOn = false;
            //Destroys the ship
            Time.timeScale = 0;
        }
        else if ((playerHp - damageValue) > 0 )
        {
            Debug.Log("The value is" + damageValue);
            //Deals the damage on the player
            playerHp -= damageValue;
            Debug.Log("Player has " + playerHp + "HP left");
        }
    }
}

