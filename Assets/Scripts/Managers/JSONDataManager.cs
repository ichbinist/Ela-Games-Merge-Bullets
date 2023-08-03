using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class JSONDataManager : Singleton<JSONDataManager>
{
    public Action OnSaveData;
    public bool IsSavedBefore
    {
        get
        {
            return (File.Exists(persistentPath));
        }
    }

    private string persistentPath;
    public JSONDATA JSONDATA;

    public Action<JSONDATA> OnDataLoaded;
    public Action<JSONDATA> OnDataCreated;

    private void Start()
    {
        SetPaths();
        if(IsSavedBefore)
            LoadData();
        else
            NewData();
    }

    private void SetPaths()
    {
        //path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
    }

    [Button]
    public void NewData()
    {
        JSONDATA = new JSONDATA();
        string json = JsonUtility.ToJson(JSONDATA);
        using StreamWriter writer = new StreamWriter(persistentPath);
        writer.Write(json);
        OnDataCreated?.Invoke(JSONDATA);
        writer.Close();
        JSONDATA.Currency = 100;
        SaveData();
    }

    [Button]
    public void SaveData()
    {
        OnSaveData?.Invoke();
        InitializeData();
        string json = JsonUtility.ToJson(JSONDATA);
        using StreamWriter writer = new StreamWriter(persistentPath);
        writer.Write(json);
    }

    private void InitializeData()
    {
        BulletGridManager.Instance.SaveBulletDataToLocalData();
        CurrencyManager.Instance.SaveCurrencyToLocalData();
    }

    [Button]
    public void LoadData()
    {
        if (File.Exists(persistentPath))
        {
            using StreamReader reader = new StreamReader(persistentPath);
            string json = reader.ReadToEnd();

            JSONDATA data = JsonUtility.FromJson<JSONDATA>(json);
            JSONDATA = data;

            OnDataLoaded?.Invoke(JSONDATA);
        }
        else
        {
            JSONDATA = null;
        }
    }
}

[System.Serializable]
public class JSONDATA
{
    public Bullets Bullets = new Bullets();
    public int Currency;
}