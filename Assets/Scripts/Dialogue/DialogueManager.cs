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
    private int incrementDialogueIndex = 1;

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

                if (GetDialogueBySentenceType(DialogueSentenceType.RPSGame))
                {
                    currentDialogueTrigger.ToggleDialogueOptions(true);
                }


                break;
            case DialoguePerson.patient:
                //dialogueObject = DialoguePerson.scout;

                break;
            case DialoguePerson.biker:
                inventoryScriptableObject.dogTreat = true;

                currentDialogueTrigger.SetCurrentDialogueIndex(incrementDialogueIndex);

                GetDialogueTriggerByType(DialoguePerson.beagle).SetCurrentDialogueIndex(incrementDialogueIndex);


                //dialogueObject = DialoguePerson.scout;

                break;
            case DialoguePerson.punk:
                //dialogueObject = DialoguePerson.scout;

                break;
            case DialoguePerson.beagle:

                if (inventoryScriptableObject.dogTreat == true) 
                {
                    IncrementDialogueIndex();
                }

                if (GetDialogueBySentenceType(DialogueSentenceType.thankYou))
                {
                    Debug.Log("Do Something");
                    currentDialogueTrigger.ToggleDialogueOptions(true);
                }


                break;
            case DialoguePerson.coolMan:
                //dialogueObject = DialoguePerson.scout;

                break;
            case DialoguePerson.eastern:
                //dialogueObject = DialoguePerson.scout;

                break;
            case DialoguePerson.emo:
                //dialogueObject = DialoguePerson.scout;

                break;
            case DialoguePerson.homeless:
                //dialogueObject = DialoguePerson.scout;

                break;
            case DialoguePerson.jock:
                //dialogueObject = DialoguePerson.scout;

                break;
            case DialoguePerson.islander:
                //dialogueObject = DialoguePerson.scout;

                break;
            case DialoguePerson.wanderer:
                //dialogueObject = DialoguePerson.scout;

                break;
            case DialoguePerson.waitress:
                //dialogueObject = DialoguePerson.scout;

                break;
            case DialoguePerson.nerd:
                //dialogueObject = DialoguePerson.scout;

                break;
            case DialoguePerson.teenBoy:
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
    private void IncrementDialogueIndex()
    {
        currentDialogueTrigger.SetCurrentDialogueIndex(incrementDialogueIndex);
    }
}
