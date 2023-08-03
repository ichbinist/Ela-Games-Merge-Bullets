using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Linq;

public class LevelStartPanel : BasePanel
{
    public Button StartButton;
    public Button BuyBulletButton;


    //public GameObject TutorialObject;

    //private void Start()
    //{
    //    TutorialObject.transform.DOScale(Vector3.one * 0.6f, 1.5f).SetLoops(-1, LoopType.Yoyo);
    //}

    private void OnEnable()
    {
        if (!Managers.Instance) return;
        SceneController.Instance.OnSceneLoaded.AddListener(Activate);
        StartButton.onClick.AddListener(LevelManager.Instance.StartLevel);
        BuyBulletButton.onClick.AddListener(BuyBulletAction);
        LevelManager.Instance.OnLevelStarted.AddListener(Deactivate);
    }

    private void OnDisable()
    {
        if (!Managers.Instance) return;
        SceneController.Instance.OnSceneLoaded.RemoveListener(Activate);
        StartButton.onClick.RemoveListener(LevelManager.Instance.StartLevel);
        BuyBulletButton.onClick.RemoveListener(BuyBulletAction);
        LevelManager.Instance.OnLevelStarted.RemoveListener(Deactivate);
    }

    private void LateUpdate()
    {
        if(BulletGridManager.Instance.Bullets.BulletData.All(x=>x.BulletLevel > 0))
        {
            BuyBulletButton.enabled = false;
        }
        else
        {
            if (CurrencyManager.Instance.IsCurrencyEnough(100))
            {
                BuyBulletButton.enabled = true;
            }
            else
            {
                BuyBulletButton.enabled = false;
            }
        }
    }

    public void BuyBulletAction()
    {
        FindObjectOfType<BulletGridController>().BuyBullet();
    }
}
