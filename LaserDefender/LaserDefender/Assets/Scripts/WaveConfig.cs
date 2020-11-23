using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    //the enemy sprite
    [SerializeField] GameObject enemyPrefab;

    //the path to follow
    [SerializeField] GameObject pathPrefab;
    //time between enemy spawn
    [SerializeField] float timeBetweenSpawns = 0.5f;
    //random time difference between spawns
    [SerializeField] float spawnRandomfactor = 0.3f;
    //number of enemies in wave
    [SerializeField] int numberOfEnemies = 5;
    //enemy movement speed
    [SerializeField] float enemyMoveSpeed = 2f;

    public GameObject GetEnemyPrefab()
    {
        return enemyPrefab;
    }

    public List<Transform> GetWaypointsList()
    {
        //each wave can have different number of waypoints 
        var waveWaypoints = new List<Transform>();

        //access the Path prefab, read each waypoint and add it to the List waveWaypoints
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);

         /* waveWaypoints:
          * 
          * [0]: waypoint0
          * [1]: waypoint1
          * [2]: waypoint2
          */
        }

        return waveWaypoints;
    }
    public float GettimeBetweenSpawns()
    {
        return timeBetweenSpawns;
    }

    public float GetspawnRandomfactor()
    {
        return spawnRandomfactor;
    }

    public int GetnumberOfEnemies()
    {
        return numberOfEnemies;
    }

    public float GetenemyMoveSpeed()
    {
        return enemyMoveSpeed;
    }


}
