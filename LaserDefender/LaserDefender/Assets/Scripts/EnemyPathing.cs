using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] List<Transform> waypointsList;
    // Start is called before the first frame update
    void Start()
    {
        //set the position of the enemy ship to the 1st waypoint
        transform.position = waypointsList[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
