using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerManager : MonoBehaviour
{

    [SerializeField] Transform[] spawnPoints; //An array of spawning spots

    [SerializeField] float delayBetweenSpawns;

    [SerializeField] int numberOfEnemiesSpawned;

    [SerializeField] float delayBetweenWaves;

    [SerializeField] GameObject enemyPrefab;

    public EnemyData[] enemyData;

    [SerializeField] int wavesNumber;

    [SerializeField] private int currentWaveCount = 0;

    public void SpawnerLogic()
    {
        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        for (int i=0; i < numberOfEnemiesSpawned; i++)
        {
            int randomInteger = Random.Range(0, spawnPoints.Length); //Ensures it spawns in EVERY spot
            GameObject spawnedShip = Instantiate(enemyPrefab, spawnPoints[randomInteger]);

            //Obtains all required data for the enemy ships

            spawnedShip.GetComponent<EnemyVisual>().enemyData = enemyData[currentWaveCount];
            spawnedShip.GetComponent<EnemyMovement>().enemyData = enemyData[currentWaveCount];
            spawnedShip.GetComponent<EnemyLife>().enemyData = enemyData[currentWaveCount];
            spawnedShip.GetComponent<EnemyFiring>().enemyData = enemyData[currentWaveCount];
            yield return new WaitForSeconds(delayBetweenSpawns);
        }

        currentWaveCount++;
        if (currentWaveCount > enemyData.Length - 1)
        {
            currentWaveCount = 0;
        }

        yield return new WaitForSeconds(delayBetweenWaves);
        StartCoroutine(SpawnWave());
    }
}
