using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Rotate(transform.up * speed * Time.deltaTime);
    }
}
