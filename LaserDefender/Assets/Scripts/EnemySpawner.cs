using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //a list of waves
    [SerializeField] List<WaveConfig> waveConfigList;

    //we start from wave 0
    int startingWave = 0;

    // Start is called before the first frame update
    void Start()
    {
        //set the currentWave to 0
        var currentWave = waveConfigList[startingWave];

        StartCoroutine(SpawnAllEnemiesInWave(currentWave));
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveToSpawn)
    {
        for (int enemyCount = 0; enemyCount < waveToSpawn.GetNumberOfEnemies(); enemyCount++)
        {
            //spawn the enemyPrefab from waveToSpawn
            //at the position specified waveToSpawn waypoints
            Instantiate(
                waveToSpawn.GetEnemyPrefab(),
                waveToSpawn.GetWayPoints()[0].transform.position,
                Quaternion.identity);

            //wait spawnTime
            yield return new WaitForSeconds(waveToSpawn.GetTimeBetweenSpawns());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
