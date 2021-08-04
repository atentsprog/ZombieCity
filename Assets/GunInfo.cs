using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInfo : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPosition;
    public GameObject bulletLight;
    public AnimatorOverrideController animatorOverride;
    public float delay = 0.2f;
    public int maxBulletCount = 6;
    public float power = 20;
}
