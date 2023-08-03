using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingObstacleController : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        BulletController bulletController = collision.collider.gameObject.GetComponent<BulletController>();
        if (bulletController != null)
        {
            bulletController.BulletLevel -= 1;
            bulletController.AssignColor();
            if(bulletController.BulletLevel <= 0)
            {
                Destroy(bulletController.gameObject);
            }
            Destroy(gameObject);
        }
    }
}
