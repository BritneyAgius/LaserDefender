using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    //a list of type Transform as waypoints are positions in x and y
    [SerializeField] List<Transform> waypointsList;

    [SerializeField] WaveConfig waveConfig;

    [SerializeField] float enemyMoveSpeed = 2f;

    //saves the waypoint in which we want to go
    //shows the next waypoint
    int waypointIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        waypointsList = waveConfig.GetWayPoints();

        //set the start position of the Enemy to the 1st waypoint
        transform.position = waypointsList[waypointIndex].transform.position;

        waypointsList = waveConfig.GetWayPoints();

        print(waypointsList.Count);
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }

    //takes care of moving Enemy along a path
    private void EnemyMove()
    {
        if(waypointIndex <= waypointsList.Count - 1)
        {
            //set the targetPosition to the waypoint where we want to go
            var targetPosition = waypointsList[waypointIndex].transform.position;

            //make sure that z axis is = 0
            targetPosition.z = 0f;

            //move enemy from current position to target position, at enemyMovement speed
            var enemyMovement = enemyMoveSpeed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, enemyMovement);

            //if enemy reaches the target position
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }

        }

        //if enemy reaches last waypoint
        else
        {
            Destroy(gameObject);
        }
    }
}
