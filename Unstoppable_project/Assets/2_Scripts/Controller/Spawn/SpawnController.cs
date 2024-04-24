using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    float nextSpawnTime;
    float spawnCoolTimeMin;
    float spawnCoolTimeMax;

    float moveSpeed;

    // TODO :
    // 스폰할 아이템의 id를 가지고 있는 컨테이너 필요
    // 해당 컨테이너는 spawner가 초기화 될때 itemdata dictionary에서
    // key값을 반복으로 받아와서 컨테이너에 저장하도록 해야함

    // 또한 ItemSpawnOffsetY의 min, max 변수가 필요
    // (아이템이 스폰될때 y 위치값을 min ~ max에서 랜덤하게 지정)

    void Start()
    {
        nextSpawnTime = 0f;
        spawnCoolTimeMin = theApp.Data.spawnData.spawnCoolTimeMin;
        spawnCoolTimeMax = theApp.Data.spawnData.spawnCoolTimeMax;

        moveSpeed = 3f;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            nextSpawnTime = Time.time + Random.Range(spawnCoolTimeMin, spawnCoolTimeMax);
            SpawnObstacle();
        }

        // TODO : SpawnItem 구현 필요
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
        obstacle.SetSpeed(moveSpeed);
    }
}
