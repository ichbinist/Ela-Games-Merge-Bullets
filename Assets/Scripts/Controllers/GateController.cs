using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GateController : MonoBehaviour
{
    public TextMeshProUGUI TopBannerText;
    public Image TopBannerImage;
    public Image BackgroundImage;
    public TextMeshProUGUI AmountText;


    public float ChangeAmount;
    public GateType GateType;
    public ModificationType ModificationType;


    private void OnEnable()
    {
        SetGateSettings();
    }

    public void SetGateSettings()
    {
        float localAmount = ChangeAmount * 100f;
        switch (ModificationType)
        {
            case ModificationType.Decrease:
                AmountText.SetText("-" + localAmount.ToString());
                TopBannerImage.color = Color.red;
                BackgroundImage.color = new Color(1,0,0,0.35f);
                break;
            case ModificationType.Increase:
                AmountText.SetText("+" + localAmount.ToString());
                TopBannerImage.color = Color.green;
                BackgroundImage.color = new Color(0, 1, 0, 0.35f);
                break;
            case ModificationType.Special:
                AmountText.SetText(" ");
                TopBannerImage.color = Color.yellow;
                BackgroundImage.color = new Color(1, 0.92f, 0.016f, 0.35f);
                break;
            default:
                break;
        }

        switch (GateType)
        {
            case GateType.BulletSize:
                TopBannerText.SetText("BULLET SIZE");
                break;
            case GateType.FireRate:
                TopBannerText.SetText("FIRE RATE");
                break;
            case GateType.Range:
                TopBannerText.SetText("RANGE");
                break;
            case GateType.TripleShot:
                TopBannerText.SetText("TRIPLE SHOT");
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PistolRunner runner = other.GetComponentInParent<PistolRunner>();
        BulletController bullet = other.GetComponentInParent<BulletController>();

        if (runner != null)
        {
            foreach (PistolController pistol in runner.Pistols)
            {
                if(GateType == GateType.TripleShot)
                {
                    pistol.TripleShot = true;
                }

                if(GateType == GateType.FireRate)
                {
                    if(ModificationType == ModificationType.Decrease)
                    {
                        pistol.AdditionalFireSpeed -= ChangeAmount;
                    }
                    else
                    {
                        pistol.AdditionalFireSpeed += ChangeAmount;
                    }
                }

                if (GateType == GateType.Range)
                {
                    if (ModificationType == ModificationType.Decrease)
                    {
                        pistol.Range -= ChangeAmount;
                    }
                    else
                    {
                        pistol.Range += ChangeAmount;
                    }
                }

                if (GateType == GateType.BulletSize)
                {
                    if (ModificationType == ModificationType.Decrease)
                    {
                        pistol.BulletSize -= ChangeAmount * 0.1f;
                    }
                    else
                    {
                        pistol.BulletSize += ChangeAmount * 0.1f;
                    }
                }
            }

            Destroy(gameObject);
        }

        if(bullet != null)
        {
            Destroy(bullet.gameObject);

            if(ModificationType == ModificationType.Increase)
            {
                ChangeAmount += 0.01f;
            }
            else if(ModificationType == ModificationType.Decrease)
            {
                ChangeAmount -= 0.01f;
                if(ChangeAmount <= 0)
                {
                    ModificationType = ModificationType.Increase;
                    ChangeAmount = 0;
                }
            }

            SetGateSettings();
        }
    }
}
public enum GateType
{
    BulletSize,
    FireRate,
    Range,
    TripleShot
}

public enum ModificationType
{
    Decrease,
    Increase,
    Special
}