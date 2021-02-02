using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : MonoBehaviour
{
    public static Trunk instance;

    public bool trunkOn;
    public float speed;
    public Transform leftCol;
    public Transform rightCol;
    public Transform topCol;
    public LayerMask layer;
    
    private bool collision;
    private Rigidbody2D rig;
    private Animator anim;
    private CircleCollider2D circleColl;

    public GameObject bullet;
    public float qntBullet;
    public float cronometro;
    public float timeBullet;

    void Start()
    {
        circleColl = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
        instance = this;
        rig = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Move();
        if (trunkOn)
        {
            Tiro();
        }
    }
    void Tiro()
    {
        cronometro += Time.deltaTime;
        if((cronometro >= timeBullet) && (qntBullet != 3))
        {
            qntBullet++;
            Instantiate(bullet, transform.position, Quaternion.identity, this.transform);
            anim.SetTrigger("Attack");
            cronometro = 0.0f;
        }   
    }
    void Move()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y);
        collision = Physics2D.Linecast(rightCol.position, leftCol.position, layer);
        if (collision)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1.0f, transform.localScale.y);
            speed *= -1.0f;
        }     
    }
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player")
        {
            float height =  col.contacts[0].point.y - topCol.position.y;
            Debug.Log(height);
            if (height > 0)
            {
                anim.SetBool("Hit", true);
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(
                    new Vector2(0.0f, 1.0f) * 5, ForceMode2D.Impulse);
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
