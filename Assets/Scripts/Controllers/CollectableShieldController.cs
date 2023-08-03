using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableShieldController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PistolRunner runner = other.GetComponentInParent<PistolRunner>();
        if (runner != null)
        {
            runner.ActivateShield();
            Destroy(gameObject);
        }
    }
}
