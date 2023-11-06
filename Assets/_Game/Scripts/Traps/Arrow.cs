using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D arrow;
    public float speed;
    float timer;
    void Start()
    {
        arrow = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        arrow.velocity = new Vector2(arrow.velocity.x, -speed);
        timer += Time.deltaTime;
        if (timer > 1) Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
