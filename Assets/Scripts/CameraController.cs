using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    Vector3 offset;
    public float speed;
    
    void Start()
    {
        offset = transform.position - player.position;
    }
    
    void FixedUpdate()
    {
        transform.position = Vector2.Lerp(transform.position, offset + player.position, speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, offset.z + + player.position.z);
    }
}





