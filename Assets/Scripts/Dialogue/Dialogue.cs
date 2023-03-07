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

    [Header("Scriptable Object References")]
    [SerializeField] private DialogueScriptableObject dialogueObject;
    //[SerializeField] private DialogueScriptableObject practiceOrbDialogue;

    [Header("Text References")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    //[SerializeField] private TextMeshProUGUI playArenaDialogueText;

    [Header("Sequence References")]
    //[SerializeField] private DialogueMenuSequence playArenaDialogueSequence;
    //[SerializeField] private DialogueMenuSequence practiceDialogueSequence;


    //TextMeshProUGUI dialogueText;
    Queue<string> sentences;
    //DialogueType dialogueObject;
    UICanvasController uICanvasController;

    //public DialogueMenuSequence PracticeDialogueSequence
    //{
    //    get => practiceDialogueSequence;
    //    private set => practiceDialogueSequence = value;
    //}
    //public DialogueMenuSequence PlayArenaDialogueSequence
    //{
    //    get => playArenaDialogueSequence;
    //    private set => playArenaDialogueSequence = value;
    //}
    //public DialogueScriptableObject PlayArenaDialogue
    //{
    //    get => dialogueObject;
    //    set => dialogueObject = value;
    //}
    //public DialogueScriptableObject PracticeOrbDialogue
    //{
    //    get => practiceOrbDialogue;
    //    set => practiceOrbDialogue = value;
    //}

    private void Awake()
    {
        instance = this;
        uICanvasController = UICanvasController.Instance;
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
                //uICanvasController.ShowPlayArenaMenuButtons(false);
                //dialogueObject = DialogueType.busDriver;
                //playArenaDialogueSequence.OpenCloseDialogueSequence(true);
                break;
            case DialogueType.scout:
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
        //switch (dialogueObject)
        //{
        //    case DialogueType.busDriver;
        //        //uICanvasController.ShowPlayArenaMenuButtons(true);
        //        //playArenaDialogueSequence.OpenCloseDialogueSequence(false);
        //        break;
        //    case DialogueType.practiceOrbDialogue:
        //        uICanvasController.ShowPracticeMenuButtons(true);
        //        practiceDialogueSequence.OpenCloseDialogueSequence(false);
        //        break;
        //    default:
        //        break;
        //    }
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

    /// <summary>
    /// Sets dialogue text to play arena and enables the popup
    /// </summary>
    public void PlayDialogueText()
    {
        //playArenaDialogueSequence.OpenCloseDialogueSequence(true);
        //dialogueText = playArenaDialogueText;
    }
}
