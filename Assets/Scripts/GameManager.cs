using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Panels")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject cardSelectionUI;

    [Header("Buttons")]
    [SerializeField] Button restartButton;

    private int currentLevel = 1;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (restartButton != null) restartButton.onClick.AddListener(RestartGame);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (cardSelectionUI != null) cardSelectionUI.SetActive(false);
    }

    // Apelat de WaveManager cand timpul = 0
    public void LevelCompleted()
    {
        currentLevel++;
        Time.timeScale = 0f; // Pauza

        if (cardSelectionUI != null) cardSelectionUI.SetActive(true);

        // Generam carduri noi
        if (CardManager.instance != null)
        {
            CardManager.instance.RandomizeNewCards();
        }
    }

    // Apelat de CardManager dupa ce ai ales un card
    public void ResumeAfterCardSelection()
    {
        if (cardSelectionUI != null) cardSelectionUI.SetActive(false);

        Time.timeScale = 1f; // Reluam timpul

        // --- MODIFICARE: Pornim automat urmatorul wave
        if (WaveManager.instance != null)
        {
            WaveManager.instance.StartNewWave();
        }
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