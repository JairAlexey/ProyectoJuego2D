using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jugador2 : MonoBehaviour
{
    public float JumpForce;
    public float Speed;

    private Animator animator;
    private Rigidbody2D rb2d;
    private bool Grounded;
    private float fallTime;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        fallTime = 0;
    }

    private void Update()
    {

        float horizontalInput = 0f;
        if (Input.GetKey(KeyCode.Keypad4)) horizontalInput = -1f;
        else if (Input.GetKey(KeyCode.Keypad6)) horizontalInput = 1f;

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
        bool wasGrounded = Grounded;
        Grounded = Physics2D.Raycast(transform.position, Vector3.down, 0.9f);

        if (Grounded)
        {
            if (!wasGrounded)
            {
                fallTime = 0;
            }
        }
        else
        {
            if (wasGrounded)
            {
                fallTime += Time.deltaTime;
            }
            if (fallTime > 5.0f)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                fallTime = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.KeypadEnter) && Grounded)
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
        float horizontalInput = 0f;
        if (Input.GetKey(KeyCode.Keypad4)) horizontalInput = -1f;
        else if (Input.GetKey(KeyCode.Keypad6)) horizontalInput = 1f;

        rb2d.velocity = new Vector2(horizontalInput * Speed, rb2d.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) //Ajusta el tag de acuerdo al objeto que consideres suelo.
        {
            Grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) //Ajusta el tag de acuerdo al objeto que consideres suelo.
        {
            Grounded = false;
        }
    }
}
