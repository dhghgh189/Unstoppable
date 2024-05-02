using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Spawn Data", menuName ="Data/Create Spawn Data Asset", order = 1)]
public class SpawnData : ScriptableObject
{
    [Header("Obstacles")]
    public GameObject[] obstaclePrefabs;

    [Header("Items")]
    // 1.0 = 100%
    public float itemSpawnPercent = 0.2f;
    public float itemSpawnMinY = -3f;
    public float itemSpawnMaxY = -0.5f;

    [Header("Difficulty")]
    public float startMoveSpeed = 3f;
    public float moveSpeedMax = 8f;
    public float startSpawnCoolTime = 4.5f;
    public float spawnCoolTimeMin = 1f;
    public int firstPhaseUpScore = 2000;
    public int phaseUpScoreMultiplier = 2;
    public float spawnCoolSubValue = 0.7f;
    public float moveSpeedPlusValue = 1f;

    [Header("CoolTime")]
    public float itemSpawnCoolTime = 5f;

    [Header("Spawn Offset")]
    public float spawnOffsetX = 4f;

    [Header("Destroy Offset")]
    public float destroyOffsetX = -6f;

    [Header("Spike Size")]
    public int spikeSizeMin = 2;
    public int spikeSizeMax = 4;
}
