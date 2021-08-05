using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public int hp = 100;
    public float bloodEffectYPosition = 1.3f;

    public GameObject bloodParticle;

    public Color damgeTextColor = Color.white;
    protected Animator animator;
    protected void CreateBloodEffect()
    {
        var pos = transform.position;
        pos.y = bloodEffectYPosition;
        Instantiate(bloodParticle, pos, Quaternion.identity);
    }

    protected void TakeHit(int damage)
    {
        hp -= damage;

        InstantiateDamageText(damage);
    }

    protected void InstantiateDamageText(int damage)
    {
        GameObject momoryObject = (GameObject)Resources.Load("TextEffect");
        GameObject damgeTextGo = Instantiate(momoryObject, transform.position, Quaternion.identity);
        TextMeshPro text = damgeTextGo.GetComponent<TextMeshPro>();
        text.color = damgeTextColor;
        text.text = damage.ToNumber();
    }

}
