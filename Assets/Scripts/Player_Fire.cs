using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Actor
{
    public GameObject bullet;
    public Transform bulletPosition;
    public int bulletCount = 25;         // 전체 총알 수
    public int bulletCountInClip = 20;   // 탄창안의 총알 수
    public int bulletMaxCountInClip = 20; // 탄창에 장전 가능한 총알 수

    float shootDelayEndTime;
    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.LogWarning("총알 리로드");
            ReloadBullet();
        }

        if (Input.GetMouseButton(0))
        {
            if (bulletCountInClip == 0)
            {
                Debug.LogWarning("리로드 해주세요");
            }
            else
            {
                if (shootDelayEndTime < Time.time)
                {
                    bulletCountInClip--;
                    RefreshBulletCountUI();

                    animator.SetBool("Fire", true);
                    shootDelayEndTime = Time.time + shootDelay;
                    IncreaseRecoil();
                    StartCoroutine(InstantiateBulletAndFlashBulletCo());
                }
            }
        }
        else
        {
            animator.SetBool("Fire", false);
            DecreaseRecoil();
        }
    }

    private void RefreshBulletCountUI()
    {
        print($"bulletCount:{bulletCount}, bulletCountInClip:{bulletCountInClip}, bulletMaxCountInClip:{bulletMaxCountInClip}");
        //bulletCount    // 전체 총알 수
        //bulletCountInClip    // 탄창안의 총알 수
        //bulletMaxCountInClip  // 탄창에 장전 가능한 총알 수
    }

    bool ingReloadBullet;
    private void ReloadBullet()
    {
        if (ingReloadBullet)
        {
            Debug.LogWarning("이미 총알 장전중입니다");
            return;
        }
        StartCoroutine(ReloadBulletCo());
    }

    public float reloadTime = 1f;
    private IEnumerator ReloadBulletCo()
    {
        ingReloadBullet  = true;
        animator.SetTrigger("Reload");
        yield return new WaitForSeconds(reloadTime);
        ingReloadBullet = false;

        int reloadCount = Mathf.Min(bulletMaxCountInClip, bulletCount);
        bulletCountInClip = reloadCount;
        bulletCount -= reloadCount;
        RefreshBulletCountUI();
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
