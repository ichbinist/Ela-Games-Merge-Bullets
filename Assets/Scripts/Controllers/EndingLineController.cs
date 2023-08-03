using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;


using UnityEngine;

public class EndingLineController : MonoBehaviour
{
    public EndingObstacleController EndingObstaclePrefab;
    public List<EndingObstacleController> Obstacles = new List<EndingObstacleController>();
    public int StartingHealth = 100;
    public int IncrementalAmount = 100;
    public PistolRunner PistolRunner;
    private void OnEnable()
    {
        LevelManager.Instance.OnLevelFinished.AddListener(CalculateDistanceAndApply);   
    }

    private void OnDisable()
    {
        LevelManager.Instance.OnLevelFinished.RemoveListener(CalculateDistanceAndApply);
    }

    private void CalculateDistanceAndApply()
    {
        if(JSONDataManager.Instance.JSONDATA.Highscore < Vector3.Distance(transform.position, PistolRunner.transform.position))
        {
            JSONDataManager.Instance.JSONDATA.Highscore = Vector3.Distance(transform.position, PistolRunner.transform.position);
            JSONDataManager.Instance.SaveData();
        }
    }

    [Button]
    public void PlaceObstacles(int lineAmount = 16)
    {
        for (int i = 0; i < lineAmount; i++)
        {
            EndingObstacleController Obstacle = Instantiate(EndingObstaclePrefab, transform);
            Obstacle.gameObject.transform.localPosition = Vector3.forward * 4f + Vector3.forward * i * 4f;
            Obstacle.Health = StartingHealth + IncrementalAmount * i;
            Obstacles.Add(Obstacle);

            Obstacle = Instantiate(EndingObstaclePrefab, transform);
            Obstacle.gameObject.transform.localPosition = Vector3.forward * 4f + Vector3.forward * i * 4f + Vector3.right * 3f;
            Obstacle.Health = StartingHealth + IncrementalAmount * i;
            Obstacles.Add(Obstacle);

            Obstacle = Instantiate(EndingObstaclePrefab, transform);
            Obstacle.gameObject.transform.localPosition = Vector3.forward * 4f + Vector3.forward * i * 4f - Vector3.right * 3f;
            Obstacle.Health = StartingHealth + IncrementalAmount * i;
            Obstacles.Add(Obstacle);

        }
    }

    [Button]
    public void ClearObstacles()
    {
        foreach (var obstacle in Obstacles)
        {
            DestroyImmediate(obstacle.gameObject);
        }
        Obstacles.Clear();
    }
}
