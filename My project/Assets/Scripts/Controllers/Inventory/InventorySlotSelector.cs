using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotSelector : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int slotIndex; // 1 = vasófono, 2 = knock knock, etc.
    [SerializeField] private InventoryManager inventoryManager;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (inventoryManager != null)
        {
            inventoryManager.SeleccionarSlot(slotIndex);
        }
        else
        {
            Debug.LogWarning("InventoryManager no asignado en el slot.");
        }
    }
}
