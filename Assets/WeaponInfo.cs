using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "New Weapon Info"
    , menuName = "Scriptable Object/Weapon Info")]
public class WeaponInfo : ScriptableObject
{
    public int damage = 20;
    public AnimatorOverrideController overrideAnimator;
    public GameObject weaponGo;


    public GameObject bullet;
    public Transform bulletPosition;
    public GameObject bulletLight;

    public float delay = 0.2f;
    public int maxBulletCount = 6;
}
