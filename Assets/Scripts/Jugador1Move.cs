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
    private bool Grounded;  // Indica si el jugador est� en el suelo
    private float fallTime;  // Tiempo que el jugador ha estado cayendo

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();  // Obtiene y asigna el componente Rigidbody2D del jugador
        animator = GetComponent<Animator>();  // Obtiene y asigna el componente Animator del jugador
        fallTime = 0;  // Inicializa el temporizador de ca�da
    }

    private void Update()
    {
        float horizontalInput = 0f;  // Inicializa la entrada horizontal del jugador

        // Verifica la entrada de teclado para el movimiento horizontal
        if (Input.GetKey(KeyCode.A)) horizontalInput = -1f;  // Si se presiona la tecla A, establece el valor horizontal en -1 (izquierda)
        else if (Input.GetKey(KeyCode.D)) horizontalInput = 1f;  // Si se presiona la tecla D, establece el valor horizontal en 1 (derecha)

        // Actualiza la animaci�n y la escala del jugador en funci�n del movimiento horizontal
        if (horizontalInput != 0)
        {
            animator.SetBool("Running", true);  // Establece la animaci�n de correr como verdadera en el Animator
            transform.localScale = new Vector3(horizontalInput, 1f, 1f);  // Voltea la escala del jugador seg�n la direcci�n horizontal
        }
        else
        {
            animator.SetBool("Running", false);  // Establece la animaci�n de correr como falsa en el Animator
        }

        // Dibuja un rayo desde la posici�n del jugador hacia abajo para detectar si est� en el suelo
        bool wasGrounded = Grounded;  // Almacena el estado previo de "Grounded"
        Debug.DrawRay(transform.position, Vector3.down * 0.9f, Color.red);  // Dibuja el rayo en la escena
        Grounded = Physics2D.Raycast(transform.position, Vector3.down, 0.9f);  // Realiza un Raycast para detectar si el jugador est� en el suelo

        // Actualiza el estado de la animaci�n de salto y el temporizador de ca�da
        if (Grounded)
        {
            if (wasGrounded == false)
            {
                animator.SetBool("Jump", false);  // Establece la animaci�n de salto como falsa en el Animator
                fallTime = 0;  // Reinicia el contador de tiempo de ca�da
            }
        }
        else
        {
            if (wasGrounded)
            {
                animator.SetBool("Jump", true);  // Establece la animaci�n de salto como verdadera en el Animator
            }
            fallTime += Time.deltaTime;  // Aumenta el contador de tiempo de ca�da
            if (fallTime > 3.0f)  // Si el tiempo de ca�da excede 3 segundos
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Reinicia la escena actual
                fallTime = 0;  // Reinicia el contador de tiempo de ca�da
            }
        }

        // Verifica la entrada de teclado para saltar si el jugador est� en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            Jump();  // Llama a la funci�n Jump para que el jugador salte
        }
    }

    // Funci�n para hacer saltar al jugador
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
        rb2d.velocity = new Vector2(horizontalInput * Speed, rb2d.velocity.y);  // Establece la velocidad horizontal en funci�n de la entrada y la velocidad vertical actual
    }
}
