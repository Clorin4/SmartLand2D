using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    private Rigidbody2D platformRB;
    public float fallDeLay = 1f;
    public BoxCollider2D BoxCollider;
    
    void Start()
    {
        platformRB = GetComponent<Rigidbody2D>();
        platformRB.isKinematic = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("Fall", fallDeLay);
            Destroy(BoxCollider, 2f);
        }
    }

    private void Fall()
    {
        platformRB.isKinematic = false;
    }

}
