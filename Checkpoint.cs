using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public string levelName;
    public GameObject painelNextLevel;

    public bool speedPlayer = false;
    private BoxCollider2D boxcoll;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        boxcoll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        OnAnimacao();
    }
    void OnAnimacao()
    {
        if (GameController.instance.animCheckpoint)
        {
            anim.SetBool("CheckpointAbrindo", true);
            anim.SetBool("CheckpointAberto", true);                
        }
    }
    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.gameObject.tag == "Player")
        {     
            if (!(GameController.instance.animCheckpoint))
            {
                GameController.instance.CollectAllCoinsOn();
            }
            else
            {
                // GameController.instance.NextLevel(levelName); 
                painelNextLevel.SetActive(true);
                Player.instance.speed = 0f;
            }

        }
    }
    // void OnTriggerEnter2D(Collider2D trigger) {
    //     if (trigger.gameObject.tag == "Player")
    //     {
    //         if (!(GameController.instance.animCheckpoint))
    //         {
    //             GameController.instance.CollectAllAppleOn();
    //         }
    //         else
    //         {
    //             Debug.Log("Else");
    //             speedPlayer = true; 
    //             LevaPlayer(); 
    //         }
    //     }
    // }
    // void LevaPlayer()
    // {
    //     Debug.Log("Tá querendo movimentar");
    //     if (speedPlayer)
    //     {
    //         Vector3 movement = new Vector3(Player.instance.speed, 0f, 0f);
    //         Player.instance.transform.position += movement * Time.deltaTime * Player.instance.speed;
    //         //Player.instance.rig.velocity = new Vector2(5f, Player.instance.rig.velocity.y);
    //     }
    // }
}
