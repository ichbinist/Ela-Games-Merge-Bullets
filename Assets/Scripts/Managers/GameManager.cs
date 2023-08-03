using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public GameEvent OnGameFinishes = new GameEvent();

    public void CompleteStage(bool state)
    {
        if (state)
        {
            OnGameFinishes.Invoke(true);
            LevelManager.Instance.LoadNextLevel();
        }
        else
        {
            OnGameFinishes.Invoke(false);
            LevelManager.Instance.ReloadLevel();
        }
    }
}
public class GameEvent : UnityEvent<bool> { }