using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnergyApple : MonoBehaviour
{
    public static EnergyApple instance;
    private SpriteRenderer sr;
    private CircleCollider2D circleCollider;
    private AudioSource audioCol;
    public GameObject audioEnergy;
    public GameObject colleted;

    public int score;
    // Start is called before the first frame update
    void Start()
    {
        audioCol = GetComponent<AudioSource>();
        instance = this;
        sr = GetComponent<SpriteRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();        
    }

    void OnTriggerEnter2D(Collider2D trigger) 
    {
        if (trigger.gameObject.tag == "Player")
        {
            audioCol.enabled = true;
            audioCol.Play();
            sr.enabled = false;
            circleCollider.enabled = false;
            colleted.SetActive(true);
            GameController.instance.UpdateEnergyTex();
            Destroy(gameObject, 0.3f);
        }
    }
}
