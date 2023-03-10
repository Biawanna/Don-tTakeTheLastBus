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
    private DialogueType dialogueObject;
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
            case DialogueType.busDriver:
                currentDialogueTrigger.PlayRandomDialogue();

                break;
            case DialogueType.scout:

                if (GetDialogueByName("RPSGame"))
                {
                    Debug.Log("Do Something");
                    currentDialogueTrigger.ToggleDialogueOptions(true);
                }


                break;
            case DialogueType.patient:
                //dialogueObject = DialogueType.scout;

                break;
            case DialogueType.biker:
                inventoryScriptableObject.dogTreat = true;

                currentDialogueTrigger.SetCurrentDialogueIndex(incrementDialogueIndex);

                GetDialogueTriggerByType(DialogueType.beagle).SetCurrentDialogueIndex(incrementDialogueIndex);


                //dialogueObject = DialogueType.scout;

                break;
            case DialogueType.punk:
                //dialogueObject = DialogueType.scout;

                break;
            case DialogueType.beagle:

                if (inventoryScriptableObject.dogTreat == true) 
                { currentDialogueTrigger.SetCurrentDialogueIndex(incrementDialogueIndex); }

                if (GetDialogueByName("ThankYou"))
                {
                    Debug.Log("Do Something");
                    currentDialogueTrigger.ToggleDialogueOptions(true);
                }


                break;
            case DialogueType.coolMan:
                //dialogueObject = DialogueType.scout;

                break;
            case DialogueType.eastern:
                //dialogueObject = DialogueType.scout;

                break;
            case DialogueType.emo:
                //dialogueObject = DialogueType.scout;

                break;
            case DialogueType.homeless:
                //dialogueObject = DialogueType.scout;

                break;
            case DialogueType.jock:
                //dialogueObject = DialogueType.scout;

                break;
            case DialogueType.islander:
                //dialogueObject = DialogueType.scout;

                break;
            case DialogueType.wanderer:
                //dialogueObject = DialogueType.scout;

                break;
            case DialogueType.waitress:
                //dialogueObject = DialogueType.scout;

                break;
            case DialogueType.nerd:
                //dialogueObject = DialogueType.scout;

                break;
            case DialogueType.teenBoy:
                //dialogueObject = DialogueType.scout;

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

    public DialogueTrigger GetDialogueTriggerByType(DialogueType type)
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


    public bool GetDialogueByName(string dialogue)
    {
        if (currentDialogueTrigger.CurrentDialogue.dialogueName.ToString() == dialogue)
        {
            bool correct = true;
            return correct;
        }

        return false;
    }
}
