using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Cambiamos UnityEngine.UI por TMPro

public class SCORE : MonoBehaviour
{
    private int score;
    public TextMeshProUGUI scoreText; // Cambiamos Text por TextMeshProUGUI

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // OnTriggerEnter2D is called when the Collider2D collision occurs
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Moneda")
        {
            score++;
            scoreText.text = "COINS: " + score; // Esta línea permanece igual
        }
    }
}