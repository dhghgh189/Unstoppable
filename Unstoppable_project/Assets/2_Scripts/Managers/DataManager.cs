using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public SpawnData spawnData;

    public void Init()
    {
        // Load Data
        spawnData = theApp.Res.Load<SpawnData>("Data/SpawnData");

        Debug.Log("Data Load Done.");
    }
}
