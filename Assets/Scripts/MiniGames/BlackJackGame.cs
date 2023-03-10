using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BlackJackGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private TextMeshProUGUI dealerScoreText;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private TextMeshProUGUI streakText;
    [SerializeField] private Button hitButton;
    [SerializeField] private Button standButton;
    [SerializeField] private GameObject retryButtons;
    [SerializeField] private GameObject gameButtons;

    private Deck deck;
    private List<Card> playerHand;
    private List<Card> dealerHand;
    private int playerScore;
    private int dealerScore;
    private bool gameInProgress;
    private string dealerName;
    private DialogueTrigger dialogueTrigger;
    private DialogueManager dialogueManager;
    private int winningStreak = 0;

    // Start is called before the first frame update
    void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
        dialogueManager = DialogueManager.instance;

        deck = new Deck();
        playerHand = new List<Card>();
        dealerHand = new List<Card>();
        playerScore = 0;
        dealerScore = 0;
        gameInProgress = false;
       

        dealerName = dialogueTrigger.CurrentDialogue.dialogueType.ToString();
        //dealerName = "inpatient";

        retryButtons.SetActive(false);
        gameButtons.SetActive(false);
        ClearBlackJackText();
    }

    private void Update()
    {
        if (gameInProgress)
        {
            UpdateBlackJackScoreUI();

            if (playerScore > 21)
            {
                messageText.text = "Player busts! Dealer wins!";
                PlayerLosesBlackJack();
                gameInProgress = false;
            }
            else if (dealerScore > 21)
            {
                messageText.text = dealerName + " busts! Player wins!";
                PlayerWinsBlackJack();
                gameInProgress = false;
            }
            else if (dealerScore >= 17)
            {
                if (dealerScore > playerScore)
                {
                    messageText.text = dealerName + " wins!";

                    PlayerLosesBlackJack();
                }
                else if (dealerScore == playerScore)
                {
                    messageText.text = "Push!";
                    Reset();
                }
                else
                {
                    messageText.text = "Player wins!";
                    PlayerWinsBlackJack();
                }

                gameInProgress = false;
            }
        }
    }

    public void Deal()
    {
        dialogueTrigger.ToggleYesNoButtons(false);
        retryButtons.SetActive(false);
        gameButtons.SetActive(true);

        deck.Shuffle();
        playerHand.Add(deck.Deal());
        dealerHand.Add(deck.Deal());
        playerHand.Add(deck.Deal());
        dealerHand.Add(deck.Deal());

        playerScore = CalculateScore(playerHand);
        dealerScore = CalculateScore(dealerHand);

        gameInProgress = true;
        messageText.text = "";
        hitButton.interactable = true;
        standButton.interactable = true;
    }

    public void Hit()
    {
        playerHand.Add(deck.Deal());
        playerScore = CalculateScore(playerHand);
    }

    public void Stand()
    {
        while (dealerScore < 17)
        {
            dealerHand.Add(deck.Deal());
            dealerScore = CalculateScore(dealerHand);
        }
    }

    private int CalculateScore(List<Card> hand)
    {
        int score = 0;
        bool hasAce = false;

        foreach (Card card in hand)
        {
            if (card.Rank == Rank.Ace)
            {
                hasAce = true;
            }

            score += card.PointValue;
        }

        if (hasAce && score <= 11)
        {
            score += 10;
        }

        return score;
    }

    public void QuitBlackJack()
    {
        gameInProgress = false;
        Reset();
        retryButtons.SetActive(false);
        gameButtons.SetActive(false);
        dialogueTrigger.CloseDialogue();
    }
    public void Restart()
    {
        Reset();

        retryButtons.SetActive(false);
        gameButtons.SetActive(true);

        Deal();
    }
    private void UpdateBlackJackScoreUI()
    {
        playerScoreText.text = "Player Score: " + playerScore.ToString();
        dealerScoreText.text = dealerName + " Score: " + dealerScore.ToString();
        streakText.text = "Streak: " + winningStreak.ToString();
    }

    private void ClearBlackJackText()
    {
        playerScoreText.text = "";
        dealerScoreText.text = "";
        messageText.text = "";
        streakText.text = "";
    }

    private void PlayerWinsBlackJack()
    {
        retryButtons.SetActive(true);
        gameButtons.SetActive(false);

        winningStreak++;

        if (!dialogueManager.InventoryScriptableObject.soul)
        {
            dialogueTrigger.IncreaseCurrentDialogueIndex();
            ClearBlackJackText();
            dialogueTrigger.OpenDialogue();
        }
    }

    private void PlayerLosesBlackJack()
    {
        retryButtons.SetActive(true);
        gameButtons.SetActive(false);

        winningStreak = 0;
    }

    private void Reset()
    {
        // Reset the game
        playerHand.Clear();
        dealerHand.Clear();
        playerScore = 0;
        dealerScore = 0;
        gameInProgress = false;

        // Clear the message text
        ClearBlackJackText();
    }
}
