using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LevelFinishPanel : BasePanel
{
    public UnityEngine.UI.Button LevelFinishButton;
    public UnityEngine.UI.Button LevelFailButton;
    public GameObject SuccessPanel, FailPanel;
    private void OnEnable()
    {
        if (!Managers.Instance) return;
        LevelFinishButton.transform.localScale = Vector3.zero;
        LevelManager.Instance.OnLevelFinished.AddListener(ActivateSuccessPanel);
        LevelManager.Instance.OnLevelFailed.AddListener(ActivateFailPanel);
        SceneController.Instance.OnSceneLoaded.AddListener(Deactivate);
        LevelFinishButton.onClick.AddListener(() => GameManager.Instance.CompleteStage(true));
        LevelFailButton.onClick.AddListener(() => { GameManager.Instance.CompleteStage(false); CurrencyManager.Instance.AddCurrency(100); });
        OnPanelDeactivated.AddListener(() => LevelFinishButton.transform.localScale = Vector3.zero);
        OnPanelActivated.AddListener(() => LevelFinishButton.transform.DOScale(Vector3.one, 1f));
    }

    private void OnDisable()
    {
        if (!Managers.Instance) return;
        LevelManager.Instance.OnLevelFinished.RemoveListener(ActivateSuccessPanel);
        LevelManager.Instance.OnLevelFailed.RemoveListener(ActivateFailPanel);
        SceneController.Instance.OnSceneLoaded.RemoveListener(Deactivate);
        LevelFinishButton.onClick.RemoveListener(() => GameManager.Instance.CompleteStage(true));
        LevelFailButton.onClick.RemoveAllListeners();
        OnPanelDeactivated.RemoveListener(() => LevelFinishButton.transform.localScale = Vector3.zero);
        OnPanelActivated.RemoveListener(() => LevelFinishButton.transform.DOScale(Vector3.one, 1f));
    }
    public void ActivateSuccessPanel()
    {
        Activate();
        SuccessPanel.SetActive(true);
        FailPanel.SetActive(false);
    }

    public void ActivateFailPanel()
    {
        Activate();
        SuccessPanel.SetActive(false);
        FailPanel.SetActive(true);
    }
}
