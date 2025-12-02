using System.Collections;
using UnityEngine;

public class Vertical : MonoBehaviour
{
    [Header("Puntos de movimiento")]
    [SerializeField] private Transform puntoArriba;
    [SerializeField] private Transform puntoAbajo;

    [Header("Ajustes")]
    [SerializeField] private float velocidad = 2f;
    [SerializeField] private float pausaArriba = 1f;
    [SerializeField] private float pausaAbajo = 1f;

    private bool yendoArriba = true;
    private bool pausando = false;

    private void Start() // Configuración de puntos
    {
        // Si no hay puntos, creamos unos por defecto
        if (puntoArriba == null) // Crear punto arriba si no está asignado
        {
            GameObject a = new GameObject("PuntoArriba_" + name);
            a.transform.position = transform.position + Vector3.up * 2f; // 2 unidades arriba
            puntoArriba = a.transform; // Asignar el nuevo punto
        }

        if (puntoAbajo == null)
        {
            GameObject b = new GameObject("PuntoAbajo_" + name);
            b.transform.position = transform.position + Vector3.down * 2f; // 2 unidades abajo
            puntoAbajo = b.transform; // Asignar el nuevo punto
        }
    }

    private void Update() // Movimiento vertical con pausas
    {
        if (pausando) return; // Si está pausando, no hacer nada

        Vector3 destino = yendoArriba ? puntoArriba.position : puntoAbajo.position; // Determinar destino

        transform.position = Vector3.MoveTowards(transform.position, destino, velocidad * Time.deltaTime); // Mover hacia el destino

        if (Vector3.Distance(transform.position, destino) < 0.01f) // Si llegó al destino
        {
            StartCoroutine(Pausa()); // Iniciar pausa antes de cambiar dirección
        }
    }

    private IEnumerator Pausa() // Manejo de pausas en el movimiento
    {
        pausando = true;

        yield return new WaitForSeconds(yendoArriba ? pausaArriba : pausaAbajo); // Esperar el tiempo de pausa correspondiente

        yendoArriba = !yendoArriba;
        pausando = false;
    }
}
