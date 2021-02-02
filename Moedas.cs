using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moedas : MonoBehaviour
{
    private SpriteRenderer sr;
    private CircleCollider2D circleCollider;
    private AudioSource audioCol;

    public GameObject colleted;
    public int score;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
        audioCol = GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D trigger) 
    {
        if(trigger.gameObject.tag == "Player")
        {
            sr.enabled = false;
            circleCollider.enabled = false;
            colleted.SetActive(true);
            audioCol.enabled = true;
            audioCol.Play();
            GameController.instance.scoreMoeda += score;
            GameController.instance.UpdateScoreTex();
            Destroy(gameObject, 0.3f);         
        }
    }
}
