using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject basicEnemyPrefab;
    public GameObject specialEnemyPrefab;
    public GameObject explosiveEnemyPrefab;

    public int numberOfBasicEnemiesPerWave = 2;
    public int numberOfSpecialEnemiesPerWave = 2;
    public int numberOfExplosiveEnemiesPerWave = 1;
    public int totalNumberOfWaves = 15;

    public TextMeshProUGUI waveText;

    public Transform[] spawners;
    public int numberOfEnemiesPerWave = 5;
    public float timeBetweenSpawns = 1.5f; // Increased time between spawns

    private int currentWave = 0;

    private void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            yield return StartCoroutine(SpawnWave()); // Wait for the current wave to finish spawning

            while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
            {
                yield return null; // Wait for the next frame
            }

            yield return new WaitForSeconds(5f); // Wait for next wave
        }
    }

    private IEnumerator SpawnWave()
    {
        currentWave++;
        waveText.text = "" + currentWave;

        int numberOfBasicEnemies = 0;
        int numberOfSpecialEnemies = 0;
        int numberOfExplosiveEnemies = 0;

        // Set the number of enemies based on the current wave
        switch (currentWave)
        {
            case 1:
                numberOfBasicEnemies = 10;
                break;
            case 2:
                numberOfBasicEnemies = 14;
                break;
            case 3:
                numberOfBasicEnemies = 15;
                numberOfSpecialEnemies = 2;
                break;
            case 4:
                numberOfBasicEnemies = 19;
                numberOfSpecialEnemies = 4;
                break;
            case 5:
                numberOfBasicEnemies = 19;
                numberOfSpecialEnemies = 6;
                break;
            case 6:
                numberOfBasicEnemies = 22;
                numberOfSpecialEnemies = 8;
                break;
            case 7:
                numberOfBasicEnemies = 24;
                numberOfExplosiveEnemies = 2;
                break;
            case 8:
                numberOfBasicEnemies = 28;
                numberOfSpecialEnemies = 14;
                break;
            case 9:
                numberOfBasicEnemies = 28;
                numberOfSpecialEnemies = 8;
                numberOfExplosiveEnemies = 5;
                break;
            case 10:
                numberOfBasicEnemies = 38;
                numberOfExplosiveEnemies = 10;
                break;
            case 11:
                numberOfBasicEnemies = 32;
                numberOfSpecialEnemies = 10;
                numberOfExplosiveEnemies = 7;
                break;
            case 12:
                numberOfBasicEnemies = 36;
                numberOfSpecialEnemies = 10;
                numberOfExplosiveEnemies = 8;
                break;
            case 13:
                numberOfBasicEnemies = 40;
                numberOfSpecialEnemies = 17;
                numberOfExplosiveEnemies = 12;
                break;
            case 14:
                numberOfSpecialEnemies = 30;
                numberOfExplosiveEnemies = 20;
                break;
            case 15:
                numberOfBasicEnemies = 58;
                numberOfSpecialEnemies = 27;
                numberOfExplosiveEnemies = 30;
                break;
        }

        // Create a list of enemy types
        List<GameObject> enemyTypes = new List<GameObject>();

        for (int i = 0; i < numberOfBasicEnemies; i++)
        {
            enemyTypes.Add(basicEnemyPrefab);
        }

        for (int i = 0; i < numberOfSpecialEnemies; i++)
        {
            enemyTypes.Add(specialEnemyPrefab);
        }

        for (int i = 0; i < numberOfExplosiveEnemies; i++)
        {
            enemyTypes.Add(explosiveEnemyPrefab);
        }

        // Shuffle the list of enemy types
        ShuffleList(enemyTypes);

        // Spawn the enemies in random order
        foreach (GameObject enemyPrefab in enemyTypes)
        {
            Instantiate(enemyPrefab, GetRandomSpawner().position, Quaternion.Euler(40.828f, 0f, 0f));
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    private GameObject GetRandomEnemyPrefab()
    {
        int randomIndex = Random.Range(0, 3);

        switch (randomIndex)
        {
            case 0:
                return basicEnemyPrefab;
            case 1:
                return specialEnemyPrefab;
            case 2:
                return explosiveEnemyPrefab;
            default:
                return basicEnemyPrefab;
        }
    }

    private Transform GetRandomSpawner()
    {
        int randomIndex = Random.Range(0, spawners.Length);
        return spawners[randomIndex];
    }

    private void ShuffleList<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
