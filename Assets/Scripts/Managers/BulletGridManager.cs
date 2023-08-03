using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletGridManager : Singleton<BulletGridManager>
{
    public Bullets Bullets;

    public void SaveBulletDataToLocalData()
    {
        JSONDataManager.Instance.JSONDATA.Bullets.BulletData = new List<BulletData>(Bullets.BulletData);
    }

    private void OnEnable()
    {
        JSONDataManager.Instance.OnDataLoaded += InitializeData;
    }

    private void OnDisable()
    {
        if (JSONDataManager.Instance)
            JSONDataManager.Instance.OnDataLoaded -= InitializeData;
    }

    private void InitializeData(JSONDATA jsondata)
    {
        Bullets = new Bullets();
        Bullets.BulletData = jsondata.Bullets.BulletData.ToList();
    }
}

[System.Serializable]
public class Bullets
{
    public List<BulletData> BulletData = new List<BulletData>();
    
    public Bullets() 
    {
        for (int i = 0; i < 8; i++)
        {
            BulletData.Add(new BulletData());
        }
    }
}

[System.Serializable]
public class BulletData
{
    public int BulletLevel;
}