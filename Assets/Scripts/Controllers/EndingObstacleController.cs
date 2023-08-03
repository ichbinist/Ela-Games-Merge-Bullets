using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndingObstacleController : MonoBehaviour
{
    public TextMeshProUGUI HealthText;
    public int Health;

    private void Update()
    {
        HealthText.SetText(Health.ToString());
    }

    private void OnTriggerEnter(Collider other)
    {
        PistolRunner runner = other.GetComponentInParent<PistolRunner>();
        BulletController bullet = other.GetComponentInParent<BulletController>();

        if (runner != null && runner.IsShieldActive == false)
        {
            runner.IsMovementStarted = false;
            foreach (PistolController pistol in runner.Pistols)
            {
                pistol.IsMovementStarted = false;
            }
            LevelManager.Instance.FinishLevel();
        }
        if(bullet != null)
        {
            Health -= bullet.BulletLevel;
            Destroy(bullet);
            if(Health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
