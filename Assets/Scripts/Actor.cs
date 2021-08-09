using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public int hp = 100;
    public int MaxHp;
    public float bloodEffectYPosition = 1.3f;

    public GameObject bloodParticle;
    protected Animator animator;

    protected void Awake()
    {
        MaxHp = hp;
    }
    protected void CreateBloodEffect()
    {
        var pos = transform.position;
        pos.y = bloodEffectYPosition;
        Instantiate(bloodParticle, pos, Quaternion.identity);
    }

    public static void CreateTextEffect(int number, Vector3 position, Color color)
    {
        CreateTextPrefab("TextEffect", number.ToNumber(), position, color);
    }

    public void CreateTalkText(string text, Color color)
    {
        CreateTextPrefab("TalkText", text, transform.position, color, transform);
    }

    private static void CreateTextPrefab(string prefabName, string text, Vector3 position, Color color, Transform parent = null)
    {
        GameObject memoryGo = (GameObject)Resources.Load(prefabName);
        GameObject go = Instantiate(memoryGo, position, Camera.main.transform.rotation);
        go.transform.SetParent(parent);
        TextMeshPro textMeshPro = go.GetComponentInChildren<TextMeshPro>();
        textMeshPro.text = text;
        textMeshPro.color = color;
    }

    public Color damageColor = Color.white;
    protected void TakeHit(int damage)
    {
        hp -= damage;
        CreateBloodEffect();
        CreateTextEffect(damage, transform.position, damageColor);
    }
}
