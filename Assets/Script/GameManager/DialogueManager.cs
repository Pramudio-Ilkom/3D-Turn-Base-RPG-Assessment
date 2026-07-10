using System.Data;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue")]
    public Dialogue[] dialogues;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;
    [Header("Dialogue Speed")]
    public float DialogueSpeed = 0.05f;
    private int currentDialogueSetIndex = 0;
    private int currentDialogueIndex = 0;
    private bool dialogueStillRun = false;
    //private alias

    private void Awake()
    {
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue(int IndexDialogue)
    {
        dialogueBox.SetActive(true);
        currentDialogueSetIndex = IndexDialogue;
        currentDialogueIndex = 0;
        StartCoroutine(TypeDialogue(currentDialogueSetIndex,
                                    currentDialogueIndex));
    }

    private void showDialogue(int currentDialogueSetInput,
                              int currentDialogueInput)
    {
        characterName.text = dialogues[currentDialogueSetInput].character[currentDialogueInput];
        dialogueText.text = dialogues[currentDialogueSetInput].dialogue[currentDialogueInput];
    }

    public void DisplayNextDialogue()
    {
        if(dialogueStillRun)
        {
            SkipDialogue();
        }
        if (currentDialogueIndex < dialogues[currentDialogueSetIndex].dialogue.Length - 1)
        {
            currentDialogueIndex++;
            StartCoroutine(TypeDialogue(currentDialogueSetIndex,
                                        currentDialogueIndex));
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        // dialogueBox.SetActive(false);
        characterName.text = "wes";
        dialogueText.text = "dah kelar";

    }

    private IEnumerator TypeDialogue(int currentDialogueSetInput,
                                     int currentDialogueInput)
    {
        dialogueStillRun = true;
        showDialogue(currentDialogueSetInput,
                     currentDialogueInput);
        dialogueText.maxVisibleCharacters = 0;
        while (dialogueText.maxVisibleCharacters < dialogues[currentDialogueSetIndex].dialogue[currentDialogueIndex].Length)
        {
            dialogueText.maxVisibleCharacters++;
            yield return new WaitForSeconds(DialogueSpeed);
        }
        dialogueStillRun = false;
    }

    public void SkipDialogue()
    {
        StopAllCoroutines();
        dialogueText.maxVisibleCharacters = dialogues[currentDialogueSetIndex].dialogue[currentDialogueIndex].Length;
    }
}
