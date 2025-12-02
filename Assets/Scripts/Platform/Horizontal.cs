using UnityEngine;

public class Horizontal : MonoBehaviour
{
    [Header("Posiciones")]
    [SerializeField] private Transform puntoA;
    [SerializeField] private Transform puntoB;

    private SpriteRenderer sprite;

    [Header("Velocidad")]
    [SerializeField] private float velocidad = 2.5f;

    private bool yendoHaciaB = true;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

        if (puntoA == null)
        {
            GameObject a = new GameObject("PuntoA_" + name);
            a.transform.position = transform.position;
            puntoA = a.transform;
        }

        if (puntoB == null)
        {
            GameObject b = new GameObject("PuntoB_" + name);
            b.transform.position = transform.position + Vector3.left * 4f;
            puntoB = b.transform;
        }
    }

    void Update()
    {
        Vector2 actual = transform.position;
        Vector2 destino = yendoHaciaB ? puntoB.position : puntoA.position;

        // Movimiento
        transform.position = Vector2.MoveTowards(actual, destino, velocidad * Time.deltaTime);

        // Verificar si llegó al destino
        if ((Vector2)transform.position == destino)
        {
            yendoHaciaB = !yendoHaciaB;
        }
    }
}
