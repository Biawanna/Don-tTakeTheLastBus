using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TicTacToeGame : MonoBehaviour
{
    public string playerSymbol = "X";
    public string aiSymbol = "O";
    public TextMeshProUGUI playerTurnText;
    public TextMeshProUGUI gameOverText;
    private string[] board = new string[9];
    private bool playerTurn = true;

    void Start()
    {
        for (int i = 0; i < board.Length; i++)
        {
            board[i] = "";
        }
    }

    void UpdatePlayerTurnText()
    {
        playerTurnText.text = playerTurn ? "Player Turn" : "AI Turn";
    }

    bool CheckForGameOver()
    {
        if (CheckForWin(playerSymbol))
        {
            gameOverText.text = "Player Wins!";
            return true;
        }

        if (CheckForWin(aiSymbol))
        {
            gameOverText.text = "AI Wins!";
            return true;
        }

        if (CheckForTie())
        {
            gameOverText.text = "Tie!";
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
            UpdatePlayerTurnText();
            button.GetComponentInChildren<Text>().text = playerSymbol;

            if (CheckForGameOver())
            {
                return;
            }

            playerTurn = false;
            UpdatePlayerTurnText();
            DoAITurn();
        }
    }

    void DoAITurn()
    {
        int index = Random.Range(0, 9);

        if (board[index] == "")
        {
            board[index] = aiSymbol;
            buttonAtIndex(index).GetComponentInChildren<Text>().text = aiSymbol;

            if (!CheckForGameOver())
            {
                playerTurn = true;
                UpdatePlayerTurnText();
            }
        }
        else
        {
            DoAITurn();
        }
    }

    Button buttonAtIndex(int index)
    {
        return transform.Find(index.ToString()).GetComponent<Button>();
    }
}
