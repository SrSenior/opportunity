using UnityEngine;
using UnityEngine.UI;

public class DoorData : MonoBehaviour
{
    [Header("Sprites para aplicar")]
    [SerializeField] private Sprite spriteCuerpo;
    [SerializeField] private Sprite spritePomo;
    [SerializeField] private Sprite spriteLetrero;

    [Header("Referencias a hijos")]
    [SerializeField] private Image pomoImage;
    [SerializeField] private Image letreroImage;

    [Header("Datos de la puerta")]
    [SerializeField] private AudioClip sonidoBasofono;
    [SerializeField] private AudioClip sonidoKnockKnock;
    [SerializeField] private int nivelRadiacion;  // 0 = bajo, 1 = medio, 2 = alto
    [SerializeField] private int nivelGafas;      // 1 = malo, 2 = neutro, 3 = bueno

    private Image cuerpoImage;

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
    public AudioClip GetSonidoBasofono() => sonidoBasofono;
    public AudioClip GetSonidoKnockKnock() => sonidoKnockKnock;
    public int GetNivelRadiacion() => nivelRadiacion;
    public int GetNivelGafas() => nivelGafas;
}
