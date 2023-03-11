using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// This script holds DialogueManager methods.
/// </summary>
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [Header("Type Settings")]
    [SerializeField] private float dialogueTypingSpeed;

    [Header("Dialogue Scriptable Objects")]
    [SerializeField] private InventoryScriptableObject inventoryScriptableObject;
    [SerializeField] private DialogueTrigger[] dialogueTriggers;

    private TextMeshProUGUI dialogueText;
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
       
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        sentences = new Queue<string>();
    }

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
                currentDialogueTrigger.PlayRandomDialogue();

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
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.thankYou))
                {
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
                    inventoryScriptableObject.dogTreat = true;
                    IncrementDialogueIndex();
                }

                break;
            case DialoguePerson.punk:
                //dialogueObject = DialoguePerson.scout;

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
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.thankYou))
                {
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
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.thankYou))
                {
                    currentDialogueTrigger.ToggleDialogueOptions(false);
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
                    currentDialogueTrigger.ToggleDialogueOptions(false);
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
                }

                else if (GetDialogueBySentenceType(DialogueSentenceType.thankYou))
                {
                    IncrementDialogueIndex();
                }

                break;

            case DialoguePerson.coolMan:
                //dialogueObject = DialoguePerson.scout;

                break;
            case DialoguePerson.eastern:
                //dialogueObject = DialoguePerson.scout;

                break;
          
            
           
            case DialoguePerson.wanderer:
                //dialogueObject = DialoguePerson.scout;

                break;
         
            case DialoguePerson.nerd:
                //dialogueObject = DialoguePerson.scout;

                break;
           
            default:
                break;
        }

        DisplayNextSentence();
    }

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

    public void EndDialogue()
    {
        currentDialogueTrigger.CloseDialogue();
    }

    public IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueTypingSpeed);
        }
    }

    public void OpenPassengerDialogue()
    {
        StartDialogue(currentDialogueTrigger.CurrentDialogue);
        //currentDialogueTrigger.CurrentDialogue.dialoguePlayed = true;
    }

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

}
