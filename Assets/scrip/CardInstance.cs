
using System;
using UnityEngine;

[Serializable]
public class CardInstance
{
    public CardData cardData;
    public int currentHp;
    public int currentDefense;
    public bool isProne;
    public bool hasAttacked;
    public bool hasUsedAbility;
    public bool canAct;

    public CardInstance(CardData data)
    {
        cardData = data;
        currentHp = data.hp;
        currentDefense = data.defense;
        isProne = false;
        hasAttacked = false;
        hasUsedAbility = false;
        canAct = false;
    }

    public bool IsDestroyed() => currentHp <= 0;

    public void TakeDamage(int damage)
    {
        if (damage <= 0 || IsDestroyed()) return;
        currentHp = Mathf.Max(0, currentHp - damage);
    }

    public void RestoreHp(int amount)
    {
        if (amount <= 0 || IsDestroyed()) return;
        currentHp += amount;
    }

    public void ResetTurn()
    {
        hasAttacked = false;
        hasUsedAbility = false;
        isProne = false;
        currentDefense = cardData.defense;
    }
}
