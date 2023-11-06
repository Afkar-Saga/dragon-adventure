using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    Rigidbody2D player;
    Animator anim;
    BoxCollider2D boxCollider;
    PlayerInput input;

    private float horizontal;
    private bool isShooting;
    private bool isCrouching;
    [Header("Pengaturan Player")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    [Header("Pengaturan Fireball")]
    public GameObject fireball;
    public GameObject titikFireball;

    void Awake()
    {
        player = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        input = GetComponent<PlayerInput>();
    }

    void Update()
    {
        PlayerMaju();
        Flip();

        anim.SetBool("Walk", horizontal != 0);
        anim.SetBool("Grounded", IsGrounded());
        anim.SetBool("Shooting", isShooting);
        anim.SetBool("Crouch", isCrouching);
        isShooting = false;
        Debug.DrawRay(boxCollider.bounds.center + new Vector3(0, -0.6f, 0), Vector2.down, Color.green);
    }

    void PlayerMaju()
    {
        if (isCrouching) return;
        player.velocity = new Vector2(horizontal * speed, player.velocity.y);
    }

    void Flip()
    {
        if (horizontal == 0) return;
        transform.localScale = (horizontal > 0) ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // player mati
        if (collision.gameObject.CompareTag("Death") || collision.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.Respawn();
            GameManager.Instance.UpdateNyawa(-1);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // ambil coin
        if (collision.gameObject.CompareTag("Coin"))
        {
            speed += 0.5f;
            jumpForce += 0.5f;
            GameManager.Instance.TambahCoin(1);
            Destroy(collision.gameObject);
        }
        // ambil items/fruits
        if (collision.gameObject.CompareTag("Items"))
        {
            GameManager.Instance.UpdateNyawa(1);
            Destroy(collision.gameObject);
        }
        // checkpoint & respawn
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            collision.gameObject.GetComponent<Animator>().SetTrigger("Checkpoint");
            GameManager.Instance.SetRespawn(collision.gameObject.transform);
        }
        // pindah room
        if (collision.gameObject.CompareTag("Room"))
        {
            Camera.main.transform.position = new Vector3(collision.gameObject.transform.position.x, 0, -10);
        }
        // menang
        if (collision.gameObject.CompareTag("Finish"))
        {
            GameObject.FindGameObjectWithTag("Finish").GetComponent<Animator>().SetTrigger("Pressed");
            anim.SetTrigger("Finish");
            GameManager.Instance.ShowVictory();
            input.actions.FindActionMap("Player").Disable();
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.min + 0.01f * Vector3.up, boxCollider.bounds.size, 0, Vector2.down, 0.05f, groundLayer);
        return raycastHit.collider != null;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (isCrouching) return;
        if (context.performed && IsGrounded())
        {
            player.velocity = new Vector2(player.velocity.x, jumpForce);
            anim.SetTrigger("Jump");
        }
        if (context.canceled && player.velocity.y > 0)
        {
            player.velocity = new Vector2(player.velocity.x, player.velocity.y * 0.5f);
        }
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isShooting = true;
            Instantiate(fireball, titikFireball.transform.position, Quaternion.identity);
        }
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isCrouching = true;
            player.velocity = new Vector2(0, player.velocity.y * 0.5f);
        }
        if (context.canceled)
        {
            isCrouching = false;
        }
    }
}
