using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotSelector : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int slotIndex; // 1 = vasófono, 2 = knock knock, etc.
    [SerializeField] private InventoryManager inventoryManager;
    private bool usable = true;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (usable)
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

    // Esta función pública permite activar o desactivar la interacción con el slot
    public void SetInteractable(bool estado)
    {
        usable = estado;
    }
}
