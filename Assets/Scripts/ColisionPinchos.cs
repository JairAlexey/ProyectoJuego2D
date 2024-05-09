using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pinchos"))
        {
            Die();
        }
    }

    void Die()
    {
        // Aquí puedes hacer que el personaje desaparezca
        gameObject.SetActive(false);

        // Reinicia la escena después de un pequeño retraso para dar tiempo a que se visualice la animación de muerte
        Invoke("RestartScene", 1f);
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
