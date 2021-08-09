using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Actor
{
    public int bulletCountInClip = 2;       // 탄창에 총알수
    public int MaxBulletCountInClip = 6;    // 탄창에 들어가는 최대수
    public int allBulletCount = 500;       // 가진 전체 총알수.
    public int MaxBulletCount;
    public float reloadTime = 1f;

    public GameObject bullet;
    public Transform bulletPosition;


    float shootDelayEndTime;
    void Fire()
    {
        if (Input.GetMouseButton(0))
        {
            isFiring = true;
            if (shootDelayEndTime < Time.time)
            {
                if (bulletCountInClip > 0)
                {
                    bulletCountInClip--;
                    AmmoUI.Instance.SetGuage(bulletCountInClip, MaxBulletCountInClip
                        , allBulletCount + bulletCountInClip, MaxBulletCount);
                    animator.SetTrigger("StartFire");
                    //animator.SetBool("Fire", true);

                    shootDelayEndTime = Time.time + shootDelay;
                    switch (currentWeapon.type)
                    {
                        case WeaponInfo.WeaponType.Gun:
                            IncreaseRecoil();
                            currentWeapon.StartCoroutine(InstantiateBulletAndFlashBulletCo());
                            break;

                        case WeaponInfo.WeaponType.Melee:
                            // 무기의 컬라이더를 활성화 하자. 
                            currentWeapon.StartCoroutine(MeleeAttackCo());
                            break;
                    }
                }
                else
                {
                    if (reloadTextDelayEndTime < Time.time)
                    {
                        reloadTextDelayEndTime = Time.time + reloadTextDelay;
                        CreateTalkText("Reload!", Color.red);
                    }
                }
            }
        }
        else
        {
            Endfiring();
        }
    }
    public float reloadTextDelay = 2;
    float reloadTextDelayEndTime;

    private IEnumerator MeleeAttackCo()
    {
        yield return new WaitForSeconds(currentWeapon.attackStartTime);
        currentWeapon.attackCollider.enabled = true;
        yield return new WaitForSeconds(currentWeapon.attackTime);
        currentWeapon.attackCollider.enabled = false;
    }

    private void Endfiring()
    {
        //animator.SetBool("Fire", false);
        DecreaseRecoil();
        isFiring = false;
    }

    GameObject bulletLight;
    public float bulletFlashTime = 0.001f;
    private IEnumerator InstantiateBulletAndFlashBulletCo()
    {
        yield return null; // 총쏘는 애니메이션 시작후에 총알 발사하기 위해서 1Frame쉼
        Instantiate(bullet, bulletPosition.position, CalculateRecoil(transform.rotation));

        bulletLight.SetActive(true);
        yield return new WaitForSeconds(bulletFlashTime);
        bulletLight.SetActive(false);
    }

    float recoilValue = 0f;
    float recoilMaxValue = 1.5f;
    float recoilLerpValue = 0.1f;
    void IncreaseRecoil()
    {
        recoilValue = Mathf.Lerp(recoilValue, recoilMaxValue, recoilLerpValue);
    }
    void DecreaseRecoil()
    {
        recoilValue = Mathf.Lerp(recoilValue, 0, recoilLerpValue);

    }

    Vector3 recoil;
    Quaternion CalculateRecoil(Quaternion rotation)
    {
        recoil = new Vector3(Random.Range(-recoilValue, recoilValue), Random.Range(-recoilValue, recoilValue), 0);
        return Quaternion.Euler(rotation.eulerAngles + recoil);
    }

    [SerializeField] float shootDelay = 0.05f;
}
