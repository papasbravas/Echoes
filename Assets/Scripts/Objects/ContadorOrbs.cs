using UnityEngine;

public class ContadorOrbs : MonoBehaviour
{
    public int totalOrbes;       // Número total detectado
    public int orbsRestantes;    // Orbes que faltan por recoger

    void Start()
    {
        // Encuentra TODOS los objetos tipo ObtainOrbs, incluso los desactivados
        ObtainOrbs[] todosLosOrbes = Resources.FindObjectsOfTypeAll<ObtainOrbs>();

        int contador = 0;

        foreach (var orb in todosLosOrbes)
        {
            // Evita contar objetos que no son parte de la escena (prefabs)
            if (orb.gameObject.scene.IsValid())
            {
                contador++;
            }
        }

        totalOrbes = contador;
        orbsRestantes = contador;

        Debug.Log("Orbes totales detectados en escena: " + totalOrbes);
    }
}
