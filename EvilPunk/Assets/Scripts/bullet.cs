using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody rb;
    public int damage = 20;

    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject, 10f);
    }
}
