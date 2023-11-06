using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    bool kanan;
    public float speed;
    GameObject player;
    Rigidbody2D fireball;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        kanan = player.transform.localScale.x > 0;
        fireball = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (kanan)
        {
            fireball.velocity = new Vector2(speed, fireball.velocity.y);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
            fireball.velocity = new Vector2(-speed, fireball.velocity.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
