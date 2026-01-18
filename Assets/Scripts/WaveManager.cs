using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;

    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI waveText;

    public bool waveRunning = false;
    int currentWave = 0;
    int currentWaveTime;

    void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        StartNewWave();
    }

    void Update()
    {
        // Pornim valul următor doar dacă nu rulează deja și apăsăm SPACE
        if (!waveRunning && Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartNewWave();
        }
    }

    public void StartNewWave()
    {
        timeText.color = Color.white;
        currentWave++;
        waveRunning = true;
        currentWaveTime = 30; // Durata rundei (poți pune 60 dacă vrei un minut)

        waveText.text = "Wave " + currentWave;

        StartCoroutine(WaveTimer());
    }

    IEnumerator WaveTimer()
    {
        while (waveRunning && currentWaveTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentWaveTime--;
            timeText.text = currentWaveTime.ToString();

            if (currentWaveTime <= 0)
            {
                WaveComplete();
            }
        }
    }

    void WaveComplete()
    {
        StopAllCoroutines();
        waveRunning = false;

        // 1. Curățăm inamicii existenți
        if (EnemyManager.instance != null)
        {
            EnemyManager.instance.DestroyAllEnemies();
        }

        timeText.color = Color.red;
        timeText.text = "0";

        // 2. APARE MENIUL DE CARDURI (Legătura cu GameManager)
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LevelCompleted();
        }
    }
}