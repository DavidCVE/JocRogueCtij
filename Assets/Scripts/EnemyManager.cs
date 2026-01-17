using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    [Header("Settings")]
    [SerializeField] GameObject enemyPrefab;

    // MODIFICARE 1: Adăugăm un loc pentru noul inamic (Charger)
    [SerializeField] GameObject chargerPrefab;

    [SerializeField] float timeBetweenSpawns = 1f;
    [SerializeField] Transform enemiesParent;

    float currentTimeBetweenSpawns;

    List<GameObject> enemies = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (enemiesParent == null)
        {
            GameObject parentObj = GameObject.Find("Enemies");
            if (parentObj != null)
            {
                enemiesParent = parentObj.transform;
            }
        }
    }

    private void Update()
    {
        // Verificăm dacă WaveManager ne lasă să spawnăm
        if (WaveManager.instance != null && WaveManager.instance.waveRunning == false)
        {
            return;
        }

        currentTimeBetweenSpawns -= Time.deltaTime;

        if (currentTimeBetweenSpawns <= 0)
        {
            SpawnEnemy();
            currentTimeBetweenSpawns = timeBetweenSpawns;
        }
    }

    void SpawnEnemy()
    {
        float randomX = UnityEngine.Random.Range(-16f, 16f);
        float randomY = UnityEngine.Random.Range(-8f, 8f);
        Vector2 spawnPosition = new Vector2(randomX, randomY);

        // MODIFICARE 2: Alegem ce inamic spawnăm
        GameObject enemyToSpawn;

        int roll = UnityEngine.Random.Range(0, 100);

        if (roll < 80)
        {
            enemyToSpawn = enemyPrefab;   // 80% șanse pentru inamic normal
        }
        else
        {
            enemyToSpawn = chargerPrefab; // 20% șanse pentru Charger
        }

        // Folosim 'enemyToSpawn' în loc de 'enemyPrefab' direct
        GameObject newEnemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);

        enemies.Add(newEnemy);

        if (enemiesParent != null)
        {
            newEnemy.transform.SetParent(enemiesParent);
        }
    }

    public void DestroyAllEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        enemies.Clear();
    }
}