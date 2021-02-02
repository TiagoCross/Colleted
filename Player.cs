using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject audioDeath;

    public bool playerOn = false;
    public bool playerOff = true;

    private AudioSource audioJump;
    public static Player instance;
    public float speed;
    public float jumpForce;
    public bool isJumping;
    public bool doubleJump;

    //Rigidbody publico somente para ser manipulado no script Checkpoint
    public Rigidbody2D rig;
    private Animator anim;

    public int lifeAtual;
    public int lifeMax;
    public GameObject life;
    
    public int energyAtual;
    public int energyMax;
    public GameObject energy;
    public int somaJump;

    // Start is called before the first frame update
    void Start()
    {
        audioJump = GetComponent<AudioSource>();
        instance = this;
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        lifeAtual = lifeMax;
        energyAtual = energyMax;
        playerOff = playerOn;
    }
    void Update() 
    {
        Move();
        Jump();
    }
    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * speed;

        if ((Input.GetAxis("Horizontal")) > 0f)
        {
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
            anim.SetBool("PlayerRun", true);
        }
        else if ((Input.GetAxis("Horizontal")) < 0f)
        {
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
            anim.SetBool("PlayerRun", true);
        }
        else if ((Input.GetAxis("Horizontal")) == 0f)
        {
            anim.SetBool("PlayerRun", false);
        }
        if ((GameController.instance.scoreEnergy) != 0)
        {
            speed = 5.0f;
            energyAtual += GameController.instance.scoreEnergy;
            GameController.instance.scoreEnergy = 0;
        }
    } 
    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                isJumping = true;
                doubleJump = true;
                anim.SetBool("PlayerJump", true);
                audioJump.enabled = true;
                audioJump.Play();
            }
            else
            {
                if (doubleJump && !(energyAtual <= 2))
                {
                    anim.SetBool("PlayerDoubleJump", true);
                    rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                    doubleJump = false;
                    somaJump ++;
                    if (somaJump == 5)
                    {
                        Energy();
                        somaJump = 0;
                    }
                }  
            }
        }
    }
    void Energy()
    {
        energyAtual -= 1;
        energy.GetComponent<Image>().fillAmount -=0.2f;
        if (energyAtual <= 2)
        {
            speed = 2.5f;
        }
    }    
    public void RecebeDano()
    {
        lifeAtual -=1;
        life.GetComponent<Image>().fillAmount -= 0.25f;
        if (lifeAtual <= 0)
        {
            Destroy(gameObject);
            GameController.instance.GameOverOn();
        }
    }
    void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("PlayerJump", false);
            doubleJump = false;
            anim.SetBool("PlayerDoubleJump", false);
        }
        if (collision.gameObject.tag == "Spiker")
        {
            speed = 0;
            anim.SetBool("PlayerHit",true);
            Destroy(gameObject, 0.5f);
            GameController.instance.GameOverOn();
        }
        if (collision.gameObject.layer == 12)
        {
            isJumping = false;
            anim.SetBool("PlayerJump", false);
            doubleJump = false;
            anim.SetBool("PlayerDoubleJump", false);
        }
        if (collision.gameObject.layer == 13)
        {
            RecebeDano();
        }
    } 
    void OnTriggerEnter2D(Collider2D trigger) {
        if (trigger.gameObject.tag == "DetectaPlayerPing")
        {
            playerOn = true;    
        }
        if (trigger.gameObject.tag == "CollisionOnBat")
        {
            playerOn = true;
        }
        if (trigger.gameObject.tag == "CollisionOnTrunk")
        {
            Trunk.instance.trunkOn = true;
        }
    } 
}
