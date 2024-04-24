using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador1Move : MonoBehaviour
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
        if (Input.GetKey(KeyCode.A)) horizontalInput = -1f;
        else if (Input.GetKey(KeyCode.D)) horizontalInput = 1f;

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

        bool wasGrounded = Grounded; // Guarda el estado previo de si estaba en el suelo
        Debug.DrawRay(transform.position, Vector3.down * 0.9f, Color.red);
        Grounded = Physics2D.Raycast(transform.position, Vector3.down, 0.9f);

        if (Grounded)
        {
            if (wasGrounded == false) // Acaba de aterrizar
            {
                animator.SetBool("Jump", false); // Termina la animación de salto
            }
        }
        else
        {
            if (wasGrounded) // Acaba de despegar
            {
                animator.SetBool("Jump", true); // Comienza la animación de salto
            }
        }



        if (Input.GetKeyDown(KeyCode.Space) & Grounded)
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
        if (Input.GetKey(KeyCode.A)) horizontalInput = -1f;
        else if (Input.GetKey(KeyCode.D)) horizontalInput = 1f;

        rb2d.velocity = new Vector2(horizontalInput * Speed, rb2d.velocity.y);
    }
}