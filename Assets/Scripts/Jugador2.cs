using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador2 : MonoBehaviour
{

    public float JumpForce;
    public float Speed;

    private Animator animator;
    private Rigidbody2D rb2d;
    private bool Grounded;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Movimiento horizontal
        float horizontalInput = 0f;
        if (Input.GetKey(KeyCode.Keypad4)) horizontalInput = -1f;
        else if (Input.GetKey(KeyCode.Keypad6)) horizontalInput = 1f;

        // Animación y orientación
        if (horizontalInput != 0)
        {
            animator.SetBool("Running", true);
            transform.localScale = new Vector3(horizontalInput, 1f, 1f);
        }
        else
        {
            animator.SetBool("Running", false);
        }

        Debug.DrawRay(transform.position, Vector3.down * 0.9f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.9f))
        {
            Grounded = true;
        }
        else Grounded = false;

        // Salto
        if (Input.GetKeyDown(KeyCode.KeypadEnter) & Grounded)
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb2d.AddForce(Vector2.up * JumpForce);
    }

    private void FixedUpdate()
    {
        // Aplicar movimiento horizontal
        float horizontalInput = 0f;
        if (Input.GetKey(KeyCode.Keypad4)) horizontalInput = -1f;
        else if (Input.GetKey(KeyCode.Keypad6)) horizontalInput = 1f;

        rb2d.velocity = new Vector2(horizontalInput * Speed, rb2d.velocity.y);
    }
}