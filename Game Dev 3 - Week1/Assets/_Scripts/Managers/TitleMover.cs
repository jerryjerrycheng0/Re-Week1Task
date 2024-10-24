using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TitleMover : MonoBehaviour
{
    //Title movement 
    public Transform endPositionTransform;
    public Vector3 punchStrenght;
    public float moveDuration;
    [SerializeField] AudioSource startSound;

    GameManager gameManagerScriptReference;


    // Start is called before the first frame update
    void Start()
    {
        gameManagerScriptReference = FindObjectOfType<GameManager>();
        startSound = GetComponent<AudioSource>();
        startSound.volume = 0.5f;
        StartCoroutine(TItleAnimation());

    }

    private IEnumerator TItleAnimation()
    {
        //Wait
        yield return new WaitForSeconds(2f);
        //The title moves down
        transform.DOMove(endPositionTransform.position, moveDuration);
        //Wait
        yield return new WaitForSeconds(moveDuration - 0.1f);
        //The tile scale punches for a cool effect
        transform.DOPunchScale(punchStrenght, 1f);
        startSound.Play();
        //Wait
        yield return new WaitForSeconds(moveDuration);
        //The tile lerps to a scale of 0
        transform.DOScale(Vector3.zero, moveDuration);
        //Destroys the title after it has scaled down
        Destroy(this, moveDuration);
        //Will allow the game to start
        gameManagerScriptReference.GameIsOn();
    }
}
