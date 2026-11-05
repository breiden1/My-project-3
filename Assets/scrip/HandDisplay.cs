using System.Collections.Generic;
using UnityEngine;

public class HandDisplay : MonoBehaviour
{
    [Header("References")]
    public DeckManager deckManager;
    public CardDisplay cardPrefab;
    public Transform handRoot;

    [Header("Layout")]
    public float cardSpacing = 1.6f;
    public float handHeight = 0f;

    private readonly List<CardInstance> displayedCards = new List<CardInstance>();

    private void Start()
    {
        RefreshHand();
    }

    private void Update()
    {
        if (deckManager == null)
            return;

        if (HandHasChanged())
            RefreshHand();
    }

    private bool HandHasChanged()
    {
        if (displayedCards.Count != deckManager.hand.Count)
            return true;

        for (int i = 0; i < displayedCards.Count; i++)
        {
            if (displayedCards[i] != deckManager.hand[i])
                return true;
        }

        return false;
    }

    public void RefreshHand()
    {
        if (deckManager == null || cardPrefab == null || handRoot == null)
            return;

        foreach (Transform child in handRoot)
            Destroy(child.gameObject);

        displayedCards.Clear();

        int handCount = deckManager.hand.Count;

        for (int i = 0; i < handCount; i++)
        {
            CardInstance card = deckManager.hand[i];
            CardDisplay display = Instantiate(cardPrefab, handRoot);

            display.cardData = card.cardData;

            SpriteRenderer frameRenderer = display.GetComponent<SpriteRenderer>();
            frameRenderer.sortingOrder = i * 10;

            display.RefreshDisplay();

            float x = (i - (handCount - 1) / 2f) * cardSpacing;
            display.transform.localPosition = new Vector3(x, handHeight, 0f);

            displayedCards.Add(card);
        }
    }
}