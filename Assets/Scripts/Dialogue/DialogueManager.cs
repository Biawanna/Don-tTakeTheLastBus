using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This script holds DialogueManager methods.
/// </summary>
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [Header("Type Settings")]
    [SerializeField] private float dialogueTypingSpeed;

    [Header("Icon References")]
    [SerializeField] private Image iconSpawnPoint;
    [SerializeField] private FadeCanvas iconFadeCanvas;

    [Header("Dialogue Scriptable Objects")]
    [SerializeField] private InventoryScriptableObject inventoryScriptableObject;
    [SerializeField] private DialogueTrigger[] dialogueTriggers;
    [SerializeField] private bool dialogueInPlay;

    [SerializeField] private TextMeshProUGUI dialogueText = null;
    private DialogueTrigger currentDialogueTrigger;
    private DialoguePerson dialogueObject;

    Queue<string> sentences;

    public TextMeshProUGUI DialogueText
    {
        get { return dialogueText; }
        set { dialogueText = value; }
    }

    public DialogueTrigger CurrentDialogueTrigger
    {
        get { return currentDialogueTrigger; }
        set { currentDialogueTrigger = value; }
    }

    public InventoryScriptableObject InventoryScriptableObject
    {
        get { return inventoryScriptableObject; }
        set { inventoryScriptableObject = value; }
    }

    public bool DialogueInPlay
    {
        get { return dialogueInPlay; }
        set { dialogueInPlay = value; }
    }
       
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        sentences = new Queue<string>();

        iconFadeCanvas.FadeOut(0);
    }

    /// <summary>
    /// Starts the dialogue depending on the dialogue person.
    /// </summary>
    public void StartDialogue(DialogueScriptableObject dialogue)
    {
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        switch (dialogue.dialogueType)
        {
            case DialoguePerson.busDriver:

                if (CheckIfPlayerWins())
                {
                    IncrementDialogueIndex();
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.gameComplete))
                {
                    currentDialogueTrigger.ToggleDialogueOptions(true);
                }

                break;

            case DialoguePerson.scout:

                if (GetDialogueBySentenceType(DialogueSentenceType.intro))
                {
                    IncrementDialogueIndex();
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.RPSGame))
                {
                    currentDialogueTrigger.ToggleDialogueOptions(true);
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.RPSWin))
                {
                    inventoryScriptableObject.headPhones = true;
                    ToolBox.UpdateIconSprite(currentDialogueTrigger.Icon, iconSpawnPoint, iconFadeCanvas);
                    IncrementDialogueIndex();
                }

                break;

            case DialoguePerson.inpatient:
                if (GetDialogueBySentenceType(DialogueSentenceType.intro))
                {
                    IncrementDialogueIndex();
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.blackJackGame))
                {
                    currentDialogueTrigger.ToggleDialogueOptions(true);
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.blackJackWin))
                {
                    inventoryScriptableObject.soul = true;
                    ToolBox.UpdateIconSprite(currentDialogueTrigger.Icon, iconSpawnPoint, iconFadeCanvas);
                    IncrementDialogueIndex();
                }

                break;

            case DialoguePerson.teenBoy:

                if (GetDialogueBySentenceType(DialogueSentenceType.intro) && inventoryScriptableObject.soul == true)
                {
                    IncrementDialogueIndex();
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.blackJackWin))
                {
                    currentDialogueTrigger.ToggleDialogueOptions(true);
                    IncrementDialogueIndex();
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.thankYou))
                {
                    ToolBox.UpdateIconSprite(currentDialogueTrigger.Icon, iconSpawnPoint, iconFadeCanvas);
                    IncrementDialogueIndex();
                }

                break;

            case DialoguePerson.biker:

                
                if (GetDialogueBySentenceType(DialogueSentenceType.intro))
                {
                    IncrementDialogueIndex();
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.RPSWin ) && inventoryScriptableObject.headPhones == true)
                {
                    inventoryScriptableObject.dogBone = true;
                    IncrementDialogueIndex();
                }
                else if (GetDialogueBySentenceType(DialogueSentenceType.thankYou))
                {
                    ToolBox.UpdateIconSprite(currentDialogueTrigger.Icon, iconSpawnPoint, iconFadeCanvas);
                    IncrementDialogueIndex();
                }

                break;

            case DialoguePerson.nun:

                if (GetDialogueBySentenceType(DialogueSentenceType.intro))
                {
                    IncrementDialogueIndex();
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.hangmanGame))
                {
                    currentDialogueTrigger.ToggleDialogueOptions(true);
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.hangManWin))
                {
                    inventoryScriptableObject.holyWater = true;
                    ToolBox.UpdateIconSprite(currentDialogueTrigger.Icon, iconSpawnPoint, iconFadeCanvas);
                    IncrementDialogueIndex();
                }

                break;

            case DialoguePerson.nerd:

                if (GetDialogueBySentenceType(DialogueSentenceType.intro))
                {
                    currentDialogueTrigger.ToggleDialogueOptions(true);
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.hangManWin))
                {
                    inventoryScriptableObject.catPicture = true;
                    ToolBox.UpdateIconSprite(currentDialogueTrigger.Icon, iconSpawnPoint, iconFadeCanvas);
                    IncrementDialogueIndex();
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.none))
                {
                    IncrementDialogueIndex();
                }

                break;

            case DialoguePerson.emo:

                if (GetDialogueBySentenceType(DialogueSentenceType.intro ) && inventoryScriptableObject.popCan == true)
                {
                    IncrementDialogueIndex();
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.popCan))
                {
                    currentDialogueTrigger.ToggleDialogueOptions(true);
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.ticTacToeGame))
                {
                    currentDialogueTrigger.ToggleDialogueOptions(true);
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.ticToeWin))
                {
                    inventoryScriptableObject.coconut = true;
                    ToolBox.UpdateIconSprite(currentDialogueTrigger.Icon, iconSpawnPoint, iconFadeCanvas);
                    IncrementDialogueIndex();
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.none))
                {
                    currentDialogueTrigger.ToggleDialogueOptions(true);
                }

                break;

            case DialoguePerson.islander:

                if (GetDialogueBySentenceType(DialogueSentenceType.intro) && inventoryScriptableObject.coconut == true)
                {
                    IncrementDialogueIndex();
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.coconut))
                {
                    currentDialogueTrigger.ToggleDialogueOptions(true);
                    IncrementDialogueIndex();
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.thankYou))
                {
                    ToolBox.UpdateIconSprite(currentDialogueTrigger.Icon, iconSpawnPoint, iconFadeCanvas);
                    IncrementDialogueIndex();
                }

                break;

            case DialoguePerson.jock:

                if (GetDialogueBySentenceType(DialogueSentenceType.intro) && inventoryScriptableObject.dogBone == true)
                {
                    IncrementDialogueIndex();
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.dogBone))
                {
                    currentDialogueTrigger.ToggleDialogueOptions(true);
                    IncrementDialogueIndex();
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.thankYou))
                {
                    currentDialogueTrigger.ToggleDialogueOptions(false);
                    ToolBox.UpdateIconSprite(currentDialogueTrigger.Icon, iconSpawnPoint, iconFadeCanvas);
                    IncrementDialogueIndex();
                }

                break;

            case DialoguePerson.homeless:

                if (GetDialogueBySentenceType(DialogueSentenceType.intro) && inventoryScriptableObject.newspaper == true)
                {
                    IncrementDialogueIndex();
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.newspaper))
                {
                    currentDialogueTrigger.ToggleDialogueOptions(true);
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.thankYou))
                {
                    IncrementDialogueIndex();
                }

                break;

            case DialoguePerson.waitress:

                if (GetDialogueBySentenceType(DialogueSentenceType.intro) && inventoryScriptableObject.coffee == true)
                {
                    IncrementDialogueIndex();
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.coffee))
                {
                    currentDialogueTrigger.ToggleDialogueOptions(true);
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.thankYou))
                {
                    currentDialogueTrigger.ToggleDialogueOptions(false);
                    IncrementDialogueIndex();
                }

                break;

            case DialoguePerson.beagle:

                if (GetDialogueBySentenceType(DialogueSentenceType.intro) && inventoryScriptableObject.holyWater == true)
                {
                    IncrementDialogueIndex();
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.dogWater))
                {
                    currentDialogueTrigger.ToggleDialogueOptions(true);
                    IncrementDialogueIndex();
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.thankYou))
                {
                    IncrementDialogueIndex();
                }

                break;

            case DialoguePerson.coolMan:

                currentDialogueTrigger.PlayRandomDialogue();

                break;
            case DialoguePerson.eastern:

                currentDialogueTrigger.PlayRandomDialogue();

                break;
           
            case DialoguePerson.wanderer:

                currentDialogueTrigger.PlayRandomDialogue();

                break;

            case DialoguePerson.punk:

                currentDialogueTrigger.PlayRandomDialogue();

                break;

            default:
                break;
        }

        DisplayNextSentence();
    }

    /// <summary>
    /// Displays the next sentence in the dialogue.
    /// </summary>
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    /// <summary>
    /// Ends the dialogue.
    /// </summary>
    public void EndDialogue()
    {
        currentDialogueTrigger.CloseDialogue();
    }

    /// <summary>
    /// Types the dialogue sentence.
    /// </summary>
    public IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            if(dialogueText != null)
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(dialogueTypingSpeed);
            }
        }
    }

    /// <summary>
    /// Opens the passengers dialogue.
    /// </summary>
    public void OpenPassengerDialogue()
    {
        StartDialogue(currentDialogueTrigger.CurrentDialogue);
    }

    /// <summary>
    /// Returns the dialogue person type.
    /// </summary>
    public DialogueTrigger GetDialogueTriggerByType(DialoguePerson type)
    {
        for (int i = 0; i < dialogueTriggers.Length; i++)
        {
            if (dialogueTriggers[i].CurrentDialogue.dialogueType == type)
            {
                return dialogueTriggers[i];
            }
        }

        // if no DialogueTrigger with the specified type is found, return null
        return null;
    }

    /// <summary>
    /// Returns the dialogue sentence type.
    /// </summary>
    public bool GetDialogueBySentenceType(DialogueSentenceType dialogueSentenceType)
    {
        if (currentDialogueTrigger.CurrentDialogue.dialogueSentenceType == dialogueSentenceType)
        {
            bool correct = true;
            return correct;
        }

        return false;
    }

    /// <summary>
    /// Moves the dialoguetrigggers current index to the next one.
    /// </summary>
    public void IncrementDialogueIndex()
    {
        currentDialogueTrigger.IncreaseCurrentDialogueIndex();
    }

    /// <summary>
    /// Checks if the player has won the game.
    /// </summary>
    public bool CheckIfPlayerWins()
    {
        var requiredItems = new[]
        {
        inventoryScriptableObject.catPicture,
        inventoryScriptableObject.headPhones,
        inventoryScriptableObject.coconut,
        inventoryScriptableObject.soul,
        inventoryScriptableObject.holyWater,
        inventoryScriptableObject.coffee,
        inventoryScriptableObject.dogBone,
        inventoryScriptableObject.popCan,
        inventoryScriptableObject.newspaper,
        inventoryScriptableObject.coffeeGiven,
        inventoryScriptableObject.waterGiven,
        inventoryScriptableObject.newspaperGiven
        };

        return requiredItems.All(item => item);
    }

    /// <summary>
    /// Resets the players inventory.
    /// </summary>
    public void ResetInventory()
    {
        inventoryScriptableObject.catPicture = false;
        inventoryScriptableObject.headPhones = false;
        inventoryScriptableObject.coconut = false;
        inventoryScriptableObject.soul = false;
        inventoryScriptableObject.holyWater = false;
        inventoryScriptableObject.coffee = false;
        inventoryScriptableObject.dogBone = false;
        inventoryScriptableObject.popCan = false;
        inventoryScriptableObject.newspaper = false;
        inventoryScriptableObject.coffeeGiven = false;
        inventoryScriptableObject.waterGiven = false;
        inventoryScriptableObject.newspaperGiven = false;
    }

    /// <summary>
    /// Sets the dialogue in play bool.
    /// </summary>
    public void SetDialogueInPlay(bool setBool)
    {
        dialogueInPlay = setBool;
    }
}
