using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TicTacToeGame : MonoBehaviour
{
    [Header("Tic Tac Toe Settings")]
    [SerializeField] private string ticTacToeDifficultyLevel;
    [SerializeField] private string playerSymbol = "X";
    [SerializeField] private string aiSymbol = "O";

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private GameObject boardGameObject;
    [SerializeField] private GameObject retryButtons;
    [SerializeField] private Button[] boardButtons;

    private string[] board = new string[9];
    private string aiName;
    private string playersName = "Player";
    private bool playerTurn = true;
    private bool playerMoved = false;
    private DialogueTrigger dialogueTrigger;
    private DialogueManager dialogueManager;
    private float aiDelay = 0.5f;

    void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
        dialogueManager = DialogueManager.instance;

        for (int i = 0; i < board.Length; i++)
        {
            board[i] = "";
        }

        aiName = dialogueTrigger.CurrentDialogue.dialogueType.ToString();
        boardGameObject.SetActive(false);
        retryButtons.SetActive(false);
        ClearTicTacToeText();
    }


    bool CheckForGameOver()
    {
        if (CheckForWin(playerSymbol))
        {
            gameOverText.text = playersName + " Wins!";
            PlayerWinsTicTacToe();

            return true;
        }

        if (CheckForWin(aiSymbol))
        {
            gameOverText.text = aiName + " Wins!";
            retryButtons.SetActive(true);

            return true;
        }

        if (CheckForTie())
        {
            gameOverText.text = "Tie!";
            retryButtons.SetActive(true);

            return true;
        }
        return false;
    }

    bool CheckForWin(string symbol)
    {
        for (int i = 0; i < 9; i += 3)
        {
            if (board[i] == symbol && board[i + 1] == symbol && board[i + 2] == symbol)
            {
                return true;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (board[i] == symbol && board[i + 3] == symbol && board[i + 6] == symbol)
            {
                return true;
            }
        }

        if (board[0] == symbol && board[4] == symbol && board[8] == symbol)
        {
            return true;
        }

        if (board[2] == symbol && board[4] == symbol && board[6] == symbol)
        {
            return true;
        }

        return false;
    }

    bool CheckForTie()
    {
        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] == "")
            {
                return false;
            }
        }

        return true;
    }

    public void OnButtonClick(Button button)
    {
        int index = int.Parse(button.name);

        if (board[index] == "" && playerTurn && !playerMoved)
        {
            board[index] = playerSymbol;
            button.GetComponentInChildren<TextMeshProUGUI>().text = playerSymbol;

            if (CheckForGameOver())
            {
                return;
            }

            playerMoved = true;
            playerTurn = false;
            StartCoroutine(DoAITurnWithDelay());
        }
    }


    void DoAITurn(int difficultyLevel)
    {
        int bestScore = int.MinValue;
        int bestMove = -1;

        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] == "")
            {
                board[i] = aiSymbol;
                int score = Minimax(board, 0, false, difficultyLevel);
                board[i] = "";

                if (score > bestScore)
                {
                    bestScore = score;
                    bestMove = i;
                }
            }
        }

        board[bestMove] = aiSymbol;
        boardButtons[bestMove].GetComponentInChildren<TextMeshProUGUI>().text = aiSymbol;

        if (!CheckForGameOver())
        {
            playerTurn = true;
        }
    }


    Button buttonAtIndex(int index)
    {
        return transform.Find(index.ToString()).GetComponent<Button>();
    }

    public void NewTicTacToeGame()
    {
        boardGameObject.SetActive(true);
        retryButtons.SetActive(false);
        RestartTicTacToe();
    }

    public void EndTicTacToe()
    {
        boardGameObject.SetActive(false);
        retryButtons.SetActive(false);
        dialogueTrigger.ToggleYesNoButtons(false);
        ClearTicTacToeText();

        dialogueTrigger.CloseDialogue();
    }

    public void RestartTicTacToe()
    {
        playerMoved = false;
        playerTurn = true;

        // Reset the board array to its initial state
        for (int i = 0; i < board.Length; i++)
        {
            board[i] = "";
        }

        // Clear the text of all buttons and reset their names
        for (int i = 0; i < boardButtons.Length; i++)
        {
            boardButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
            boardButtons[i].name = i.ToString();
        }

        // Reset other UI elements
        dialogueTrigger.ToggleYesNoButtons(false);
        gameOverText.text = playersName + "'s turn";
    }
    private void ClearTicTacToeText()
    {
        gameOverText.text = "";
        dialogueManager.SetDialogueInPlay(false);
    }

    private void PlayerWinsTicTacToe()
    {
        retryButtons.SetActive(true);

        if (!dialogueManager.InventoryScriptableObject.coconut)
        {
            retryButtons.SetActive(false);

            dialogueTrigger.ToggleDialogueOptions(false);
            dialogueTrigger.IncreaseCurrentDialogueIndex();
            boardGameObject.SetActive(false);
            ClearTicTacToeText();
            dialogueTrigger.OpenDialogue();
        }
    }
    int Minimax(string[] board, int depth, bool maximizingPlayer, int difficultyLevel)
    {
        if (CheckForWin(aiSymbol))
        {
            return 10 - depth;
        }

        if (CheckForWin(playerSymbol))
        {
            return -10 + depth;
        }

        if (CheckForTie())
        {
            return 0;
        }

        if (depth >= difficultyLevel)
        {
            return 0;
        }

        int bestScore = maximizingPlayer ? int.MinValue : int.MaxValue;

        for (int i = 0; i < board.Length; i++)
        {
            if (board[i] == "")
            {
                board[i] = maximizingPlayer ? aiSymbol : playerSymbol;
                int score = Minimax(board, depth + 1, !maximizingPlayer, difficultyLevel);
                board[i] = "";

                if (maximizingPlayer)
                {
                    bestScore = Mathf.Max(bestScore, score);
                }
                else
                {
                    bestScore = Mathf.Min(bestScore, score);
                }
            }
        }

        return bestScore;
    }

    private int Evaluate(string[] board, string playerSymbol, string aiSymbol)
    {
        int score = 0;

        // Check rows
        for (int i = 0; i < 9; i += 3)
        {
            if (board[i] == aiSymbol && board[i + 1] == aiSymbol && board[i + 2] == aiSymbol)
            {
                score += 100;
            }
            else if (board[i] == playerSymbol && board[i + 1] == playerSymbol && board[i + 2] == playerSymbol)
            {
                score -= 100;
            }
        }

        // Check columns
        for (int i = 0; i < 3; i++)
        {
            if (board[i] == aiSymbol && board[i + 3] == aiSymbol && board[i + 6] == aiSymbol)
            {
                score += 100;
            }
            else if (board[i] == playerSymbol && board[i + 3] == playerSymbol && board[i + 6] == playerSymbol)
            {
                score -= 100;
            }
        }

        // Check diagonals
        if (board[0] == aiSymbol && board[4] == aiSymbol && board[8] == aiSymbol)
        {
            score += 100;
        }
        else if (board[0] == playerSymbol && board[4] == playerSymbol && board[8] == playerSymbol)
        {
            score -= 100;
        }

        if (board[2] == aiSymbol && board[4] == aiSymbol && board[6] == aiSymbol)
        {
            score += 100;
        }
        else if (board[2] == playerSymbol && board[4] == playerSymbol && board[6] == playerSymbol)
        {
            score -= 100;
        }

        // Check for open lines
        for (int i = 0; i < 9; i += 3)
        {
            if (board[i] == aiSymbol && board[i + 1] == aiSymbol && board[i + 2] == "")
            {
                score += 10;
            }
            else if (board[i] == playerSymbol && board[i + 1] == playerSymbol && board[i + 2] == "")
            {
                score -= 10;
            }

            if (board[i] == aiSymbol && board[i + 2] == aiSymbol && board[i + 1] == "")
            {
                score += 10;
            }
            else if (board[i] == playerSymbol && board[i + 2] == playerSymbol && board[i + 1] == "")
            {
                score -= 10;
            }

            if (board[i + 1] == aiSymbol && board[i + 2] == aiSymbol && board[i] == "")
            {
                score += 10;
            }
            else if (board[i + 1] == playerSymbol && board[i + 2] == playerSymbol && board[i] == "")
            {
                score -= 10;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            if (board[i] == aiSymbol && board[i + 3] == aiSymbol && board[i + 6] == "")
            {
                score += 10;
            }
            else if (board[i] == playerSymbol && board[i + 3] == playerSymbol && board[i + 6] == "")
            {
                score -= 10;
            }
        }

        return score;
    }

    IEnumerator DoAITurnWithDelay()
    {
        gameOverText.text = aiName + "'s turn";

        yield return new WaitForSeconds(aiDelay);

        int difficultyLevel = 0;

        switch (ticTacToeDifficultyLevel)
        {
            case "Easy":
                difficultyLevel = 2;
                break;
            case "Medium":
                difficultyLevel = 4;
                break;
            case "Hard":
                difficultyLevel = 6;
                break;
        }

        DoAITurn(difficultyLevel);

        // Check if the game is over
        if (CheckForGameOver())
        {
            yield break;
        }

        // Switch turns
        playerTurn = true;
        playerMoved = false;
        gameOverText.text = playersName + "'s turn";
    }
}


