using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TicTacToeGame : MonoBehaviour
{
    public string playerSymbol = "X";
    public string aiSymbol = "O";
    //public TextMeshProUGUI playerTurnText;
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

    //void UpdatePlayerTurnText()
    //{
    //    playerTurnText.text = playerTurn ? "Player Turn" : "Emo Turn";
    //}

    bool CheckForGameOver()
    {
        if (CheckForWin(playerSymbol))
        {
            gameOverText.text = "Player Wins!";
            retryButtons.SetActive(true);

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

            //UpdatePlayerTurnText();
            button.GetComponentInChildren<TextMeshProUGUI>().text = playerSymbol;

            if (CheckForGameOver())
            {
                return;
            }

            playerTurn = false;
            //UpdatePlayerTurnText();
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
            //UpdatePlayerTurnText();
        }
    }



    Button buttonAtIndex(int index)
    {
        return transform.Find(index.ToString()).GetComponent<Button>();
    }

    public void NewTicTacToeGame()
    {
        boardGameObject.SetActive(true);
        RestartTicTacToe();
        //UpdatePlayerTurnText();
    }

    public void EndTicTacToe()
    {
        boardGameObject.SetActive(false);
        retryButtons.SetActive(false);
        dialogueTrigger.ToggleYesNoButtons(false);

        dialogueTrigger.CloseDialogue();
    }

    private void ClearTicTacToeText()
    {
        gameOverText.text = "";
        //playerTurnText.text = "";
    }

    public void RestartTicTacToe()
    {
        foreach (Button i in boardButtons)
        {
            i.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }

        dialogueTrigger.ToggleYesNoButtons(false);
        gameOverText.text = "";
    }
}
