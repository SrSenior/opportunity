using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GeigerController : MonoBehaviour, IAplicableAPuerta
{

    [Header("Sprite Principal")]
    private Image selfImage;
    private Sprite spriteOriginal;

    [Header("Sprites de la barra de carga")]
    [SerializeField] private Sprite[] fasesCarga; // 5 sprites, progresivos

    [Header("Audio")]
    [SerializeField] private AudioClip beepFinal;
    [SerializeField] private AudioClip sonidoBajo;
    [SerializeField] private AudioClip sonidoMedio;
    [SerializeField] private AudioClip sonidoAlto;

    [Header("Geiger Izquierda")]
    [SerializeField] private Image geigerIzq;
    [SerializeField] private Image barraIzq;
    [SerializeField] private Image glowIzq;

    [Header("Geiger Derecha")]
    [SerializeField] private Image geigerDer;
    [SerializeField] private Image barraDer;
    [SerializeField] private Image glowDer;

    [Header("Slot en uso")]
    [SerializeField] private InventorySlotSelector inventorySlot;

    private bool cargando = false;
    private bool esperandoClick = false;
    private DoorData puertaActual;
    private bool esDerecha;
    private bool enUso;
    public bool EstaEnUso => enUso; //Caracterísica de acceso público para ver desde fuera si está en uso o no

    private void Awake()
    {
        selfImage = GetComponent<Image>();
        if (selfImage != null)
            spriteOriginal = selfImage.sprite;
    }

    public void AplicarADoor(DoorData puerta)
    {
        if (cargando || esperandoClick) return;

        puertaActual = puerta;
        esDerecha = puerta.EsPuertaDerecha();

        Image geiger = esDerecha ? geigerDer : geigerIzq;
        Image barra = esDerecha ? barraDer : barraIzq;

        // Oculta este mismo objeto (el de la mano)
        if (selfImage != null)
        {
            selfImage.enabled = false;
        }

        geiger.gameObject.SetActive(true);
        barra.gameObject.SetActive(true);
        glowIzq.gameObject.SetActive(false);
        glowDer.gameObject.SetActive(false);

        puerta.Bloquear();
        StartCoroutine(ProcesoCarga(geiger, barra));
    }

    private IEnumerator ProcesoCarga(Image geiger, Image barra)
    {
        enUso = true;
        inventorySlot.SetInteractable(!enUso); //En teoría, con esto se desactiva el slot del Geiger
        puertaActual.Bloquear();

        cargando = true;
        float duracion = 15f;
        int pasos = fasesCarga.Length;

        for (int i = 0; i < pasos - 1; i++)
        {
            barra.sprite = fasesCarga[i];
            yield return new WaitForSeconds(duracion / pasos);
        }

        barra.sprite = fasesCarga[fasesCarga.Length - 1];
        AudioSource.PlayClipAtPoint(beepFinal, Camera.main.transform.position);

        cargando = false;
        esperandoClick = true;

        // Ilumina el Geiger
        if (esDerecha)
            glowDer.gameObject.SetActive(true);
        else
            glowIzq.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!esperandoClick) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;
            Image geiger = esDerecha ? geigerDer : geigerIzq;

            if (RectTransformUtility.RectangleContainsScreenPoint(geiger.rectTransform, mousePos))
            {
                EjecutarResultado();
            }
        }
    }

    private void EjecutarResultado()
    {
        enUso = false;
        inventorySlot.SetInteractable(!enUso);//En teoría, con esto se activa nuevamente el slot del Geiger

        int nivel = puertaActual.GetNivelRadiacion();
        AudioClip clip = nivel switch
        {
            0 => sonidoBajo,
            1 => sonidoMedio,
            2 => sonidoAlto,
            _ => null
        };

        if (clip != null)
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);

        if (esDerecha)
        {
            geigerDer.gameObject.SetActive(false);
            barraDer.gameObject.SetActive(false);
            glowDer.gameObject.SetActive(false);
        }
        else
        {
            geigerIzq.gameObject.SetActive(false);
            barraIzq.gameObject.SetActive(false);
            glowIzq.gameObject.SetActive(false);
        }

        // Reactiva el sprite en la mano
        if (selfImage != null)
        {
            selfImage.enabled = true;
            selfImage.sprite = spriteOriginal;
        }

        esperandoClick = false;
        puertaActual.Desbloquear();
        puertaActual = null;
    }
}