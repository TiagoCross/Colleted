using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{
    public float time;
    private TargetJoint2D target;
    private BoxCollider2D boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        target = GetComponent<TargetJoint2D>();
    }
    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Player")
        {
            Invoke("Falling", time);
        }
        if (col.gameObject.layer == 8 || col.gameObject.tag == "Spiker")
        {
            Destroy(gameObject, 0.1f);
        }
    }
    void Falling()
    {
        target.enabled = false;
    }
}
