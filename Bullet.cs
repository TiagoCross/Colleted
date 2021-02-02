using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    void Update()
    {
        transform.Translate(new Vector2(transform.position.x, 0.0f) * speed * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Destroy(gameObject);
            Trunk.instance.qntBullet--;
        }
        if (collision.gameObject.tag == "Player")
        {
            Player.instance.RecebeDano();
        }        
    }
}
