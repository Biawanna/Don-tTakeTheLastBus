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

    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    [Header("Scriptable Object References")]
    //[SerializeField] private DialogueScriptableObject dialogueObject;

    [Header("Sequence References")]
    //[SerializeField] private DialogueMenuSequence playArenaDialogueSequence;
    //[SerializeField] private DialogueMenuSequence practiceDialogueSequence;


    Queue<string> sentences;

    UICanvasController uICanvasController;
    private TextMeshProUGUI dialogueText;
    private DialogueType dialogueObject;

    private void Awake()
    {
        instance = this;
        uICanvasController = UICanvasController.Instance;
    }

    private void Start()
    {
        sentences = new Queue<string>();
        dialogueText = textMeshProUGUI;
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
                //uICanvasController.ShowPlayArenaMenuButtons(false);
                //playArenaDialogueSequence.OpenCloseDialogueSequence(true);
                break;
            case DialogueType.scout:
                dialogueObject = DialogueType.scout;
                //dialogueObject = DialogueType.practiceOrbDialogue;
                //practiceDialogueSequence.OpenCloseDialogueSequence(true);
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
                //uICanvasController.ShowPlayArenaMenuButtons(true);
                //playArenaDialogueSequence.OpenCloseDialogueSequence(false);
                break;
            case DialogueType.scout:
                //uICanvasController.ShowPracticeMenuButtons(true);
                //practiceDialogueSequence.OpenCloseDialogueSequence(false);
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
