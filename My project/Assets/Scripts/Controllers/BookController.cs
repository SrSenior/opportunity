using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // ← Importante para eventos de UI

public class BookController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Sprites")]
    [SerializeField] private Sprite normalSprite;
    [SerializeField] private Sprite highlightedSprite;

    [Header("Referencias")]
    [SerializeField] private RectTransform bookRect;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        if (image == null)
            Debug.LogError("No se encontró un componente Image en el objeto PhoneControler.");

        if (bookRect == null)
            Debug.LogWarning("RectTransform no asignado en BookController. Usando el RectTransform propio.");
        else
            ApplyRectTransformSettings();
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
