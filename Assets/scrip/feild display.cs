using System.Collections.Generic;
using UnityEngine;

public class FieldDisplay : MonoBehaviour
{
    [Header("References")]
    public FieldManager fieldManager;
    public CardDisplay cardPrefab;
    public Transform fieldRoot;

    [Header("Layout")]
    public float cardSpacing = 1.6f;
    public float fieldHeight = 0f;

    private readonly List<CardInstance> displayedCards = new List<CardInstance>();

    private void Start() { RefreshField(); }

    private void Update()
    {
        if (fieldManager != null && FieldHasChanged()) RefreshField();
    }

    private bool FieldHasChanged()
    {
        if (displayedCards.Count != fieldManager.creatures.Count) return true;
        for (int i = 0; i < displayedCards.Count; i++)
            if (displayedCards[i] != fieldManager.creatures[i]) return true;
        return false;
    }

    public void RefreshField()
    {
        if (fieldManager == null || cardPrefab == null || fieldRoot == null) return;

        foreach (Transform child in fieldRoot) Destroy(child.gameObject);
        displayedCards.Clear();

        int creatureCount = fieldManager.creatures.Count;

        for (int i = 0; i < creatureCount; i++)
        {
            CardInstance card = fieldManager.creatures[i];
            CardDisplay display = Instantiate(cardPrefab, fieldRoot);
            display.cardData = card.cardData;

            SpriteRenderer frameRenderer = display.GetComponent<SpriteRenderer>();
            frameRenderer.sortingOrder = i * 10;

            display.RefreshDisplay();

            float x = (i - (creatureCount - 1) / 2f) * cardSpacing;
            display.transform.localPosition = new Vector3(x, fieldHeight, 0f);

            displayedCards.Add(card);
        }
    }
}
