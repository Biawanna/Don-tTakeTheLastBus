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

    private TextMeshProUGUI dialogueText;
    private DialogueTrigger dialogueTrigger;
    private DialogueType dialogueObject;

    Queue<string> sentences;

    public TextMeshProUGUI DialogueText
    {
        get { return dialogueText; }
        set { dialogueText = value; }
    }

    public DialogueTrigger DialogueTrigger
    {
        get { return dialogueTrigger; }
        set { dialogueTrigger = value; }
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
                dialogueTrigger.PlayRandomDialogue();

                break;
            case DialogueType.scout:
                //dialogueObject = DialogueType.scout;
             
                break;
            case DialogueType.patient:
                //dialogueObject = DialogueType.scout;

                break;
            case DialogueType.biker:
                //dialogueObject = DialogueType.scout;

                break;
            case DialogueType.punk:
                //dialogueObject = DialogueType.scout;

                break;
            case DialogueType.beagle:
                //dialogueObject = DialogueType.scout;

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
        switch (dialogueObject)
        {
            case DialogueType.busDriver:
                dialogueTrigger.CloseDialogue();

                break;
            case DialogueType.scout:
                dialogueTrigger.CloseDialogue();

                break;
            default:
                break;
        }
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
        StartDialogue(dialogueTrigger.CurrentDialogue);
        //dialogueTrigger.CurrentDialogue.dialoguePlayed = true;
    }
}
