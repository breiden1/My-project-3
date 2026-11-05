using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public int maxCreatures = 7;
    public List<CardInstance> creatures = new List<CardInstance>();

    public bool AddCreature(CardInstance card)
    {
        if (creatures.Count >= maxCreatures)
            return false;

        if (card.cardData.cardType != CardType.Creature)
            return false;

        creatures.Add(card);
        return true;
    }

    public void RemoveCreature(CardInstance card)
    {
        creatures.Remove(card);
    }

    public bool HasSpace()
    {
        return creatures.Count < maxCreatures;
    }

    public bool ContainsCreature(CardInstance card)
    {
        return creatures.Contains(card);
    }
}