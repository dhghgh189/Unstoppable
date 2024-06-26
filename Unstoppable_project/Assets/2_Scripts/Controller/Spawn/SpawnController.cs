using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    float nextSpawnTime;
    float spawnCoolTimeMin;

    float currentSpawnCoolTime;

    float itemSpawnTick;
    float currentTick;

    float itemSpawnMinY;
    float itemSpawnMaxY;

    float moveSpeed;
    float moveSpeedMax;

    float spawnCoolSubValue;
    float moveSpeedPlusValue;

    void Start()
    {
        nextSpawnTime = 0f;
        spawnCoolTimeMin = theApp.Data.spawnData.spawnCoolTimeMin;
        currentSpawnCoolTime = theApp.Data.spawnData.startSpawnCoolTime;

        itemSpawnTick = theApp.Data.spawnData.itemSpawnCoolTime;
        currentTick = 0f;

        itemSpawnMinY = theApp.Data.spawnData.itemSpawnMinY;
        itemSpawnMaxY = theApp.Data.spawnData.itemSpawnMaxY;

        moveSpeed = theApp.Data.spawnData.startMoveSpeed;
        moveSpeedMax = theApp.Data.spawnData.moveSpeedMax;

        spawnCoolSubValue = theApp.Data.spawnData.spawnCoolSubValue;
        moveSpeedPlusValue = theApp.Data.spawnData.moveSpeedPlusValue;

        // debug
        Debug.Log("Start Value");
        Debug.Log($"currentSpawnCoolTime : {currentSpawnCoolTime}");
        Debug.Log($"moveSpeed : {moveSpeed}");
    }

    void Update()
    {
        if (theApp.Game.isGameOver)
            return;

        if (Time.time >= nextSpawnTime)
        {
            nextSpawnTime = Time.time + currentSpawnCoolTime;
            SpawnObstacle();
        }

        if (currentTick >= itemSpawnTick)
        {
            float rand = Random.Range(1, 101) / 100.0f;

            if (rand <= theApp.Data.spawnData.itemSpawnPercent)
                SpawnItem();               

            currentTick = 0;
        }

        currentTick += Time.deltaTime;
    }

    void SpawnObstacle()
    {
        float xPos = theApp.Data.spawnData.spawnOffsetX;
        GameObject[] _obstacles = theApp.Data.spawnData.obstaclePrefabs;

        int obstacleIdx = Random.Range(0, _obstacles.Length);
        GameObject go = theApp.Res.Instantiate(_obstacles[obstacleIdx], pooling: true);

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

    void SpawnItem()
    {
        float xPos = theApp.Data.spawnData.spawnOffsetX;
        float yPos = Random.Range(itemSpawnMinY, itemSpawnMaxY);

        float rand = Random.Range(1, 101) / 100.0f;

        float sum = 0f;

        int itemID = -1;

        foreach (ItemData item in theApp.Data.itemData.Values)
        {
            sum += item.SpawnPercent;

            if (rand <= sum)
            {
                itemID = item.ItemID;
                break;
            }
        }

        GameObject go = theApp.Res.Instantiate("Prefabs/ItemHolder", pooling: true);
        go.transform.position = new Vector3(xPos, yPos, 0f);

        ItemHolder itemHolder = go.GetComponent<ItemHolder>();
        itemHolder.SetInfo(itemID, moveSpeed);
    }

    public void PhaseUp()
    {
        float newCoolTime = currentSpawnCoolTime - spawnCoolSubValue;
        float newMoveSpeed = moveSpeed + moveSpeedPlusValue;

        currentSpawnCoolTime = Mathf.Max(newCoolTime, spawnCoolTimeMin);
        moveSpeed = Mathf.Min(newMoveSpeed, moveSpeedMax);

        // debug
        Debug.Log("Phase Up");
        Debug.Log($"currentSpawnCoolTime : {currentSpawnCoolTime}");
        Debug.Log($"moveSpeed : {moveSpeed}");
    }
}
