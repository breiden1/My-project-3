
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [Header("Deck Setup")]
    public List<CardData> startingDeck = new List<CardData>();
    public List<CardInstance> deck = new List<CardInstance>();
    public List<CardInstance> hand = new List<CardInstance>();

    public int startingHandSize = 4;
    public int maxHandSize = 10;

    private void Start()
    {
        StartGame();
    }

    public void StartGame()
    {
        deck.Clear();
        hand.Clear();

        foreach (CardData card in startingDeck)
            deck.Add(new CardInstance(card));

        ShuffleDeck();
        DrawStartingHand();
    }

    public void ShuffleDeck()
    {
        for (int i = deck.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            CardInstance temp = deck[i];
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
    }

    public void DrawStartingHand()
    {
        for (int i = 0; i < startingHandSize; i++)
        {
            if (DrawCard() == null)
                break;
        }
    }

    public CardInstance DrawCard()
    {
        if (deck.Count == 0 || hand.Count >= maxHandSize)
            return null;

        CardInstance drawnCard = deck[0];
        deck.RemoveAt(0);
        hand.Add(drawnCard);

        return drawnCard;
    }
}
