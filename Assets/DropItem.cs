using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DropItemType
{
    Gold,
    Point,
    Item,
}
public class DropItem : MonoBehaviour
{
    public DropItemType type;
    public int amount;
    public int itemId;
    public Color textColor = Color.white;

    bool alreadyDone = false;
    private void OnTriggerEnter(Collider other)
    {
        if (alreadyDone)
            return;

        if(other.CompareTag("Player"))
        {
            alreadyDone = true;
            switch (type)
            {
                case DropItemType.Gold:
                    StageManager.Instance.AddGold(amount);
                    Actor.InstantiateDamageText(amount, transform.position, textColor);
                    break;
                case DropItemType.Point:
                    StageManager.Instance.AddScore(amount);
                    Actor.InstantiateDamageText(amount, transform.position, textColor);
                    break;
                case DropItemType.Item:
                    StageManager.Instance.AddItem(itemId, amount);
                    break;
            }

            Destroy(transform.parent.gameObject);
        }
    }
}
