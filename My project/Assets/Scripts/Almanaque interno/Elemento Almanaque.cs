using UnityEngine;
using UnityEngine.EventSystems;

public class ElementoAlmanaque : MonoBehaviour, IPointerClickHandler
{
    [Header("Panel principal asociado a este elemento")]
    [SerializeField] private GameObject panelObjetivo;

    [Header("Otros paneles que deben cerrarse al hacer clic")]
    [SerializeField] private GameObject[] panelesParaCerrar;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (panelObjetivo == null)
        {
            Debug.LogWarning("No se asignó un panel en ElementoAlmanaque.");
            return;
        }

        // Cerrar todos los paneles de otros elementos
        foreach (GameObject panel in panelesParaCerrar)
        {
            if (panel != null && panel != panelObjetivo)
                panel.SetActive(false);
        }

        // Alternar visibilidad del panel propio
        bool nuevoEstado = !panelObjetivo.activeSelf;
        panelObjetivo.SetActive(nuevoEstado);
    }
}
