using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteCheck : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        BulletRunnerController controller = collision.collider.GetComponent<BulletRunnerController>();
        if(controller != null)
        {
            if(controller.BulletControllers.Count == 0)
            {
                controller.StopRunning();
                LevelManager.Instance.FailLevel();
            }
        }
    }
}
