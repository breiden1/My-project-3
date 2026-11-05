using System.Collections.Generic;
using UnityEngine;

public class SummoningManager : MonoBehaviour
{
    [Header("Player References")]
    public DeckManager playerDeck;
    public FieldManager playerField;
    public List<CardInstance> playerSacrificeZone = new List<CardInstance>();

    [Header("AI References")]
    public DeckManager aiDeck;
    public FieldManager aiField;
    public List<CardInstance> aiSacrificeZone = new List<CardInstance>();

    public bool SummonPlayerCard(CardData cardData, CardInstance[] sacrifices)
    {
        return SummonCard(cardData, sacrifices, playerDeck, playerField, playerSacrificeZone);
    }

    public bool SummonAICard(CardData cardData, CardInstance[] sacrifices)
    {
        return SummonCard(cardData, sacrifices, aiDeck, aiField, aiSacrificeZone);
    }

    private bool SummonCard(CardData cardData, CardInstance[] sacrifices, DeckManager deck, FieldManager field, List<CardInstance> sacrificeZone)
    {
        if (cardData.cardType != CardType.Creature)
            return false;

        CardInstance summonedCard = null;

        foreach (CardInstance card in deck.hand)
        {
            if (card.cardData == cardData)
            {
                summonedCard = card;
                break;
            }
        }

        if (summonedCard == null)
            return false;

        int sacrificeWorth = 0;

        foreach (CardInstance sacrifice in sacrifices)
        {
            if (!field.ContainsCreature(sacrifice))
                return false;

            if (sacrifices.Length > 1)
            {
                for (int i = 0; i < sacrifices.Length; i++)
                {
                    for (int j = i + 1; j < sacrifices.Length; j++)
                    {
                        if (sacrifices[i] == sacrifices[j])
                            return false;
                    }
                }
            }

            sacrificeWorth += sacrifice.cardData.sacrificeWorth;
        }

        if (sacrificeWorth < cardData.sacrificeCost)
            return false;

        if (field.creatures.Count - sacrifices.Length >= field.maxCreatures)
            return false;

        foreach (CardInstance sacrifice in sacrifices)
        {
            field.RemoveCreature(sacrifice);
            sacrificeZone.Add(sacrifice);
        }

        deck.hand.Remove(summonedCard);
        field.AddCreature(summonedCard);

        return true;
    }
}