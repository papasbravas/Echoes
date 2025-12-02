using System.Collections.Generic;
using UnityEngine;

public class HiddenPath : MonoBehaviour
{
    [Header("Objeto a recoger")]
    public GameObject objetoARecoger; // El objeto que el jugador debe recoger para revelar el camino

    [Header("Camino oculto")]
    public GameObject caminoOculto; // El camino que se revelará al recoger el objeto
    public List<GameObject> objetosOcultos = new List<GameObject>(); // Lista de objetos ocultos

    private void Start()
    {
        // Asegura que el camino comienza oculto
        if (caminoOculto != null)
        {
            caminoOculto.SetActive(false);
        }
        // Asegura que los enemigos comienzan ocultos
        foreach (var objeto in objetosOcultos)
        {
            if (objeto != null)
            {
                objeto.SetActive(false);
            }
        }
    }

    // Este método debe llamarse cuando el objeto se recoja
    public void OnObjectPickedUp()
    {
        // Revela el camino oculto
        if (caminoOculto != null)
        {
            caminoOculto.SetActive(true);
        }
        // Revela los enemigos ocultos
        foreach (var objeto in objetosOcultos)
        {
            if (objeto != null)
            {
                objeto.SetActive(true);
            }
        }

    }
}
