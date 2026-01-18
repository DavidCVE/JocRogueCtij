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
    }

    public void StartNewWave()
    {
        timeText.color = Color.white;
        currentWave++;
        waveRunning = true;
        currentWaveTime = 30;

        waveText.text = "Wave " + currentWave;

        StartCoroutine(WaveTimer());

        if (EnemyManager.instance != null)
        {

        }
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
        yield return null;
    }

    void WaveComplete()
    {
        StopAllCoroutines();
        waveRunning = false;

        if (EnemyManager.instance != null)
        {
            EnemyManager.instance.DestroyAllEnemies();
        }

        timeText.color = Color.red;
        timeText.text = "0";


        UnityEngine.Debug.Log("Wave terminat! Se deschid cardurile...");

        if (GameManager.Instance != null)
        {
            GameManager.Instance.LevelCompleted();
        }
    }
}