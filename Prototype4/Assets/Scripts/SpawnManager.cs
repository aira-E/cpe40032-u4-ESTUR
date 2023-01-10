using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 9;
    public int enemyCount;
    public int waveNumber = 1;
    public GameObject powerupPrefab;
    private PlayerController playerControllerScript;
    //ublic bool spawner;
    //public GameObject GameOverPanel;


    // Start is called before the first frame update
    void Start()
    {
        // Turn enemy into a prefab so it can be instantiated by a Spawn Manager. 
        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefab, GenerateSpawnPosition(),
            powerupPrefab.transform.rotation);
        playerControllerScript = 
            GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Method that returns a spawn point
    private Vector3 GenerateSpawnPosition()
    {
        // Randomly generate spawn position
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        // for-loop to dynamically increase the number of enemies that spawn during gameplay
        for (int i = 0; i < enemiesToSpawn; i++)
        {

            Instantiate(enemyPrefab, GenerateSpawnPosition(),
            enemyPrefab.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Destroy enemies if they fall off
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            // Increase enemy count with waves
            waveNumber++;
            SpawnEnemyWave(waveNumber);
            // Spawn Powerups with new waves
            Instantiate(powerupPrefab, GenerateSpawnPosition(),
                powerupPrefab.transform.rotation);
        }
    }
    //void SpawnObstacle()
    //{
        // If the game is over stop instantiating power-ups and enemies
   //     if (playerControllerScript.gameOver == true)
   //     {
   //         spawner = false;
   //     }
   // }
}
