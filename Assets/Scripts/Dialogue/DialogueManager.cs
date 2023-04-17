using System.Collections;
using System.Collections.Generic;
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

    [Header("Inventory")]
    [SerializeField] private PlayerInventory playerInventory;

    [Header("Dialogue Scriptable Objects")]
    [SerializeField] private DialogueTrigger[] dialogueTriggers;
    [SerializeField] private bool dialogueInPlay;

    private TextMeshProUGUI dialogueText = null;
    private DialogueTrigger currentDialogueTrigger;

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
        get { return playerInventory.Inventory; }
        set { playerInventory.Inventory = value; }
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

                if (playerInventory.CheckIfPlayerWins())
                {
                    IncrementDialogueIndex();
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.gameComplete))
                {
                    currentDialogueTrigger.ToggleDialogueOptions(true);
                }

                break;

            case DialoguePerson.scout:

                ScoutDialogue();
                break;

            case DialoguePerson.inpatient:

                InPatientDialogue();
                break;

            case DialoguePerson.teenBoy:

                TeenBoyDialogue();
                break;

            case DialoguePerson.biker:

                BikerDialogue();
                break;

            case DialoguePerson.nun:

                NunDialogue();
                break;

            case DialoguePerson.nerd:

                NerdDialogue();
                break;

            case DialoguePerson.emo:

                EmoDialogue();
                break;

            case DialoguePerson.islander:

                IslanderDialogue();
                break;

            case DialoguePerson.jock:

                JockDialogue();
                break;

            case DialoguePerson.homeless:

                HomelessDialogue();
                break;

            case DialoguePerson.waitress:

                WaitressDialogue();
                break;

            case DialoguePerson.beagle:

                BeagleDialogue();
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
    /// Sets the dialogue in play bool.
    /// </summary>
    public void SetDialogueInPlay(bool setBool)
    {
        dialogueInPlay = setBool;
    }

    // Passenger dialogue methods.

    private void ScoutDialogue()
    {
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
            playerInventory.SetItemTrue(item => InventoryScriptableObject.headPhones = item, true);

            ToolBox.UpdateIconSprite(currentDialogueTrigger.Icon, iconSpawnPoint, iconFadeCanvas);
            IncrementDialogueIndex();
        }
    }

    private void InPatientDialogue()
    {
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
            playerInventory.SetItemTrue(item => InventoryScriptableObject.soul = item, true);

            ToolBox.UpdateIconSprite(currentDialogueTrigger.Icon, iconSpawnPoint, iconFadeCanvas);
            IncrementDialogueIndex();
        }
    }

    private void NunDialogue()
    {
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
            playerInventory.SetItemTrue(item => InventoryScriptableObject.holyWater = item, true);

            ToolBox.UpdateIconSprite(currentDialogueTrigger.Icon, iconSpawnPoint, iconFadeCanvas);
            IncrementDialogueIndex();
        }
    }

    private void BikerDialogue()
    {
        if (GetDialogueBySentenceType(DialogueSentenceType.intro))
        {
            IncrementDialogueIndex();
        }

        else if (GetDialogueBySentenceType(DialogueSentenceType.RPSWin) && InventoryScriptableObject.headPhones)
        {
            playerInventory.SetItemTrue(item => InventoryScriptableObject.dogBone = item, true);

            IncrementDialogueIndex();
        }
        else if (GetDialogueBySentenceType(DialogueSentenceType.thankYou))
        {
            ToolBox.UpdateIconSprite(currentDialogueTrigger.Icon, iconSpawnPoint, iconFadeCanvas);
            IncrementDialogueIndex();
        }
    }

    private void TeenBoyDialogue()
    {
        if (GetDialogueBySentenceType(DialogueSentenceType.intro) && InventoryScriptableObject.soul)
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
    }

    private void BeagleDialogue()
    {
        if (GetDialogueBySentenceType(DialogueSentenceType.intro) && InventoryScriptableObject.holyWater)
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
    }

    private void WaitressDialogue()
    {
        if (GetDialogueBySentenceType(DialogueSentenceType.intro) && InventoryScriptableObject.coffee)
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
    }

    private void HomelessDialogue()
    {
        if (GetDialogueBySentenceType(DialogueSentenceType.intro) && InventoryScriptableObject.newspaper)
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
    }

    private void JockDialogue()
    {
        if (GetDialogueBySentenceType(DialogueSentenceType.intro) && InventoryScriptableObject.dogBone)
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
    }

    private void IslanderDialogue()
    {
        if (GetDialogueBySentenceType(DialogueSentenceType.intro) && InventoryScriptableObject.coconut)
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
    }

    private void EmoDialogue()
    {
        if (GetDialogueBySentenceType(DialogueSentenceType.intro) && InventoryScriptableObject.popCan)
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
            playerInventory.SetItemTrue(item => InventoryScriptableObject.coconut = item, true);

            ToolBox.UpdateIconSprite(currentDialogueTrigger.Icon, iconSpawnPoint, iconFadeCanvas);
            IncrementDialogueIndex();
        }

        else if (GetDialogueBySentenceType(DialogueSentenceType.none))
        {
            currentDialogueTrigger.ToggleDialogueOptions(true);
        }
    }

    private void NerdDialogue()
    {
        if (GetDialogueBySentenceType(DialogueSentenceType.intro))
        {
            currentDialogueTrigger.ToggleDialogueOptions(true);
        }

        else if (GetDialogueBySentenceType(DialogueSentenceType.hangManWin))
        {
            playerInventory.SetItemTrue(item => InventoryScriptableObject.catPicture = item, true);

            ToolBox.UpdateIconSprite(currentDialogueTrigger.Icon, iconSpawnPoint, iconFadeCanvas);
            IncrementDialogueIndex();
        }

        else if (GetDialogueBySentenceType(DialogueSentenceType.none))
        {
            IncrementDialogueIndex();
        }
    }
}
