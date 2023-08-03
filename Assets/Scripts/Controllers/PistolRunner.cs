using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolRunner : MonoBehaviour
{
    public Transform PistolHolder;
    public List<PistolController> Pistols = new List<PistolController>();

    public float Speed = 6f;
    public float SwerveSpeed = 6f;

    public float MaxWidth = 4.5f;

    public bool IsMovementStarted = false;

    private float StartingLocalPistolPosition = 0;

    public bool IsShieldActive = false;
    public GameObject ShieldObject;
    public void StartMovement()
    {
        Camera.main.GetComponent<CinemachineBrain>().ActiveVirtualCamera.Priority = -2;

        for (int i = 0; i < Pistols.Count; i++)
        {
            Pistols[i].IsMovementStarted = true;
            if (i == 0)
            {
                Pistols[i].transform.localPosition = Vector3.zero;
            }
            else
            {
                if(i%2 == 0)
                {
                    Pistols[i].transform.localPosition = new Vector3(0.3f * (i-1) + 0.3f, 0f, -0.1f * (i-1));
                }
                else
                {
                    Pistols[i].transform.localPosition = new Vector3(-0.3f * i - 0.3f, 0f, -0.1f * i);
                }
            }
        }
        IsMovementStarted = true;
    }

    public void ActivateShield()
    {
        IsShieldActive = true;
        ShieldObject.SetActive(true);
        StartCoroutine(ShieldTimer());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartingLocalPistolPosition = PistolHolder.transform.localPosition.x;
        }

        if (IsMovementStarted)
        {
            transform.position += Vector3.forward * Time.deltaTime * Speed;
            Sverwe();
        }
    }

    private void Sverwe()
    {
        Vector2 ScreenSwerveAmount = Input.mousePosition - InputManager.Instance.firstTouchPosition;

        Vector2 UnclampedWorldSwerveAmount = ScreenSwerveAmount / Screen.width;

        Vector2 ClampedSwerveAmount = new Vector2(UnclampedWorldSwerveAmount.x * SwerveSpeed, 0f);

        PistolHolder.transform.localPosition = new Vector3(Mathf.Clamp(StartingLocalPistolPosition + ClampedSwerveAmount.x, -MaxWidth, MaxWidth), PistolHolder.transform.localPosition.y, 0f);
    }

    IEnumerator ShieldTimer()
    {
        yield return new WaitForSeconds(3);
        IsShieldActive = false;
        ShieldObject.SetActive(false);
    }
}