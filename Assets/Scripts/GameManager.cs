using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Singleton - Instance cu "I" mare pentru a fi accesibil din CardManager și Card
    public static GameManager Instance;

    [Header("Panels")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject cardSelectionUI; // Panoul care conține cele 3 carduri

    [Header("Buttons")]
    [SerializeField] Button restartButton;

    private int currentLevel = 1;

    void Awake()
    {
        // Setăm Singleton-ul
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Configurăm butonul de restart
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);

        // Ne asigurăm că panourile sunt ascunse la început
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (cardSelectionUI != null) cardSelectionUI.SetActive(false);
    }

    // Funcția apelată de WaveManager când timpul expiră
    public void LevelCompleted()
    {
        currentLevel++;

        // 1. Oprim timpul (Pauză)
        Time.timeScale = 0f;

        // 2. Afișăm UI-ul de carduri
        if (cardSelectionUI != null)
        {
            cardSelectionUI.SetActive(true);
        }

        // 3. Generăm cardurile noi prin CardManager
        if (CardManager.instance != null)
        {
            CardManager.instance.RandomizeNewCards();
        }
    }

    // Funcția apelată de Card.cs după ce jucătorul a dat click pe un card
    public void ResumeAfterCardSelection()
    {
        // 1. Ascundem meniul
        if (cardSelectionUI != null)
        {
            cardSelectionUI.SetActive(false);
        }

        // 2. Pornim timpul înapoi
        Time.timeScale = 1f;

        UnityEngine.Debug.Log("Pregătește-te! Apasă SPACE pentru noul val.");
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void GameOver()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}