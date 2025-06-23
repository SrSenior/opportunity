using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VasófonoController : MonoBehaviour, IAplicableAPuerta
{
    [Header("Sprites")]
    [SerializeField] private Sprite spriteCentral;
    [SerializeField] private GameObject spriteCableIzquierda;
    [SerializeField] private GameObject spriteCableDerecha;

    private Image imagen;
    private AudioSource audioSource;
    private bool cableIzquierdoActivo = false;
    private bool cableDerechoActivo = false;
    private Coroutine rutina;

    private void Awake()
    {
        imagen = GetComponent<Image>();
        if (imagen != null && spriteCentral != null)
            imagen.sprite = spriteCentral;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void AplicarADoor(DoorData puerta)
    {
        bool esIzquierda = !puerta.EsPuertaDerecha();

        // Si el cable correspondiente ya está activo, desactivarlo, detener sonido y salir
        if (esIzquierda && cableIzquierdoActivo)
        {
            spriteCableIzquierda.SetActive(false);
            cableIzquierdoActivo = false;
            if (audioSource.isPlaying)
                audioSource.Stop();
            if (rutina != null)
            {
                StopCoroutine(rutina);
                rutina = null;
            }
            return;
        }
        else if (!esIzquierda && cableDerechoActivo)
        {
            spriteCableDerecha.SetActive(false);
            cableDerechoActivo = false;
            if (audioSource.isPlaying)
                audioSource.Stop();
            if (rutina != null)
            {
                StopCoroutine(rutina);
                rutina = null;
            }
            return;
        }

        // Activar el cable correspondiente y desactivar el otro
        if (esIzquierda)
        {
            spriteCableIzquierda.SetActive(true);
            cableIzquierdoActivo = true;

            spriteCableDerecha.SetActive(false);
            cableDerechoActivo = false;
        }
        else
        {
            spriteCableDerecha.SetActive(true);
            cableDerechoActivo = true;

            spriteCableIzquierda.SetActive(false);
            cableIzquierdoActivo = false;
        }

        // Detener rutina anterior por si acaso
        if (rutina != null)
        {
            StopCoroutine(rutina);
            rutina = null;
        }

        rutina = StartCoroutine(ProcesoVasofono(puerta));
    }

    private IEnumerator ProcesoVasofono(DoorData puerta)
    {
        AudioClip respuesta = puerta.GetSonidoVasofono();

        yield return new WaitForSeconds(0.5f); // Espera antes del sonido

        if (respuesta != null)
        {
            audioSource.clip = respuesta;
            audioSource.Play();
        }

        rutina = null;
    }
}
