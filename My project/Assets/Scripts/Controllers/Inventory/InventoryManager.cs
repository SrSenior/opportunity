using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [Header("Instrumentos")]
    [SerializeField] private GameObject vasofono;
    [SerializeField] private GameObject knockKnock;
    [SerializeField] private GameObject geiger;
    [SerializeField] private GameObject gafas;

    [Header("Puertas")]
    [SerializeField] private DoorData puertaIzquierda;
    [SerializeField] private DoorData puertaDerecha;

    [Header("Scripts")]
    [SerializeField] private GafasController gafasController;



    private GameObject herramientaActiva = null;

    public static InventoryManager Instancia { get; private set; }

    private void Awake()
    {
        if (Instancia == null)
            Instancia = this;
        else
            Destroy(gameObject);
    }

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

        // Lógica especial para las gafas (slot 4)
        if (slotIndex == 4 && gafasController != null && puertaIzquierda != null && puertaDerecha != null)
        {
            gafasController.AplicarADoor(puertaIzquierda);
            gafasController.AplicarADoor(puertaDerecha);
        }

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

    public IAplicableAPuerta ObtenerHerramientaActiva()
    {
        return herramientaActiva?.GetComponent<IAplicableAPuerta>();
    }
}
