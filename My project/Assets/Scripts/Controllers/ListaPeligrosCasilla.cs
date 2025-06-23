using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CasillaPeligro : MonoBehaviour, IPointerClickHandler
{
    public enum EstadoPeligro
    {
        Seguro,
        Neutro,
        Peligro
    }

    [Header("Sprites de estado")]
    [SerializeField] private Sprite spriteSeguro;
    [SerializeField] private Sprite spriteNeutro;
    [SerializeField] private Sprite spritePeligro;

    [Header("¿Esta casilla es automática?")]
    [SerializeField] private bool esCalculada = false;

    private EstadoPeligro estadoActual = EstadoPeligro.Seguro;
    private Image imagen;

    private void Awake()
    {
        imagen = GetComponent<Image>();
        ActualizarSprite();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (esCalculada) return;

        // Ciclar entre los tres estados
        estadoActual = (EstadoPeligro)(((int)estadoActual + 1) % 3);
        ActualizarSprite();
    }

    private void ActualizarSprite()
    {
        switch (estadoActual)
        {
            case EstadoPeligro.Seguro:
                imagen.sprite = spriteSeguro;
                break;
            case EstadoPeligro.Neutro:
                imagen.sprite = spriteNeutro;
                break;
            case EstadoPeligro.Peligro:
                imagen.sprite = spritePeligro;
                break;
        }

        imagen.enabled = true;
    }

    public EstadoPeligro ObtenerEstado() => estadoActual;

    public void EstablecerEstado(EstadoPeligro nuevoEstado)
    {
        estadoActual = nuevoEstado;
        ActualizarSprite();
    }
}
