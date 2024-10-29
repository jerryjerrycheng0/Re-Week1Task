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
        moosic.pitch = 1f;
        playerLife = FindObjectOfType<PlayerLife>();
    }

    public void Update()
    {
        if (playerLife.isPlayerDed)
        {
           moosic.pitch = 0.3f;
        }
    }

    // Update is called once per frame
    public void PlayMusic()
    {
        if (GameManager.isGameOn)
        {
            moosic.Play();
        }
        

    }
}
