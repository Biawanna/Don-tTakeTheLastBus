using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
/// <summary>
/// This script holds Dialogue box functions.
/// </summary>
public class Dialogue : MonoBehaviour
{
    public static Dialogue instance;

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
                dialogueObject = DialogueType.busDriver;

                break;
            case DialogueType.scout:
                dialogueObject = DialogueType.scout;
             
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
}
