using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Spawn Data", menuName ="Data/Create Spawn Data Asset", order = 1)]
public class SpawnData : ScriptableObject
{
    [Header("Obstacles")]
    public GameObject[] obstaclePrefabs;

    [Header("CoolTime")]
    public float spawnCoolTimeMin = 2.5f;
    public float spawnCoolTimeMax = 4.5f;

    [Header("Spawn Offset")]
    public float spawnOffsetX = 4f;

    [Header("Destroy Offset")]
    public float destroyOffsetX = -6f;

    [Header("Spike Size")]
    public int spikeSizeMin = 2;
    public int spikeSizeMax = 4;
}