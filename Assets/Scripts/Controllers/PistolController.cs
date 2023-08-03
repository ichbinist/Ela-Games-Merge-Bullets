using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolController : MonoBehaviour
{
    public bool IsMovementStarted;
    public BulletController AssignedBullet;

    public float StartingFireSpeed = 1f;
    public float AdditionalFireSpeed = 0f;
}
