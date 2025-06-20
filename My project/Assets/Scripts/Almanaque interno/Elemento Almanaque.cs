using UnityEngine;
using UnityEngine.EventSystems;

public class ElementoAlmanaque : MonoBehaviour, IPointerClickHandler
{
    [Header("Panel asociado")]
    [SerializeField] private GameObject panelObjetivo;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (panelObjetivo != null)
        {
            bool nuevoEstado = !panelObjetivo.activeSelf;
            panelObjetivo.SetActive(nuevoEstado);
        }
        else
        {
            Debug.LogWarning("No se asignó un panel en ElementoAlmanaque.");
        }
    }
}
