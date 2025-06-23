using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DoorData : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Sprites para aplicar")]
    [SerializeField] private Sprite spriteCuerpo;
    [SerializeField] private Sprite spritePomo;
    [SerializeField] private Sprite spriteLetrero;

    [Header("Referencias a hijos")]
    [SerializeField] private Image pomoImage;
    [SerializeField] private Image letreroImage;
    [SerializeField] private GameObject highligh; //Cuando pasan el mouse sobre

    [Header("Datos de la puerta")]
    [SerializeField] private AudioClip sonidoVasofono;
    [SerializeField] private AudioClip sonidoKnockKnock;
    [SerializeField] private int nivelRadiacion;  // 0 = bajo, 1 = medio, 2 = alto
    [SerializeField] private int nivelGafas;      // 1 = malo, 2 = neutro, 3 = bueno
    [SerializeField] private bool esPuertaDerecha;

    public bool EsPuertaDerecha() => esPuertaDerecha; //Esto viene a ser equivalente al Getter de si es o no puerta derecha

    private Image cuerpoImage;
    private bool usable = true; //Para bloquear/desbloquear la puerta

    private void Awake()
    {
        cuerpoImage = GetComponent<Image>();
        if (cuerpoImage == null)
            Debug.LogError("No se encontró un componente Image en el objeto padre (puerta).");
    }

    private void Start()
    {
        AplicarSprites();
    }

    private void AplicarSprites()
    {
        if (cuerpoImage != null && spriteCuerpo != null)
            cuerpoImage.sprite = spriteCuerpo;

        if (pomoImage != null && spritePomo != null)
            pomoImage.sprite = spritePomo;

        if (letreroImage != null && spriteLetrero != null)
            letreroImage.sprite = spriteLetrero;
    }

    public void AsignarSprites(Sprite cuerpo, Sprite pomo, Sprite letrero)
    {
        spriteCuerpo = cuerpo;
        spritePomo = pomo;
        spriteLetrero = letrero;
        AplicarSprites();
    }

    // Getters públicos para herramientas
    public AudioClip GetSonidoVasofono() => sonidoVasofono;
    public AudioClip GetSonidoKnockKnock() => sonidoKnockKnock;
    public int GetNivelRadiacion() => nivelRadiacion;
    public int GetNivelGafas() => nivelGafas;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (cuerpoImage.raycastTarget)
            highligh.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (cuerpoImage.raycastTarget)
            highligh.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(usable)
        {
            var herramienta = InventoryManager.Instancia.ObtenerHerramientaActiva();
            if (herramienta != null)
            {
                herramienta.AplicarADoor(this);
            }
        }
    }

    public void Bloquear()
    {
        usable = false;
    }

    public void Desbloquear()
    {
        usable = true;
    }

}
