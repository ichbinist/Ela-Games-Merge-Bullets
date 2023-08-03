using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class LevelFinishPanel : BasePanel
{
    public UnityEngine.UI.Button LevelFinishButton;
    public UnityEngine.UI.Button LevelFailButton;
    public GameObject SuccessPanel, FailPanel;
    public TextMeshProUGUI HighscoreText;
    private void OnEnable()
    {
        if (!Managers.Instance) return;
        LevelFinishButton.transform.localScale = Vector3.zero;
        LevelManager.Instance.OnLevelFinished.AddListener(ActivateSuccessPanel);
        LevelManager.Instance.OnLevelFailed.AddListener(ActivateFailPanel);
        SceneController.Instance.OnSceneLoaded.AddListener(Deactivate);
        LevelFinishButton.onClick.AddListener(LevelFinishButtonEvent);
        LevelFailButton.onClick.AddListener(LevelFailButtonEvent);
        OnPanelDeactivated.AddListener(() => LevelFinishButton.transform.localScale = Vector3.zero);
        OnPanelActivated.AddListener(() => LevelFinishButton.transform.DOScale(Vector3.one, 1f));
    }

    private void OnDisable()
    {
        if (!Managers.Instance) return;
        LevelManager.Instance.OnLevelFinished.RemoveListener(ActivateSuccessPanel);
        LevelManager.Instance.OnLevelFailed.RemoveListener(ActivateFailPanel);
        SceneController.Instance.OnSceneLoaded.RemoveListener(Deactivate);
        LevelFinishButton.onClick.RemoveAllListeners();
        LevelFailButton.onClick.RemoveAllListeners();
        OnPanelDeactivated.RemoveListener(() => LevelFinishButton.transform.localScale = Vector3.zero);
        OnPanelActivated.RemoveListener(() => LevelFinishButton.transform.DOScale(Vector3.one, 1f));
    }
    public void ActivateSuccessPanel()
    {
        Activate();
        SuccessPanel.SetActive(true);
        FailPanel.SetActive(false);
        StartCoroutine(DelayedCall());
    }

    public void ActivateFailPanel()
    {
        Activate();
        SuccessPanel.SetActive(false);
        FailPanel.SetActive(true);
    }

    private void LevelFinishButtonEvent()
    {
        GameManager.Instance.CompleteStage(true); 
        CurrencyManager.Instance.AddCurrency(Mathf.RoundToInt(Mathf.Max(100 * JSONDataManager.Instance.JSONDATA.Highscore, 100)));
    }

    private void LevelFailButtonEvent()
    {
        GameManager.Instance.CompleteStage(false); 
        CurrencyManager.Instance.AddCurrency(100);
    }

    IEnumerator DelayedCall()
    {
        yield return new WaitForSeconds(0.02f);
        HighscoreText.SetText((Mathf.RoundToInt(JSONDataManager.Instance.JSONDATA.Highscore * 10)).ToString("00000"));
    }
}