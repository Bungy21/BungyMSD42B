using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // a list of Waves
    [SerializeField] List<WaveConfig> waveConfigsList;

    //star from 0
    int startingWave = 0;


    //Start is called before the first frame update
    void Start()
    {
        //set the currentWave to the 1st wave (0)
        var currentWave = waveConfigsList[startingWave];

        StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    }

    // Update is called once per frame
   void Update()
    {

    }
    //spawn all enemies in waveToSpawn
   private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveToSpawn)
   {
        for (int enemyCount =1; enemyCount <= waveToSpawn.GetnumberOfEnemies(); enemyCount++)
        {
            //spawn the enemy prefab from WaveToSpawn
            //at the position of the first waypoint in Path
            var newEnemy = Instantiate(
                           waveToSpawn.GetEnemyPrefab(),
                           waveToSpawn.GetWaypointsList()[0].transform.position,
                           Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(waveToSpawn.GettimeBetweenSpawns());
        }


   }
}
