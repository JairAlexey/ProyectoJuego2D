using UnityEngine;
using UnityEngine.SceneManagement;

public class TubeTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Aseg�rate de que el nombre de la escena aqu� coincida con el que guardaste
            SceneManager.LoadScene("WinnersScene");
        }
    }
} 