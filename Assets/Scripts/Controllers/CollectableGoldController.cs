using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableGoldController : MonoBehaviour
{
    public int CurrencyReward = 100;
    private void OnTriggerEnter(Collider other)
    {
        PistolRunner runner = other.GetComponentInParent<PistolRunner>();
        if (runner != null)
        {
            CurrencyManager.Instance.AddCurrency(CurrencyReward);
            Destroy(gameObject);
        }
    }
}
