using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public LayerMask layer;
    public Transform leftCol;
    public Transform rightCol;
    public Transform topCol;
    public float speed;

    private bool colisao;
    private bool moveBat;
    private Rigidbody2D rig;
    private CircleCollider2D circleColl;
    private Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        circleColl = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.instance.playerOn)
        {
            moveBat = true; 
        }
        Move();
        Idle();
    }
    void Move()
    {
        if (moveBat)
        {
            circleColl.enabled = true;
            anim.SetTrigger("CeilingIn");
            anim.SetBool("Flying", true);
            rig.constraints = RigidbodyConstraints2D.FreezeRotation;
            rig.velocity = new Vector2(speed, rig.velocity.y);
            colisao = Physics2D.Linecast(leftCol.position, rightCol.position, layer);
            if (colisao)
            {
                transform.localScale = new Vector2(transform.localScale.x * -1.0f, 
                transform.localScale.y);
                speed *= -1.0f;
            }    
        }     
    }
    void Idle()
    {
        if (!moveBat)
        {
            rig.constraints = RigidbodyConstraints2D.FreezePositionY;
            circleColl.enabled = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.tag == "Player")
        {   
            float height = col.contacts[0].point.y - topCol.position.y;
            if (height > 0)
            {
                circleColl.enabled = false;
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(
                    new Vector2(0.0f, 1.0f) * 5, ForceMode2D.Impulse);
                anim.SetBool("Hit", true);
                speed = 0;
                Destroy(gameObject, 0.3f);
            }
            else
            {
                GameController.instance.GameOverOn();
                Destroy(col.gameObject);
            }
        }
    }
}
