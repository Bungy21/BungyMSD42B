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
}
