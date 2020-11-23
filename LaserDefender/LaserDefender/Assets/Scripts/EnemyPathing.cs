using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    //a list of type Transform as waypoints are positions in x and y   
    [SerializeField] List<Transform> waypointsList;

    [SerializeField] float enemyMoveSpeed = 2f;


    [SerializeField] WaveConfig waveConfig;

    //shows the next waypoint 
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        //set the position of the enemy ship to the 1st waypoint
        transform.position = waypointsList[0].transform.position;

        waypointsList = waveConfig.GetWaypointsList();

    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();

    }

    private void EnemyMove()
    {
        // 0, 
        if (waypointIndex < waypointsList.Count)
        {
            //set the targetPosition to the next waypoint Position
            //targetposition: where we want to go
            var targetPosition = waypointsList[waypointIndex].transform.position;

            targetPosition.z = 0f;

            var enemyMovement = enemyMoveSpeed * Time.deltaTime;

            //move from current position, to target position, at enemyMovement speed
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, enemyMovement);

            // check if we reached targetPosition
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        //if enemy reached last     waypoint
        else
        {
            Destroy(gameObject);
        }

    }
}
