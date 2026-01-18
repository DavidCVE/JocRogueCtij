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
        // Pornim valul nou doar dacă nu rulează deja și apăsăm SPACE
        if (!waveRunning && Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            StartNewWave();
        }
    }

    void StartNewWave()
    {
        timeText.color = Color.white;
        currentWave++;
        waveRunning = true;
        currentWaveTime = 30; // Poți schimba durata aici

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

        // 1. Curățăm inamicii
        if (EnemyManager.instance != null)
        {
            EnemyManager.instance.DestroyAllEnemies();
        }

        timeText.color = Color.red;
        timeText.text = "0";

        // 2. MODIFICARE: Declanșăm apariția cardurilor în GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.LevelCompleted();
        }
    }
}