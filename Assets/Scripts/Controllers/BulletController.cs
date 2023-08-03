using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int GridPosition;
    public int BulletLevel;
    public List<Color> LevelColors = new List<Color>();
    public Renderer Renderer;
    [Button]
    public void AssignRandomColors(int count = 16)
    {
        LevelColors.Clear();
        for (int i = 0; i < count; i++)
        {
            LevelColors.Add(new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f));
        }
    }

    public void AssignColor()
    {
        MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
        materialPropertyBlock.SetColor("_BaseColor", LevelColors[BulletLevel]);
        Renderer.SetPropertyBlock(materialPropertyBlock);
    }
}
