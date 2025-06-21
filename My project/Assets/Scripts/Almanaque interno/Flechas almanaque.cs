using UnityEngine;

public class Flechasalmanaque : MonoBehaviour
{
    [Header("Páginas del almanaque (paneles)")]
    [SerializeField] private GameObject[] paginas;

    [Header("Dirección de avance")]
    [SerializeField] private bool avanzarADerecha = true;

    private int paginaActual = 0;

    private void Start()
    {
        ActivarSoloPagina(paginaActual);
    }

    public void CambiarPagina()
    {
        if (paginas.Length == 0) return;

        paginaActual = avanzarADerecha ?
            (paginaActual + 1) % paginas.Length :
            (paginaActual - 1 + paginas.Length) % paginas.Length;

        Debug.Log(avanzarADerecha
            ? $"Avanzando a la derecha. Página actual: {paginaActual}"
            : $"Avanzando a la izquierda. Página actual: {paginaActual}");

        ActivarSoloPagina(paginaActual);
    }

    private void ActivarSoloPagina(int indice)
    {
        for (int i = 0; i < paginas.Length; i++)
        {
            paginas[i].SetActive(i == indice);
        }
    }
}
