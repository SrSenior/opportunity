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

    private void Awake()
    {
        imagen = GetComponent<Image>();
        if (imagen != null && spriteCentral != null)
            imagen.sprite = spriteCentral;
    }

    public void AplicarADoor(DoorData puerta)
    {
        StartCoroutine(ProcesoVasofono(puerta));
    }

    private IEnumerator ProcesoVasofono(DoorData puerta)
    {
        AudioClip respuesta = puerta.GetSonidoVasofono();
        bool esIzquierda = !puerta.EsPuertaDerecha();

        if(esIzquierda)
        {
            spriteCableIzquierda.SetActive(true);
            spriteCableDerecha.SetActive(false);
        }
        else
        {
            spriteCableDerecha.SetActive(true);
            spriteCableIzquierda.SetActive(false);
        }

        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        yield return new WaitForSeconds(0.5f);//Esperamos medio segundo antes de reproducir la respuesta
        if (respuesta != null)
        {
            audioSource.clip = respuesta;
            audioSource.Play();
        }
    }
}
