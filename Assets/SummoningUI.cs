using UnityEngine;

public class SummoningUI : MonoBehaviour
{
    [Header("References")]
    public SummoningManager summoningManager;
    public TurnManager turnManager;
    public GameUI gameUI;

    public void SummonSelectedCard()
    {
        CardInteraction selectedCard = CardInteraction.GetSelectedCard();

        if (selectedCard == null)
        {
            gameUI.ShowMessage("Select a card first!");
            return;
        }

        if (!turnManager.IsPlayerTurn() || !turnManager.IsInActionPhase())
        {
            gameUI.ShowMessage("You can only summon during your action phases!");
            return;
        }

        CardData cardData = selectedCard.GetCardData();

        if (cardData.cardType != CardType.Creature)
        {
            gameUI.ShowMessage("You can only summon creatures!");
            return;
        }

        if (cardData.sacrificeCost > 0)
        {
            gameUI.ShowMessage("This creature requires sacrifices!");
            return;
        }

        if (summoningManager.SummonPlayerCard(cardData, new CardInstance[0]))
            gameUI.ShowMessage(cardData.cardName + " was summoned!");
        else
            gameUI.ShowMessage("Unable to summon this creature!");
    }
}