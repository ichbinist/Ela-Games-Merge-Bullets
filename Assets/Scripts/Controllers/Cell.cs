using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

public class Cell : MonoBehaviour
{
    public Vector3 CellPosition;

    public SpriteRenderer SpriteRenderer;
    public CellState CellState;
    private BulletGridController _gridController;

    private void OnEnable()
    {
        _gridController = FindObjectOfType<BulletGridController>();
    }
}

public enum CellState
{
    Empty,
    Full
}