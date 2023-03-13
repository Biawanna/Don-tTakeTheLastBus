using System;
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
    [Header("Icon References")]
    [SerializeField] private Image iconSpawnPoint;
    [SerializeField] private FadeCanvas iconFadeCanvas;

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

        iconFadeCanvas.FadeOut(0);
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
                    UpdateIconSprite(currentDialogueTrigger.Icon);
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
                    UpdateIconSprite(currentDialogueTrigger.Icon);
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
                    UpdateIconSprite(currentDialogueTrigger.Icon);
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
                    UpdateIconSprite(currentDialogueTrigger.Icon);
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
                    UpdateIconSprite(currentDialogueTrigger.Icon);
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
                    UpdateIconSprite(currentDialogueTrigger.Icon);
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
                    UpdateIconSprite(currentDialogueTrigger.Icon);
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
                    UpdateIconSprite(currentDialogueTrigger.Icon);
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
                    UpdateIconSprite(currentDialogueTrigger.Icon);
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
                    IncrementDialogueIndex();
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

    private void UpdateIconSprite(Sprite sprite)
    {
        if (sprite == null)
        {
            return;
        }

        iconSpawnPoint.sprite = sprite;

        iconFadeCanvas.StartFadeInFadeOutRoutine();
    }

    /// <summary>
    /// Moves the dialoguetrigggers current index to the next one.
    /// </summary>
    public void IncrementDialogueIndex()
    {
        currentDialogueTrigger.IncreaseCurrentDialogueIndex();
    }
    private bool CheckIfPlayerWins()
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
        inventoryScriptableObject.newspaper
        };

        return requiredItems.All(item => item);
    }
}
