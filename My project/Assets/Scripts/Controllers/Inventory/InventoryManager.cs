using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Instrumentos")]
    [SerializeField] private GameObject vasofono;
    [SerializeField] private GameObject knockKnock;
    [SerializeField] private GameObject geiger;
    [SerializeField] private GameObject gafas;

    private GameObject herramientaActiva = null;

    public void SeleccionarSlot(int slotIndex)
    {
        GameObject herramientaSeleccionada = ObtenerHerramientaPorIndice(slotIndex);

        if (herramientaSeleccionada == null)
            return;

        if (herramientaSeleccionada == herramientaActiva)
        {
            herramientaActiva.SetActive(false);
            herramientaActiva = null;
            return;
        }

        if (herramientaActiva != null)
            herramientaActiva.SetActive(false);

        herramientaSeleccionada.SetActive(true);
        herramientaActiva = herramientaSeleccionada;
    }

    private GameObject ObtenerHerramientaPorIndice(int index)
    {
        switch (index)
        {
            case 1: return vasofono;
            case 2: return knockKnock;
            case 3: return geiger;
            case 4: return gafas;
            default:
                Debug.LogWarning("Índice de slot no válido: " + index);
                return null;
        }
    }
}
