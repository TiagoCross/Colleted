    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngryPing : MonoBehaviour
{
    public bool angryPing;
    public bool playerDestroy;
    public float speed;
    public float speedMax;
    public Transform leftCol;
    public Transform rightCol;
    public Transform topCol;
    public LayerMask layer;
    
    private bool colisaoMove;
    private bool colisaoMoveAngry;
    private CircleCollider2D circleColl;
    private Rigidbody2D rig;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        circleColl = GetComponent<CircleCollider2D>();
    } 
    void Update()
    {
        if (speed < 0)
        {
            DetectaPlayer();
        }
        Move();
    }    
    void DetectaPlayer()
    {
        if (Player.instance.playerOn)
        {
            anim.SetBool("AngryPingRun", true);
            angryPing = true;
        }
    }
    void Move()
    {
        if (angryPing)
        {
            rig.velocity = new Vector2(speedMax, rig.velocity.y);
            colisaoMoveAngry = Physics2D.Linecast(leftCol.position, rightCol.position, layer);
    
            if (colisaoMoveAngry)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1f, 
                transform.localScale.y);  
                speedMax *= -1f;
            }
        }
        else
        {
            rig.velocity = new Vector2(speed, rig.velocity.y);
            colisaoMove = Physics2D.Linecast(leftCol.position, rightCol.position, layer);
        
            if (colisaoMove)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1f, 
                transform.localScale.y);
                speed *= -1f;
            }
        }      
    }
    void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Player")
        {
            float height = collision.contacts[0].point.y - topCol.position.y;
            
            if (height > 0 && !playerDestroy)
            {
                speedMax = 0;
                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(
                new Vector2(0f, 1.0f) * 5, ForceMode2D.Impulse);
                anim.SetBool("AngryPingHitPlayer", true);
                Destroy(gameObject, 0.3f);
            }
            else
            {
                playerDestroy = true;
                GameController.instance.GameOverOn();
                Destroy(collision.gameObject);
            }  
        }       
    }
}
