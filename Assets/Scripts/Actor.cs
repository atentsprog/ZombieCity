using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public int power = 20;

    public int hp = 100;
    protected Animator animator;

    public float bloodEffectYPosition = 1.3f;
    public GameObject bloodParticle;

    protected void CreateBloodEffect()
    {
        var pos = transform.position;
        pos.y = bloodEffectYPosition;
        Instantiate(bloodParticle, pos, Quaternion.identity);
    }

    protected void TakeHit(int damage)
    {
        hp -= damage;
    }
}
