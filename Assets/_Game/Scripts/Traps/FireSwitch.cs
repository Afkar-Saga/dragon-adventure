using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSwitch : MonoBehaviour
{
    Animator anim;
    private bool pressed = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("Pressed", pressed);
        pressed = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pressed = true;
            GameObject.Find("Group Fire").tag = "Ground";
            foreach (GameObject fire in GameObject.FindGameObjectsWithTag("Fire"))
            {
                fire.GetComponent<Animator>().SetTrigger("Disabled");
            }
        }
    }
}
