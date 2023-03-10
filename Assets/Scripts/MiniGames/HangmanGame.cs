using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HangmanGame : MonoBehaviour
{
    [Header("Hangman Settings")]
    [SerializeField] private int numberOfGuesses;
    [SerializeField] private string[] wordList = { "apple", "banana", "cherry", "orange", "pear" };

    // UI elements
    [SerializeField] private TextMeshProUGUI wordText;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private GameObject retryButtons;
    [SerializeField] private GameObject lettersObject;
    [SerializeField] private Button[] letterButtons;

    // Game variables
    private string word;
    private char[] letters;
    private int remainingGuesses = 6;
    private List<char> guessedLetters = new List<char>();
    private DialogueTrigger dialogueTrigger;
    private DialogueManager dialogueManager;

    private void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
        dialogueManager = DialogueManager.instance;

        ToggleHangmanRetryButtons(false);
        ToggleHangmanLetter(false);
        ClearHangmanText();
    }

    public void NewGame()
    {
        dialogueTrigger.ToggleYesNoButtons(false);
        ToggleHangmanRetryButtons(false);
        ToggleHangmanLetter(true);

        remainingGuesses = numberOfGuesses;

        // Choose a random word from the word list
        word = wordList[Random.Range(0, wordList.Length)];

        // Create an array of the letters in the word
        letters = word.ToLower().ToCharArray();

        // Initialize the UI
        wordText.text = new string('_', letters.Length);
        messageText.text = "Guess a letter! Remaining guesses: " + remainingGuesses;

        foreach (Button button in letterButtons)
        {
            button.interactable = true;
        }

        guessedLetters.Clear();
    }

    public void GuessLetter(string letter)
    {
        // Get the first character of the string as the guessed letter
        char guessedLetter = char.ToLower(letter[0]);

        // Disable the button to prevent the player from guessing the same letter twice
        Button button = letterButtons[guessedLetter - 'a'];
        button.interactable = false;

        // Check if the letter is in the word
        bool foundLetter = false;


        for (int i = 0; i < letters.Length; i++)
        {
            if (letters[i] == guessedLetter)
            {
                // Update the word text to show the correctly guessed letter
                char[] wordChars = wordText.text.ToCharArray();
                wordChars[i] = guessedLetter;
                wordText.text = new string(wordChars);

                foundLetter = true;
            }
        }

        // If the letter is not in the word, decrement the remaining guesses
        if (!foundLetter)
        {
            remainingGuesses--;
        }

        // Add the letter to the list of guessed letters
        guessedLetters.Add(guessedLetter);

        // Check if the player has won or lost
        if (wordText.text.IndexOf('_') == -1)
        {
            // Player has won
            messageText.text = "You win!";
            HangManWin();
            foreach (Button b in letterButtons)
            {
                b.interactable = false;
            }
            ToggleHangmanLetter(false);
            ToggleHangmanRetryButtons(true);
        }
        else if (remainingGuesses == 0)
        {
            // Player has lost
            wordText.text = word;
            messageText.text = "Game over! You lose!";
            foreach (Button b in letterButtons)
            {
                b.interactable = false;
            }
            ToggleHangmanLetter(false);
            ToggleHangmanRetryButtons(true);
        }
        else
        {
            // Update the message text to show the remaining guesses and guessed letters
            messageText.text = "Guess a letter! Remaining guesses: " + remainingGuesses + " Guessed letters: " + string.Join(", ", guessedLetters);
        }
    }


    public void EndHangManGame()
    {
        ToggleHangmanRetryButtons(false);
        ToggleHangmanLetter(false);
        ClearHangmanText();
        dialogueTrigger.CloseDialogue();
    }

    private void HangManWin()
    {
        if (!dialogueManager.InventoryScriptableObject.holyWater)
        {
            dialogueTrigger.IncreaseCurrentDialogueIndex();
            ClearHangmanText();
            dialogueTrigger.OpenDialogue();
        }
    }
    private void ToggleHangmanRetryButtons(bool showOptions)
    {
        dialogueTrigger.ToggleObjectVisibilty(showOptions, retryButtons);
    }

    private void ToggleHangmanLetter(bool showLetters)
    {
        dialogueTrigger.ToggleObjectVisibilty(showLetters, lettersObject);
    }

    private void ClearHangmanText()
    {
        messageText.text = "";
        wordText.text = "";
    }
}
