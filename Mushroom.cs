using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    private bool playerDestroy;
    public float speed;
    public Transform leftCollider;
    public Transform rightCollider;
    public Transform topCollider;
    public LayerMask layer;
    public float timeJump;
    public float forceImpulse;
    public float cronometro;
    private bool colisao;
    private Animator anim;
    private CircleCollider2D circleCollider;
    private BoxCollider2D boxCollider;
    private Rigidbody2D rig;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        circleCollider = GetComponent<CircleCollider2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        DetectaPlayer();
    }
    void Move()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y);
        colisao = Physics2D.Linecast(rightCollider.position, leftCollider.position, layer);

        if (colisao)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1f, transform.localScale.y);
            speed *= -1f;
        }
    }
    void DetectaPlayer()
    {
        if (Player.instance.playerOn)
        {
            cronometro += Time.deltaTime;
            if (cronometro > timeJump)
            {
                rig.AddForce(new Vector2(0.0f, 1.0f) * forceImpulse, ForceMode2D.Impulse);
                cronometro = 0.0f;
                if (speed < 0)
                {
                    Debug.Log(1);
                    speed = speed - 0.5f;
                }
                else
                {
                    speed = speed + 0.5f;
                }
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col) 
    {
        if (col.gameObject.tag == "Player")
        {
            float height = col.contacts[0].point.y - topCollider.position.y;
            
            if (height > 0 && !playerDestroy)
            {
                col.gameObject.GetComponent<Rigidbody2D>().AddForce(
                    new Vector2(0f,1f) * 5, ForceMode2D.Impulse);
                anim.SetBool("MushroomHit", true);
                speed = 0;
                Destroy(gameObject, 0.3f);
                Player.instance.playerOn = false;
            }       
            else
            {
                playerDestroy = true;
                GameController.instance.GameOverOn();
                Destroy(col.gameObject);
            }
        }    
    }
}
