using UnityEngine;
using UnityEngine.EventSystems;

public class CloseBookPanel : MonoBehaviour, IPointerClickHandler
{

    [SerializeField] private GameObject openbookPanel;
    public void OnPointerClick(PointerEventData eventData)
    {
        openbookPanel.SetActive(false);
    }

}
