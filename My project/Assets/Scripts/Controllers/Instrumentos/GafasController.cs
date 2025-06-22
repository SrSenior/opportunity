using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GafasController : MonoBehaviour, IAplicableAPuerta
{
    [SerializeField] private List<VideoGafasSet> setsDeVideos;

    public void AplicarADoor(DoorData puerta)
    {
        StartCoroutine(ProcesoGafas(puerta));
    }

    private IEnumerator ProcesoGafas(DoorData puerta)
    {
        Debug.Log("Iniciando proceso para puerta: " + (puerta.EsPuertaDerecha() ? "Derecha" : "Izquierda"));

        bool esDerecha = puerta.EsPuertaDerecha();
        int nivel = puerta.GetNivelGafas(); // 1 = malo, 2 = neutro, 3 = bueno

        VideoGafasSet set = setsDeVideos.Find(s => s.esPuertaDerecha == esDerecha);
        if (set != null)
        {
            GameObject video = null;
            switch (nivel)
            {
                case 1: video = set.videoMalo; break;
                case 2: video = set.videoNeutro; break;
                case 3: video = set.videoBueno; break;
            }

            if (video != null)
                video.SetActive(true);
        }

        yield return new WaitForSeconds(0.5f);
    }

}
