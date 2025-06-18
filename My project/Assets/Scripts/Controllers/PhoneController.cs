using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System; // ← Importante para eventos de UI

public class PhoneController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Sprites")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite highlightedSprite;

    [Header("Referencias")]
    [SerializeField] private RectTransform phoneRect;
    [SerializeField] private DialogueSystem dialogueSystem;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        if (image == null)
            Debug.LogError("No se encontró un componente Image en el objeto PhoneControler.");

        if (phoneRect == null)
            Debug.LogWarning("RectTransform no asignado en PhoneControler. Usando el RectTransform propio.");
        else
            ApplyRectTransformSettings();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Entra 1");
        if (dialogueSystem != null)
        {
            Debug.Log("Entra 2");
            dialogueSystem.TriggerDialogue();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.sprite = highlightedSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = normalSprite;
    }

    private void ApplyRectTransformSettings()
    {
        // Aquí podrías usar phoneRect más adelante si lo necesitás
    }
}
