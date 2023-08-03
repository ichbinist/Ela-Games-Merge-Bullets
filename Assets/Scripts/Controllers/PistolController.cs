using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolController : MonoBehaviour
{
    public bool IsMovementStarted;
    public bool TripleShot;
    public int AsignedBulletLevel = 0;

    public float BulletSize = 1f;
    public float StartingFireSpeed = 1f;
    public float AdditionalFireSpeed = 0f;
    private float timer;
    public BulletController BaseBulletController;

    public float Range = 1f;
    public Transform PistolBullets;
    private void Update()
    {
        if (IsMovementStarted)
        {
            timer += Time.deltaTime * StartingFireSpeed + Time.deltaTime * AdditionalFireSpeed;
            if(timer >= 1f)
            {
                timer = 0;

                if (TripleShot)
                {
                    Fire(transform.forward);
                    Fire(transform.forward + transform.right * 0.15f);
                    Fire(transform.forward + transform.right * -0.15f);

                }
                else
                {
                    Fire(transform.forward);
                }
            }
        }
    }

    private void Fire(Vector3 direction)
    {
        BulletController localBulletController = Instantiate(BaseBulletController, transform.position, Quaternion.identity);
        localBulletController.transform.parent = PistolBullets;
        localBulletController.BulletLevel = AsignedBulletLevel;
        localBulletController.AssignColor();
        localBulletController.transform.forward = direction;
        localBulletController.transform.localScale = Vector3.one * BulletSize;
        StartCoroutine(BulletTime(direction, localBulletController.gameObject));
    }

    private IEnumerator BulletTime(Vector3 direction, GameObject bullet)
    {
        float _time = 0;

        while (_time < Range)
        {
            yield return null;
            _time += Time.deltaTime;
            if(bullet == null)
                yield break;
            bullet.transform.position += direction * Time.deltaTime * 40f;
        }
        Destroy(bullet);
    }
}
