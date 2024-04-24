using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager
{
    readonly string strDataPath = $"Data";

    public SpawnData spawnData;
    public Dictionary<int, ItemData> itemData;

    public void Init()
    {
        // Load Data
        spawnData = LoadData<SpawnData>();
        itemData = LoadAllData<int, ItemData>();
    }

    public T LoadData<T>() where T : UnityEngine.Object
    {
        return theApp.Res.Load<T>($"{strDataPath}/{typeof(T).Name}");        
    }

    public Dictionary<Key, Data> LoadAllData<Key, Data>() where Data : UnityEngine.Object, ILoader<Key>
    {
        string className = typeof(Data).Name;
        string path = $"{strDataPath}/{className}";

        List<string> listAssetNames = new List<string>();

        TextAsset txt = theApp.Res.Load<TextAsset>($"{path}/{className}_List");
        if (txt == null)
            return null;

        using (StringReader sr = new StringReader(txt.text))
        {
            string assetName;
            while ((assetName = sr.ReadLine()) != null)
            {
                listAssetNames.Add(assetName);
            }
        }

        Dictionary<Key, Data> dict = new Dictionary<Key, Data>();
        foreach (string assetName in listAssetNames)
        {
            Data data = theApp.Res.Load<Data>($"{path}/{assetName}");
            if (data != null)
                dict.Add(data.GetKey(), data);
        }

        return dict;
    }
}
