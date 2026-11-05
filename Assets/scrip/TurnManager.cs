using UnityEngine;

public enum TurnPhase { Draw, ActionSegment1, Attack, ActionSegment2 }

public class TurnManager : MonoBehaviour
{
    [Header("Player References")]
    public DeckManager playerDeck;
    public FieldManager playerField;
    [Header("AI References")]
    public DeckManager aiDeck;
    public FieldManager aiField;
    [Header("UI")]
    public GameUI gameUI;
    [Header("Turn Settings")]
    public bool playerTurn = true;
    public TurnPhase currentPhase = TurnPhase.Draw;

    private void Start() { BeginTurn(); }

    public void BeginTurn()
    {
        currentPhase = TurnPhase.Draw;
        if (playerTurn)
        {
            playerDeck.DrawCard();
            foreach (CardInstance card in playerField.creatures) card.ResetTurn();
        }
        else
        {
            aiDeck.DrawCard();
            foreach (CardInstance card in aiField.creatures) card.ResetTurn();
        }
        UpdateUI();
    }

    public void AdvancePhase()
    {
        switch (currentPhase)
        {
            case TurnPhase.Draw: currentPhase = TurnPhase.ActionSegment1; break;
            case TurnPhase.ActionSegment1: currentPhase = TurnPhase.Attack; break;
            case TurnPhase.Attack: currentPhase = TurnPhase.ActionSegment2; break;
            case TurnPhase.ActionSegment2: EndTurn(); return;
        }
        UpdateUI();
    }

    public void EndTurn() { playerTurn = !playerTurn; BeginTurn(); }

    private void UpdateUI()
    {
        string phaseName = currentPhase switch
        {
            TurnPhase.Draw => "Draw",
            TurnPhase.ActionSegment1 => "Action Segment 1",
            TurnPhase.Attack => "Attack Segment",
            TurnPhase.ActionSegment2 => "Action Segment 2",
            _ => "Unknown"
        };
        gameUI.SetPhase((playerTurn ? "Player - " : "AI - ") + phaseName);
    }

    public bool IsPlayerTurn() { return playerTurn; }
    public bool IsInActionPhase() { return currentPhase == TurnPhase.ActionSegment1 || currentPhase == TurnPhase.ActionSegment2; }
    public bool IsInAttackPhase() { return currentPhase == TurnPhase.Attack; }
}