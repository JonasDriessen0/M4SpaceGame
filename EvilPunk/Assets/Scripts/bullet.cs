using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody rb;
    GunRotate gunRotate;

    void Start()
    {
        rb.velocity = transform.right * speed;
    }
}
