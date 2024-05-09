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
        // Aqu� puedes hacer que el personaje desaparezca
        gameObject.SetActive(false);

        // Reinicia la escena despu�s de un peque�o retraso para dar tiempo a que se visualice la animaci�n de muerte
        Invoke("RestartScene", 1f);
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
