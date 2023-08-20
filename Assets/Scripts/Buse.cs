using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buse : MonoBehaviour
{
    public float speed;
    //Rigidbody rb;
    Vector3 pos;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //pos = new Vector3 
    }

    void FixedUpdate()
    {
        //rb.velocity = -transformLocal.forward * speed *
        //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, speed);
        transform.Translate(-transform.forward * speed * Time.fixedDeltaTime);
    }
}
