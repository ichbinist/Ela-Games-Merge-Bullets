using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
    public int TempCurrency = 0;
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
        
    }

    public void SaveCurrencyToLocalData()
    {
        
    }

    public void AddCurrency(int currency)
    {
        JSONDataManager.Instance.JSONDATA.Currency += currency;
        JSONDataManager.Instance.SaveData();
    }

    public void RemoveCurrency(int currency)
    {
        JSONDataManager.Instance.JSONDATA.Currency -= currency;
        JSONDataManager.Instance.SaveData();
    }

    public bool IsCurrencyEnough(int amount)
    {
        if (JSONDataManager.Instance.JSONDATA.Currency >= amount)
            return true;
        else
            return false;
    }
}
