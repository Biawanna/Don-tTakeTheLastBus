using System.Collections;
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

    private int playerScore;
    private int npcScore;
    private int roundCount;
    private HandSign npcHandSign;
    private DialogueTrigger dialogueTrigger;
    private float rpsWaitTime = 0.1f;

    private void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();

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
        npcScoreText.text = "Scout: 0";

        StartCoroutine(RPSWinRoutine());
    }

    void PlayRound(HandSign playerHandSign)
    {
        // Generate a random hand sign for the NPC
        npcHandSign = (HandSign)Random.Range(0, 3);

        // Determine the result of the round
        GameResult result = GetResult(playerHandSign, npcHandSign);

        // Update the scores and round count
        if (result == GameResult.Win)
        {
            playerScore++;
        }
        else if (result == GameResult.Loss)
        {
            npcScore++;
        }
        roundCount++;
        UpdateScore();
    }

    GameResult GetResult(HandSign playerHandSign, HandSign npcHandSign)
    {
        if (playerHandSign == HandSign.Rock && npcHandSign == HandSign.Scissors ||
            playerHandSign == HandSign.Paper && npcHandSign == HandSign.Rock ||
            playerHandSign == HandSign.Scissors && npcHandSign == HandSign.Paper)
        {
            return GameResult.Win;
        }
        else if (playerHandSign == HandSign.Rock && npcHandSign == HandSign.Paper ||
                 playerHandSign == HandSign.Paper && npcHandSign == HandSign.Scissors ||
                 playerHandSign == HandSign.Scissors && npcHandSign == HandSign.Rock)
        {
            return GameResult.Loss;
        }
        else
        {
            return GameResult.Draw;
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
                    UpdateScore();

                    ToggleRPSButtons(false);
                    ToggleRetryButtons(true);
                    yield break;
                }
                else if (playerScore < npcScore)
                {
                    winText.text = "Scout wins!";
                    UpdateScore();

                    ToggleRPSButtons(false);
                    ToggleRetryButtons(true);
                    yield break;
                }
                else
                {
                    winText.text = "Draw!";
                    UpdateScore();

                    ToggleRPSButtons(false);
                    ToggleRetryButtons(true);
                    yield break;
                }
            }

            yield return new WaitForSeconds(rpsWaitTime);
        }
    }
}
