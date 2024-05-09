using UnityEngine;
using UnityEngine.SceneManagement;

public class TubeTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Asegúrate de que el nombre de la escena aquí coincida con el que guardaste
            SceneManager.LoadScene("WinnersScene");
        }
    }
} 