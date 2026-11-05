
using UnityEngine;

public enum CardType { Creature, Effect }
public enum StarterType { FastStarter, SlowStarter }
public enum EffectType { Eternal, Instant, Specified, Attachable }

[CreateAssetMenu(fileName = "New Card", menuName = "Conjuris/Card")]
public class CardData : ScriptableObject
{
    [Header("Card Information")]
    public string cardName;
    public Sprite artwork;
    [TextArea(2, 5)] public string description;
    public CardType cardType;

    [Header("Creature Stats")]
    public StarterType starterType;
    public int attack;
    public int defense;
    public int hp;

    [Header("Sacrifice")]
    [Min(0)] public int sacrificeCost;
    [Min(0)] public int sacrificeWorth;

    [Header("Effect Card Settings")]
    public EffectType effectType;
    [TextArea(3, 8)] public string ability;

    [Header("Deck Rules")]
    [Min(1)] public int maxCopiesInDeck = 3;
}

