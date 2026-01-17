using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesar pentru a reîncărca scena
using UnityEngine.UI; // Necesar pentru a folosi tipul "Button"

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton

    [SerializeField] GameObject gameOverPanel; // Panoul de Game Over
    [SerializeField] Button restartButton;     // Butonul de Restart

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Aici este linia exactă de la minutul 3:59:
        // Ascultă butonul și când este apăsat, cheamă funcția RestartGame
        restartButton.onClick.AddListener(RestartGame);

        // Asigură-te că panoul e ascuns la început
        gameOverPanel.SetActive(false);
    }

    public void GameOver()
    {
        // Activează panoul când mori
        gameOverPanel.SetActive(true);

        // Oprește timpul (ca să nu se mai miște inamicii pe fundal)
        Time.timeScale = 0f;
    }

    void RestartGame()
    {
        // Repornește timpul (altfel jocul va rămâne blocat după restart)
        Time.timeScale = 1f;

        // Reîncarcă scena curentă
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}