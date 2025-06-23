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

    public bool InstrumentoEnUso { get; private set; } /*
                                                        * Se comprueba si el instrumento est� en uso.Esta estructura
                                                        * vendr�a a ser algo como un Singleton abreviado. Tiene un
                                                        * m�todo get para conocer su estado y un set privado para
                                                        * evitar que otros m�todos modifiquen su valor. En resumen,
                                                        * puedes ver su estado desde fuera, pero no modificarlo
                                                        */
    public void BloquearInventario() => InstrumentoEnUso = true;
    public void DesbloquearInventario() => InstrumentoEnUso = false;

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

        if (InstrumentoEnUso) return; //Si hay un instrumento ejecut�ndose, se bloquea el inventario

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
        {
            // Si el activo es el Geiger y est� en uso, no se desactiva
            GeigerController geigerController = herramientaActiva.GetComponent<GeigerController>();
            if (geigerController == null || !geigerController.EstaEnUso)
            {
                herramientaActiva.SetActive(false);
            }
        }

        herramientaSeleccionada.SetActive(true);
        herramientaActiva = herramientaSeleccionada;

        // L�gica especial para las gafas (slot 4)
        if (slotIndex == 4 && gafasController != null && puertaIzquierda != null && puertaDerecha != null)
        {
            gafasController.AplicarADoor(puertaIzquierda);
            gafasController.AplicarADoor(puertaDerecha);
        }

    }

    public int ObtenerIndiceHerramientaActiva()
    {
        if (herramientaActiva == null) return 0;
        if (herramientaActiva == vasofono) return 1;
        if (herramientaActiva == knockKnock) return 2;
        if (herramientaActiva == geiger) return 3;
        if (herramientaActiva == gafas) return 4;

        return 0;
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
                Debug.LogWarning("�ndice de slot no v�lido: " + index);
                return null;
        }
    }

    public IAplicableAPuerta ObtenerHerramientaActiva()
    {
        return herramientaActiva?.GetComponent<IAplicableAPuerta>();
    }
}
