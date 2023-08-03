using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PistolAreaController : MonoBehaviour
{
    public List<PistolController> Pistols = new List<PistolController>();
    public List<PistolController> AssignedPistols = new List<PistolController>();

    public BulletGridController BulletGridController;
    public PistolRunner PistolRunner;

    private bool IsPistolSequenceStarted = false;

    private void OnCollisionEnter(Collision collision)
    {
        BulletController controller = collision.collider.gameObject.GetComponent<BulletController>();

        if (controller != null)
        {
            foreach (PistolController pistol in Pistols)
            {
                if(pistol.AsignedBulletLevel == 0)
                {
                    pistol.AsignedBulletLevel = controller.BulletLevel;
                    AssignedPistols.Add(pistol);
                    Pistols.Remove(pistol);
                    break;
                }
            }
            Destroy(controller.gameObject);
        }
    }

    private void Update()
    {
        if(JSONDataManager.Instance.JSONDATA.Bullets.BulletData.Any(x=>x.BulletLevel>0) && BulletGridController.BulletControllers.All(x=>x == null) && !IsPistolSequenceStarted)
        {
            PistolRunner.Pistols = AssignedPistols.ToList();
            foreach (PistolController pistol in AssignedPistols)
            {
                pistol.transform.parent = PistolRunner.PistolHolder.transform;
            }
            //AssignedPistols.Clear();
            IsPistolSequenceStarted = true;
            PistolRunner.StartMovement();
        }
    }
}
