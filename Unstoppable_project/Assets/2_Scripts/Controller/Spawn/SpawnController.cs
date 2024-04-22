using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    float nextSpawnTime;
    float spawnCoolTimeMin;
    float spawnCoolTimeMax;

    void Start()
    {
        nextSpawnTime = 0f;
        spawnCoolTimeMin = theApp.Data.spawnData.spawnCoolTimeMin;
        spawnCoolTimeMax = theApp.Data.spawnData.spawnCoolTimeMax;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            nextSpawnTime = Time.time + Random.Range(spawnCoolTimeMin, spawnCoolTimeMax);
            SpawnObstacle();
        }
    }

    void SpawnObstacle()
    {
        float xPos = theApp.Data.spawnData.spawnOffsetX;
        GameObject[] _obstacles = theApp.Data.spawnData.obstaclePrefabs;

        int obstacleIdx = Random.Range(0, _obstacles.Length);
        GameObject go = theApp.Res.Instantiate(_obstacles[obstacleIdx], transform);

        Obstacle obstacle = go.GetComponent<Obstacle>();
        if (obstacle as Obstacle_spike != null)
        {
            Obstacle_spike spike = obstacle as Obstacle_spike;

            int iSize = Random.Range(
                theApp.Data.spawnData.spikeSizeMin, theApp.Data.spawnData.spikeSizeMax + 1);
            spike.SetSize(iSize);

            if (iSize > 1)
            {
                xPos += iSize / 2.0f;
            }
        }

        obstacle.transform.position = new Vector3(xPos, go.transform.position.y, 0f);
        obstacle.SetSpeed(3f);
    }
}
