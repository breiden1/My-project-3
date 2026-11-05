using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public int maxHp = 80;
    public int currentHp = 20;

    public void TakeDamage(int amount)
    {
        if (amount <= 0 || currentHp <= 0)

        currentHp = Mathf.Max(0, currentHp - amount);
    }

    public void Heal(int amount)
    {
        if (amount <= 0 || currentHp <= 0)

        currentHp = Mathf.Min(maxHp, currentHp + amount);
    }

    public void ResetHealth()
    {
        currentHp = 20;
    }
}
