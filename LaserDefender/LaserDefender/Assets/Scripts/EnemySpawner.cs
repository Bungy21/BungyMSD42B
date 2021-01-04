using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // a list of Waves
    [SerializeField] List<WaveConfig> waveConfigsList;

    [SerializeField] bool looping = false;

    //star from 0
    int startingWave = 0;


    //Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            //start coroutine that spawns all waves
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping); //while (looping == true)
  
    }

    // Update is called once per frame
   void Update()
    {

    }
    //spawn all enemies in waveToSpawn
   private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveToSpawn)
   {
        //loop to spawn all enemies in wave
        for (int enemyCount =1; enemyCount <= waveToSpawn.GetnumberOfEnemies(); enemyCount++)
        {
            //spawn the enemy prefab from WaveToSpawn
            //at the position of the first waypoint in Path
            var newEnemy = Instantiate(
                           waveToSpawn.GetEnemyPrefab(),
                           waveToSpawn.GetWaypointsList()[startingWave].transform.position,
                           Quaternion.identity) as GameObject;

            //the wave will be selected from her and the enemy applied to it
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveToSpawn);

            yield return new WaitForSeconds(waveToSpawn.GettimeBetweenSpawns());
        }


   }

    private IEnumerator SpawnAllWaves()
    {
        //loop
        foreach(WaveConfig currentWave in waveConfigsList)
        {
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }
}
