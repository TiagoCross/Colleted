using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    public Transform[] pointsMove;
    public float speed;
    public float veloMove;
    public int startPoint;

    void Start()
    {
        transform.position = pointsMove[startPoint].transform.position;
    }
    void Update()
    {
        transform.Rotate(0.0f, 0.0f, Time.deltaTime * veloMove);
        Move();
    }
    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, 
        pointsMove[startPoint].transform.position, speed * Time.deltaTime);

        if (transform.position == pointsMove[startPoint].transform.position)
        {
            startPoint +=1;
        }
        if (startPoint == pointsMove.Length)
        {
            startPoint = 0;
        }
    }
}
