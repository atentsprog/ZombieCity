﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public int hp = 100;
    public float bloodEffectYPosition = 1.3f;

    public GameObject bloodParticle;
    protected Animator animator;
    protected void CreateBloodEffect()
    {
        var pos = transform.position;
        pos.y = bloodEffectYPosition;
        Instantiate(bloodParticle, pos, Quaternion.identity);
    }

    protected void CreateTextEffect(int number, Color color)
    {
        GameObject memoryGo = (GameObject)Resources.Load("TextEffect");
        GameObject go = Instantiate(memoryGo, transform.position, Camera.main.transform.rotation);
        TextMeshPro textMeshPro = go.GetComponent<TextMeshPro>();
        textMeshPro.text = number.ToNumber();
        textMeshPro.color = color;
    }
    public Color damageColor = Color.white;
    protected void TakeHit(int damage)
    {
        hp -= damage;
        CreateBloodEffect();
        CreateTextEffect(damage, damageColor);
    }
}
