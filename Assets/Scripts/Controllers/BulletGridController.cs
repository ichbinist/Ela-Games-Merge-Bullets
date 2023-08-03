using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGridController : MonoBehaviour
{
    [ShowInInspector]
    [ReadOnly]
    public Grid Grid;

    public Vector2 GridSize = Vector2.one * 8;
    public float CellSize = 1f;
    public Cell CellPrefab;
    public BulletController BulletPrefab;
    public Transform RunnerBulletHolder;

    public List<BulletController> BulletControllers = new List<BulletController>();

    private void Start()
    {
        CreateGrid();
        CreateBullets();
    }

    public void InitializeGrid()
    {
        Grid = new Grid(GridSize);
        CalculateCellPositions();
    }

    public void CreateBullets()
    {
        for (int i = 0; i < JSONDataManager.Instance.JSONDATA.Bullets.BulletData.Count; i++)
        {
            if(JSONDataManager.Instance.JSONDATA.Bullets.BulletData[i].BulletLevel > 0)
                CreateBullet(JSONDataManager.Instance.JSONDATA.Bullets.BulletData[i].BulletLevel, i, false);
        }
    }

    public void CreateGrid()
    {
        InitializeGrid();
        for (int i = 0; i < GridSize.x * GridSize.y; i++)
        {
            Cell cellObject = Instantiate(CellPrefab, new Vector3(Grid.Cells[i].CellPosition.x, transform.position.y, Grid.Cells[i].CellPosition.z), Quaternion.identity, transform);
            cellObject.CellPosition = new Vector3(Grid.Cells[i].CellPosition.x, transform.position.y, Grid.Cells[i].CellPosition.z);
            cellObject.gameObject.name = "Cell: " + i;
            cellObject.gameObject.transform.localScale = Vector3.one * CellSize;
            Grid.Cells[i] = cellObject;
        }
    }

    private void CalculateCellPositions()
    {
        float totalCellWidth = GridSize.x * CellSize;
        float totalCellHeight = GridSize.y * CellSize;

        Vector2 centerOffset = new Vector2(totalCellWidth * 0.5f - CellSize * 0.5f, totalCellHeight * 0.5f - CellSize * 0.5f);
        Vector2 startPosition = new Vector2(transform.position.x - centerOffset.x, transform.position.z - centerOffset.y);

        int a = 0;

        for (int i = 0; i < GridSize.x; i++)
        {
            for (int j = 0; j < GridSize.y; j++)
            {
                Vector3 cellPosition = new Vector3(startPosition.x + i * CellSize, transform.position.y, startPosition.y + j * CellSize);
                Grid.Cells[a].CellPosition = cellPosition;
                a++;
            }
        }
    }

    public void BuyBullet()
    {
        for (int i = 0; i < BulletGridManager.Instance.Bullets.BulletData.Count; i++)
        {
            if (BulletGridManager.Instance.Bullets.BulletData[i].BulletLevel == 0)
            {
                CreateBullet(1, i);
                CurrencyManager.Instance.RemoveCurrency(100);
                break;
            }
        }
    }

    private void CreateBullet(int level, int position, bool saveData = true)
    {
        BulletController localBullet = Instantiate(BulletPrefab, RunnerBulletHolder);
        localBullet.BulletLevel = level;
        localBullet.GridPosition = position;
        localBullet.transform.position = Grid.Cells[position].CellPosition;
        localBullet.AssignColor();
        BulletControllers.Add(localBullet);
        if (saveData)
        {
            BulletGridManager.Instance.Bullets.BulletData[position].BulletLevel = level;
            JSONDataManager.Instance.SaveData();
        }
    }
}
public class Grid
{
    public Vector2 GridSize;
    public List<Cell> Cells;
    public Grid(Vector2 gridSize)
    {
        GridSize = gridSize;
        Cells = new List<Cell>();

        for (int i = 0; i < GridSize.x * GridSize.y; i++)
        {
            Cells.Add(new Cell());
        }
    }
}