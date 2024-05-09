using System.Collections; 
using System.Collections.Generic;  
using UnityEngine; 
using UnityEngine.SceneManagement;  

public class Jugador1Move : MonoBehaviour
{
    public float JumpForce;  // Fuerza aplicada al jugador cuando salta
    public float Speed;  // Velocidad horizontal del jugador

    private Animator animator;  // Referencia al componente Animator del jugador
    private Rigidbody2D rb2d;  // Referencia al componente Rigidbody2D del jugador
    private bool Grounded;  // Indica si el jugador está en el suelo
    private float fallTime;  // Tiempo que el jugador ha estado cayendo

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();  // Obtiene y asigna el componente Rigidbody2D del jugador
        animator = GetComponent<Animator>();  // Obtiene y asigna el componente Animator del jugador
        fallTime = 0;  // Inicializa el temporizador de caída
    }

    private void Update()
    {
        float horizontalInput = 0f;  // Inicializa la entrada horizontal del jugador

        // Verifica la entrada de teclado para el movimiento horizontal
        if (Input.GetKey(KeyCode.A)) horizontalInput = -1f;  // Si se presiona la tecla A, establece el valor horizontal en -1 (izquierda)
        else if (Input.GetKey(KeyCode.D)) horizontalInput = 1f;  // Si se presiona la tecla D, establece el valor horizontal en 1 (derecha)

        // Actualiza la animación y la escala del jugador en función del movimiento horizontal
        if (horizontalInput != 0)
        {
            animator.SetBool("Running", true);  // Establece la animación de correr como verdadera en el Animator
            transform.localScale = new Vector3(horizontalInput, 1f, 1f);  // Voltea la escala del jugador según la dirección horizontal
        }
        else
        {
            animator.SetBool("Running", false);  // Establece la animación de correr como falsa en el Animator
        }

        // Dibuja un rayo desde la posición del jugador hacia abajo para detectar si está en el suelo
        bool wasGrounded = Grounded;  // Almacena el estado previo de "Grounded"
        Debug.DrawRay(transform.position, Vector3.down * 0.9f, Color.red);  // Dibuja el rayo en la escena
        Grounded = Physics2D.Raycast(transform.position, Vector3.down, 0.9f);  // Realiza un Raycast para detectar si el jugador está en el suelo

        // Actualiza el estado de la animación de salto y el temporizador de caída
        if (Grounded)
        {
            if (wasGrounded == false)
            {
                animator.SetBool("Jump", false);  // Establece la animación de salto como falsa en el Animator
                fallTime = 0;  // Reinicia el contador de tiempo de caída
            }
        }
        else
        {
            if (wasGrounded)
            {
                animator.SetBool("Jump", true);  // Establece la animación de salto como verdadera en el Animator
            }
            fallTime += Time.deltaTime;  // Aumenta el contador de tiempo de caída
            if (fallTime > 3.0f)  // Si el tiempo de caída excede 3 segundos
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Reinicia la escena actual
                fallTime = 0;  // Reinicia el contador de tiempo de caída
            }
        }

        // Verifica la entrada de teclado para saltar si el jugador está en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            Jump();  // Llama a la función Jump para que el jugador salte
        }
    }

    // Función para hacer saltar al jugador
    private void Jump()
    {
        rb2d.AddForce(Vector2.up * JumpForce);  // Aplica una fuerza hacia arriba al Rigidbody2D del jugador para hacerlo saltar
    }

    private void FixedUpdate()
    {
        float horizontalInput = 0f;  // Inicializa la entrada horizontal del jugador

        // Verifica la entrada de teclado para el movimiento horizontal
        if (Input.GetKey(KeyCode.A)) horizontalInput = -1f;  // Si se presiona la tecla A, establece el valor horizontal en -1 (izquierda)
        else if (Input.GetKey(KeyCode.D)) horizontalInput = 1f;  // Si se presiona la tecla D, establece el valor horizontal en 1 (derecha)

        // Establece la velocidad horizontal del jugador
        rb2d.velocity = new Vector2(horizontalInput * Speed, rb2d.velocity.y);  // Establece la velocidad horizontal en función de la entrada y la velocidad vertical actual
    }
}
