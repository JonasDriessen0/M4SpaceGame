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
    public float timeBetweenSpawns = 1f;

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
        waveText.text = "Wave: " + currentWave;

        int numberOfBasicEnemies = 0;
        int numberOfSpecialEnemies = 0;
        int numberOfExplosiveEnemies = 0;

        // Set the number of enemies based on the current wave
        switch (currentWave)
        {
            case 1:
                numberOfBasicEnemies = 5;
                break;
            case 2:
                numberOfBasicEnemies = 7;
                break;
            case 3:
                numberOfBasicEnemies = 6;
                numberOfSpecialEnemies = 1;
                break;
            case 4:
                numberOfBasicEnemies = 7;
                numberOfSpecialEnemies = 2;
                break;
            case 5:
                numberOfBasicEnemies = 1;
                break;
            case 6:
                numberOfBasicEnemies = 10;
                numberOfSpecialEnemies = 4;
                break;
            case 7:
                numberOfBasicEnemies = 6;
                numberOfExplosiveEnemies = 1;
                break;
            case 8:
                numberOfBasicEnemies = 12;
                numberOfSpecialEnemies = 5;
                break;
            case 9:
                numberOfBasicEnemies = 14;
                numberOfSpecialEnemies = 5;
                numberOfExplosiveEnemies = 1;
                break;
            case 10:
                numberOfBasicEnemies = 1;
                break;
            case 11:
                numberOfBasicEnemies = 15;
                numberOfSpecialEnemies = 5;
                numberOfExplosiveEnemies = 3;
                break;
            case 12:
                numberOfBasicEnemies = 17;
                numberOfSpecialEnemies = 5;
                numberOfExplosiveEnemies = 4;
                break;
            case 13:
                numberOfBasicEnemies = 20;
                numberOfSpecialEnemies = 4;
                numberOfExplosiveEnemies = 3;
                break;
            case 14:
                numberOfBasicEnemies = 21;
                numberOfSpecialEnemies = 6;
                numberOfExplosiveEnemies = 2;
                break;
            case 15:
                numberOfBasicEnemies = 1;
                break;
        }

        // Spawn the enemies based on the number of each type
        for (int i = 0; i < numberOfBasicEnemies; i++)
        {
            Instantiate(basicEnemyPrefab, GetRandomSpawner().position, Quaternion.Euler(40.828f, 0f, 0f));
            yield return new WaitForSeconds(timeBetweenSpawns);
        }

        for (int i = 0; i < numberOfSpecialEnemies; i++)
        {
            Instantiate(specialEnemyPrefab, GetRandomSpawner().position, Quaternion.Euler(40.828f, 0f, 0f));
            yield return new WaitForSeconds(timeBetweenSpawns);
        }

        for (int i = 0; i < numberOfExplosiveEnemies; i++)
        {
            Instantiate(explosiveEnemyPrefab, GetRandomSpawner().position, Quaternion.Euler(40.828f, 0f, 0f));
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
}
