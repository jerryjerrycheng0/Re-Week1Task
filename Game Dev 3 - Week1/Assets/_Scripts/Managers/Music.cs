using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource moosic;
    PlayerLife playerLife;

    // Start is called before the first frame update
    void Start()
    {
        moosic.pitch = 1f; //Ensure the music is played normally
        playerLife = FindObjectOfType<PlayerLife>(); //Obtaining the isPlayerDed boolean value
    }

    public void Update()
    {
        if (playerLife.isPlayerDed)
        {
           moosic.pitch = 0.3f; //Lowers the pitch when dead
        }
    }

    // Update is called once per frame
    public void PlayMusic()
    {
        if (GameManager.isGameOn)
        {
            moosic.Play(); //Ensures the music is played AFTER the title is gone
        }
        

    }
}
