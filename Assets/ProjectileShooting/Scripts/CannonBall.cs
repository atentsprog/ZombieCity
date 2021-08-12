using UnityEngine;
using System.Collections;

public class CannonBall : MonoBehaviour 
{
    [SerializeField]
    GameObject deathEffect;

    public float explodeDelay = 3;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(explodeDelay);

        Instantiate(deathEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
    //void OnCollisionEnter(Collision collision)
    //{
    //    Instantiate(deathEffect, transform.position, Quaternion.LookRotation(collision.contacts[0].normal));

    //    Destroy(gameObject);
    //}
}
