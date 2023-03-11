using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TicTacToeGame : MonoBehaviour
{
    public string playerSymbol = "X";
    public string aiSymbol = "O";
    public TextMeshProUGUI gameOverText;
    [SerializeField] private GameObject boardGameObject;
    [SerializeField] private GameObject retryButtons;
    [SerializeField] private Button[] boardButtons;
    private string[] board = new string[9];
    private bool playerTurn = true;
    private DialogueTrigger dialogueTrigger;
    private DialogueManager dialogueManager;

    void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
        dialogueManager = DialogueManager.instance;

        for (int i = 0; i < board.Length; i++)
        {
            board[i] = "";
        }

        boardGameObject.SetActive(false);
        retryButtons.SetActive(false);
        ClearTicTacToeText();
    }


    bool CheckForGameOver()
    {
        if (CheckForWin(playerSymbol))
        {
            gameOverText.text = "Player Wins!";
            PlayerWinsTicTacToe();

            return true;
        }

        if (CheckForWin(aiSymbol))
        {
            gameOverText.text = "Emo Wins!";
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

        if (board[index] == "")
        {
            board[index] = playerSymbol;

            button.GetComponentInChildren<TextMeshProUGUI>().text = playerSymbol;

            if (CheckForGameOver())
            {
                return;
            }

            playerTurn = false;
            DoAITurn();
        }
    }

    void DoAITurn()
    {
        int index = -1;

        while (index == -1 || board[index] != "")
        {
            index = Random.Range(0, board.Length);
        }

        board[index] = aiSymbol;

        boardButtons[index].GetComponentInChildren<TextMeshProUGUI>().text = aiSymbol;

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
        gameOverText.text = "";
    }
    private void ClearTicTacToeText()
    {
        gameOverText.text = "";
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
}
