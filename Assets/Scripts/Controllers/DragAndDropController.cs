using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;
using UnityEngine.UIElements;

public class DragAndDropController : MonoBehaviour
{
    private BulletController HoldingBullet;
    public BulletGridController _BulletGridController;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                BulletController bullet = hit.transform.GetComponent<BulletController>();
                if (bullet != null)
                {
                    HoldingBullet = bullet;
                }
            }
        }

        if(Input.GetMouseButton(0) && HoldingBullet != null)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 18.5f;
            Vector3 Worldpos = Camera.main.ScreenToWorldPoint(mousePos);

            HoldingBullet.transform.position = Worldpos;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (HoldingBullet == null) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            RaycastHit[] hits;
            hits = Physics.RaycastAll(ray);  

            if (Physics.Raycast(ray, out hit))
            {
                Cell cell = null;

                foreach (RaycastHit rayhits in hits)
                {
                    cell = rayhits.transform.GetComponent<Cell>();
                    if (cell != null)
                    {
                        break;
                    }
                }

                if (cell != null)
                {
                    int cellIndex = _BulletGridController.Grid.Cells.IndexOf(cell);

                    if (JSONDataManager.Instance.JSONDATA.Bullets.BulletData[cellIndex].BulletLevel == 0)
                    {
                        HoldingBullet.transform.position = cell.CellPosition;
                        BulletGridManager.Instance.Bullets.BulletData[HoldingBullet.GridPosition].BulletLevel = 0;
                        HoldingBullet.GridPosition = cellIndex;
                        BulletGridManager.Instance.Bullets.BulletData[cellIndex].BulletLevel = HoldingBullet.BulletLevel;
                        JSONDataManager.Instance.SaveData();
                        HoldingBullet = null;
                    }
                    else
                    {
                        if(JSONDataManager.Instance.JSONDATA.Bullets.BulletData[cellIndex].BulletLevel == HoldingBullet.BulletLevel)
                        {
                            if(HoldingBullet.GridPosition == cellIndex)
                            {
                                HoldingBullet.transform.position = _BulletGridController.Grid.Cells[HoldingBullet.GridPosition].CellPosition;
                                HoldingBullet = null;
                                return;
                            }
                            else
                            {
                                BulletGridManager.Instance.Bullets.BulletData[cellIndex].BulletLevel++;
                                BulletGridManager.Instance.Bullets.BulletData[HoldingBullet.GridPosition].BulletLevel = 0;
                                _BulletGridController.BulletControllers.Find(x => x.GridPosition == cellIndex).BulletLevel++;
                                _BulletGridController.BulletControllers.Find(x => x.GridPosition == cellIndex).AssignColor();
                                _BulletGridController.BulletControllers.Remove(HoldingBullet);
                                Destroy(HoldingBullet.gameObject);
                                HoldingBullet = null;
                                JSONDataManager.Instance.SaveData();
                            }
                        }
                        else
                        {
                            HoldingBullet.transform.position = _BulletGridController.Grid.Cells[HoldingBullet.GridPosition].CellPosition;
                            HoldingBullet = null;
                        }
                    }
                }
                else
                {
                    HoldingBullet.transform.position = _BulletGridController.Grid.Cells[HoldingBullet.GridPosition].CellPosition;
                    HoldingBullet = null;
                }
            }
        }
    }
}