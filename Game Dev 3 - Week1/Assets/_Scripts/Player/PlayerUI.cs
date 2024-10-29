using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public TMP_Text playerHP;
    PlayerLife playerLife;
    
    // Start is called before the first frame update
    void Start()
    {
        playerLife = FindObjectOfType<PlayerLife>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.isGameOn == true)
        {
            playerHP.text = "Player HP: " + playerLife.playerHp; //Showcases the player's current HP
        }
        else
        {
            playerHP.text = " "; //Shows nothing before the game starts
        }
    }
}
