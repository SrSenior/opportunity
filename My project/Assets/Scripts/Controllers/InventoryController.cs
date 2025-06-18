using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // ← Importante para eventos de UI

public class InventoryController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Sprites")]
    [SerializeField] private Image inventoryImage; // referencia al objeto padre del inventario
    [SerializeField] private Sprite slotSprite; // sprite que representa este slot iluminado
    [SerializeField] private Sprite defaultSprite; // sprite normal

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (inventoryImage != null && slotSprite != null)
            inventoryImage.sprite = slotSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (inventoryImage != null && defaultSprite != null)
            inventoryImage.sprite = defaultSprite;
    }
}
