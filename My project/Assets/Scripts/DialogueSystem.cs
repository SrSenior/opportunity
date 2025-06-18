using UnityEngine;
using System.Collections;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private GameObject dialogueIndicator;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;

    private bool didDialogueStart;
    private bool isTyping;
    private Coroutine dialogueCoroutine;

    private int lineIndex;
    private float typingTime = 0.05f;
    private float autoAdvanceDelay = 3.0f;

    public void TriggerDialogue()
    {
        if (!didDialogueStart)
        {
            StartDialogue();
        }
        else if (isTyping)
        {
            // Si se está escribiendo, mostrar toda la línea inmediatamente
            isTyping = false;
        }
    }

    private void StartDialogue()
    {
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        lineIndex = 0;
        Time.timeScale = 0f;

        dialogueCoroutine = StartCoroutine(ShowLineAndAutoAdvance());
    }

    private IEnumerator ShowLineAndAutoAdvance()
    {
        while (lineIndex < dialogueLines.Length)
        {
            yield return StartCoroutine(TypeLine(dialogueLines[lineIndex]));
            yield return new WaitForSecondsRealtime(autoAdvanceDelay);
            lineIndex++;
        }

        // Fin del diálogo
        didDialogueStart = false;
        dialoguePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private IEnumerator TypeLine(string line)
    {
        dialogueText.text = string.Empty;
        isTyping = true;

        foreach (char ch in line)
        {
            if (!isTyping)
            {
                dialogueText.text = line;
                break;
            }

            dialogueText.text += ch;
            yield return new WaitForSecondsRealtime(typingTime);
        }

        isTyping = false;
    }
}
