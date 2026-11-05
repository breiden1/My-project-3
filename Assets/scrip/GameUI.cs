using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [Header("Player References")]
    public PlayerState player;
    public PlayerState aiPlayer;

    [Header("UI Text")]
    public TMP_Text playerHpText;
    public TMP_Text aiHpText;
    public TMP_Text phaseText;
    public TMP_Text messageText;

    public void RefreshUI()
    {
        playerHpText.text = "Player HP: " + player.currentHp;
        aiHpText.text = "AI HP: " + aiPlayer.currentHp;
    }

    public void SetPhase(string phase)
    {
        phaseText.text = "Phase: " + phase;
    }

    public void ShowMessage(string message)
    {
        messageText.text = message;
    }

    private void Start()
    {
        RefreshUI();
        SetPhase("Draw");
        ShowMessage("Game started!");
    }
}