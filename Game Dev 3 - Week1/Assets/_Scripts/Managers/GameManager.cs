using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    //By making the bool static and public, we can access the variable from anywhere without referencing
    //Be careful with using static, since it makes sense when there is only one istance of this object in the whole project!
    //For example, do not use static for the enemyHp variable, since there would be more than on!
    public static bool isGameOn = false;

    public UnityEvent gameStarted;

    public GameObject gameOverUI; //Game over screen
    public void GameIsOn()
    {
        isGameOn = true;

        gameStarted.Invoke();
    }

    public void GameIsOff()
    {
        isGameOn = false;
    }

    public void GameOver()
    {
        //Activates the game over screen
        gameOverUI.SetActive(true);
    }
}
