using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KnockKnockController : MonoBehaviour, IAplicableAPuerta
{
    [Header("Sprites")]
    [SerializeField] private Sprite spriteCentral;
    [SerializeField] private Sprite[] spritesIzquierda;
    [SerializeField] private Sprite[] spritesDerecha;

    [Header("Audio")]
    [SerializeField] private AudioClip sonidoKnockKnock;

    private Image imagen;

    private void Awake()
    {
        imagen = GetComponent<Image>();
        if (imagen != null && spriteCentral != null)
            imagen.sprite = spriteCentral;
    }

    public void AplicarADoor(DoorData puerta)
    {
        StartCoroutine(ProcesoKnockKnock(puerta));
    }

    private IEnumerator ProcesoKnockKnock(DoorData puerta)
    {
        AudioClip respuesta = puerta.GetSonidoKnockKnock();
        bool esIzquierda = !puerta.EsPuertaDerecha();

        Sprite[] sprites = esIzquierda ? spritesIzquierda : spritesDerecha;

        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        // Animación
        float duracion = sonidoKnockKnock.length;
        float tiempoAnim = 0f;
        bool toggle = false;

        audioSource.clip = sonidoKnockKnock;
        audioSource.Play();

        while (tiempoAnim < duracion)
        {
            imagen.sprite = toggle ? sprites[0] : sprites[1];
            toggle = !toggle;
            tiempoAnim += 0.3f;
            yield return new WaitForSeconds(0.3f);
        }

        imagen.sprite = spriteCentral;


        yield return new WaitForSeconds(0.5f);//Esperamos medio segundo antes de reproducir la respuesta
        if (respuesta != null)
        {
            audioSource.clip = respuesta;
            audioSource.Play();
        }
    }
}
