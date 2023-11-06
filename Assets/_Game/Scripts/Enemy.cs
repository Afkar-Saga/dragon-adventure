using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int nyawaMusuh = 3;
    public GameObject gate;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nyawaMusuh == 0)
        {
            Destroy(gate);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected");
        if (collision.gameObject.CompareTag("Death"))
        {
            Debug.Log("Death");
            Attacked();
        }
    }

    void Attacked()
    {
        Debug.Log("I got attacked!");
        nyawaMusuh--;
        anim.SetTrigger("Hit");
    }
}
