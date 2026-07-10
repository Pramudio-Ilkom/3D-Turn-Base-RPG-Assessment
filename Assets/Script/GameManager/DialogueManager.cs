using System.Data;
using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue")]
    public Dialogue[] dialogues;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;
    [Header("Dialogue Speed")]
    public float DialogueSpeed = 0.05f;
    private int currentDialogueIndex = 0;

    private void Awake()
    {
            dialogueText = dialogueBox.GetComponent<TextMeshProUGUI>;
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
        string[] dialogue = dialogues[IndexDialogue].dialogue;
        dialogueBox.SetActive(true);
        currentDialogueIndex = 0;
        DisplayNextDialogue(dialogue);
    }

    public void DisplayNextDialogue(string[] dialogue)
    {
        if (currentDialogueIndex < dialogue.Length)
        {
            StartCoroutine(TypeDialogue(dialogue[currentDialogueIndex]));
            currentDialogueIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        dialogueBox.SetActive(false);
    }

    private IEnumerator TypeDialogue(string dialogue)
    {
        dialogueText.text = dialogue;
        dialogueText.maxVisibleCharacters = 0;
        while (dialogueText.maxVisibleCharacters < dialogue.Length)
        {
            dialogueText.maxVisibleCharacters++;
            yield return new WaitForSeconds(DialogueSpeed);
        }
    }

    public void SkipDialogue(string dialogue)
    {
        StopAllCoroutines();
        dialogueText.maxVisibleCharacters = dialogue.Length;
    }
}
