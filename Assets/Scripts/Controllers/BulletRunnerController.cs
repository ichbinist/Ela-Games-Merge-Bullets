using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletRunnerController : MonoBehaviour
{
    private bool isRunning = false;
    public float Speed = 6f;
    public List<BulletController> BulletControllers = new List<BulletController>();
    public Transform BulletHolder;
    private void OnEnable()
    {
        LevelManager.Instance.OnLevelStarted.AddListener(StartRunning);
    }

    private void OnDisable()
    {
        if(LevelManager.Instance)
            LevelManager.Instance.OnLevelStarted.RemoveListener(StartRunning);
    }

    public void StartRunning()
    {
        isRunning = true;
        
        Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.Priority = -1;
        
        BulletControllers.Clear();
        for (int i = 0; i < BulletHolder.transform.childCount; i++)
        {
            BulletControllers.Add(BulletHolder.transform.GetChild(i).GetComponent<BulletController>());
        }
    }

    public void StopRunning()
    {
        isRunning = false;
    }

    private void Update()
    {
        if (isRunning)
        {
            transform.position += Vector3.forward * Time.deltaTime * Speed;
        }

        if(BulletControllers.Any(x=>x == null))
        {
            BulletControllers.RemoveAt(BulletControllers.FindIndex(x => x == null));
        }
    }
}
