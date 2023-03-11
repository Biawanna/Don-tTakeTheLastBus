using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public enum HandSign
{
    Rock,
    Paper,
    Scissors
}

public enum GameResult
{
    Win,
    Loss,
    Draw
}

public class RPSGame : MonoBehaviour
{
    [SerializeField] private GameObject rpsButtons;
    [SerializeField] private GameObject retryButtons;

    public TextMeshProUGUI winText;
    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI npcScoreText;
    public int numRounds = 3;

    private float rpsWaitTime = 0.1f;
    private int playerScore;
    private int npcScore;
    private int roundCount;
    private HandSign npcHandSign;
    private DialogueTrigger dialogueTrigger;
    private DialogueManager dialogueManager;

    private void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
        dialogueManager = DialogueManager.instance;

        ToggleRetryButtons(false);
        ToggleRPSButtons(false);
        ClearRPSText();
    }

    public void NewGame()
    {
        dialogueTrigger.ToggleYesNoButtons(false);
        ToggleRetryButtons(false);
        ToggleRPSButtons(true);

        // Reset the scores and round count
        playerScore = 0;
        npcScore = 0;
        roundCount = 0;

        // Reset the text labels
        winText.text = "";
        playerScoreText.text = "Player: 0";
        npcScoreText.text = dialogueTrigger.CurrentDialogue.dialogueType.ToString() + ": 0";

        StartCoroutine(RPSWinRoutine());
    }

    void PlayRound(HandSign playerHandSign)
    {
        // Generate a random hand sign for the NPC
        npcHandSign = (HandSign)Random.Range(0, 3);

        // Determine the result of the round
        GameResult result = GetResult(playerHandSign, npcHandSign);

        // Update the scores and round count based on the result
        switch (result)
        {
            case GameResult.Win:
                playerScore++;
                winText.text = "Point player";
                roundCount++;

                break;
            case GameResult.Loss:
                npcScore++;
                winText.text = "Point " + dialogueTrigger.CurrentDialogue.dialogueType.ToString();
                roundCount++;

                break;
            case GameResult.Draw:
                winText.text = "Draw";

                break;
        }
        UpdateScore();
    }


    GameResult GetResult(HandSign playerHandSign, HandSign npcHandSign)
    {
        // Define the winning combinations of hand signs
        Dictionary<HandSign, HandSign> winningCombinations = new Dictionary<HandSign, HandSign>
    {
        { HandSign.Rock, HandSign.Scissors },
        { HandSign.Paper, HandSign.Rock },
        { HandSign.Scissors, HandSign.Paper }
    };

        // Determine the result of the round based on the hand signs
        if (playerHandSign == npcHandSign)
        {
            return GameResult.Draw;
        }
        else if (winningCombinations[playerHandSign] == npcHandSign)
        {
            return GameResult.Win;
        }
        else
        {
            return GameResult.Loss;
        }
    }


    public void OnRockClick()
    {
        PlayRound(HandSign.Rock);
    }

    public void OnPaperClick()
    {
        PlayRound(HandSign.Paper);
    }

    public void OnScissorsClick()
    {
        PlayRound(HandSign.Scissors);
    }
    public void ClearRPSText()
    {
        winText.text = "";
        playerScoreText.text = "";
        npcScoreText.text = "";
    }

    public void EndRPSGame()
    {
        ToggleRetryButtons(false);
        ClearRPSText();
        dialogueTrigger.CloseDialogue();
    }

    private void PlayerWins()
    {
        ToggleRPSButtons(false);
        ToggleRetryButtons(true);

        if (!dialogueManager.InventoryScriptableObject.headPhones)
        {
            ToggleRetryButtons(false);
            dialogueTrigger.ToggleDialogueOptions(false);
            dialogueTrigger.IncreaseCurrentDialogueIndex();
            ClearRPSText();
            dialogueTrigger.OpenDialogue();
        }
    }

    private void PlayerLosesOrDraw()
    {
        ToggleRPSButtons(false);
        ToggleRetryButtons(true);
    }

    public void ToggleRetryButtons(bool showOptions)
    {
        dialogueTrigger.ToggleObjectVisibilty(showOptions, retryButtons);
    }
    private void ToggleRPSButtons(bool showOptions)
    {
        dialogueTrigger.ToggleObjectVisibilty(showOptions, rpsButtons);
    }

    private void UpdateScore()
    {
        playerScoreText.text = "Player: " + playerScore;
        npcScoreText.text = "Scout: " + npcScore;
    }

    private IEnumerator RPSWinRoutine()
    {
        while(true)
        {

            // Check if the game is over
            if (roundCount >= numRounds)
            {
                // Determine the winner
                if (playerScore > npcScore)
                {
                    winText.text = "You win!";
                    PlayerWins();
                   
                    yield break;
                }
                else if (playerScore < npcScore)
                {
                    winText.text = dialogueTrigger.CurrentDialogue.dialogueType.ToString() + " wins!";
                    PlayerLosesOrDraw();
                   
                    yield break;
                }
                else
                {
                    winText.text = "Draw!";
                    PlayerLosesOrDraw();

                    yield break;
                }
            }

            yield return new WaitForSeconds(rpsWaitTime);
        }
    }
}
