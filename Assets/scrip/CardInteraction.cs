using UnityEngine;

public class CardInteraction : MonoBehaviour
{
    public float selectionHeight = 0.3f;

    private static CardInteraction selectedCard;
    private Vector3 originalPosition;
    private int originalSortingOrder;
    private bool isSelected;

    private SpriteRenderer frameRenderer;
    private CardDisplay cardDisplay;

    private void Start()
    {
        originalPosition = transform.localPosition;
        frameRenderer = GetComponent<SpriteRenderer>();
        cardDisplay = GetComponent<CardDisplay>();
        originalSortingOrder = frameRenderer.sortingOrder;
    }

    private void OnMouseDown()
    {
        if (transform.parent.GetComponent<FieldDisplay>() != null)
            return;

        if (selectedCard == this)
        {
            SetSelected(false);
            selectedCard = null;
        }
        else
        {
            if (selectedCard != null)
                selectedCard.SetSelected(false);

            selectedCard = this;
            SetSelected(true);
        }
    }

    private void SetSelected(bool selected)
    {
        isSelected = selected;
        transform.localPosition = originalPosition + Vector3.up * (isSelected ? selectionHeight : 0f);
        frameRenderer.sortingOrder = isSelected ? 10000 : originalSortingOrder;
        cardDisplay.RefreshDisplay();
    }

    public static CardInteraction GetSelectedCard()
    {
        return selectedCard;
    }

    public CardData GetCardData()
    {
        return cardDisplay.cardData;
    }
}