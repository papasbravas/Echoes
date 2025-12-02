using UnityEngine;

public class BatMovement : MonoBehaviour
{
    [Header("Posiciones")]
    [SerializeField] private Transform puntoA;
    [SerializeField] private Transform puntoB;

    private SpriteRenderer sprite;

    [Header("Velocidad")]
    [SerializeField] private float velocidad = 2f;

    private bool yendoHaciaB = true;
    private bool mirandoDerecha = true;

    [Header("Puntos dinámicos")]
    public int puntos;

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
            Girar();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            Girar();
        }
        

    }
    private void Girar()
    {
        // Cambia el estado de mirandoDerecha y voltea el sprite
        mirandoDerecha = !mirandoDerecha;
        sprite.flipX = !mirandoDerecha;
    }
}
