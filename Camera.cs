using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;
    public Transform background;
    public bool maxMin;
    public float xMax;
    public float xMin;
    public float yMax;
    public float yMin;
    private void FixedUpdate() 
    {
        transform.position = Vector2.Lerp(transform.position, player.position, 0.2f);
        background.transform.position = Vector2.Lerp(transform.position, background.position, 0.2f);

        if (maxMin)     
        {
            transform.position = new Vector3(Mathf.Clamp(player.position.x, xMin, xMax), 
            Mathf.Clamp(player.position.y, yMin, yMax), 2 * player.position.z);
            
            background.transform.position = new Vector3(Mathf.Clamp(player.position.x, xMin, xMax), 
            Mathf.Clamp(player.position.y, yMin, yMax), 2 * player.position.z);
        }  
    }
}
