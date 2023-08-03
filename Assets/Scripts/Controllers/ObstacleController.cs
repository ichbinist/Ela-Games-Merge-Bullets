using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PistolRunner runner = other.GetComponentInParent<PistolRunner>();
        if (runner != null && runner.IsShieldActive == false)
        {
            runner.IsMovementStarted = false;
            foreach (PistolController pistol in runner.Pistols)
            {
                pistol.IsMovementStarted = false;
            }
            LevelManager.Instance.FailLevel();
        }
    }
}
