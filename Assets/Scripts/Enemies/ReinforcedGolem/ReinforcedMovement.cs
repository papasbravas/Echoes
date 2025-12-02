using UnityEngine;

public class ReinforcedMovement : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private Transform puntoA;
    [SerializeField] private Transform puntoB;
    [SerializeField] private float velocidad = 1.5f;

    private bool yendoHaciaB = true;
    private SpriteRenderer sprite;

    //[Header("Ataque")]
    //[SerializeField] private GameObject efectoAtaque;
    //[SerializeField] private Transform puntoAtaque; // Child delante del golem
    //[SerializeField] private float tiempoEntreAtaques = 5f;
    //private float cooldown;
    [SerializeField] private Animator animator;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        if (animator == null) animator = GetComponent<Animator>();
        if (puntoA == null) puntoA = CrearPunto("PuntoA", transform.position);
        if (puntoB == null) puntoB = CrearPunto("PuntoB", transform.position + Vector3.left * 4f);
    }

    private Transform CrearPunto(string nombre, Vector3 pos)
    {
        GameObject go = new GameObject(nombre + "_" + name);
        go.transform.position = pos;
        return go.transform;
    }

    private void Mover()
    {
        Transform destino = yendoHaciaB ? puntoB : puntoA;
        transform.position = Vector2.MoveTowards(transform.position, destino.position, velocidad * Time.deltaTime);

        if ((Vector2)transform.position == (Vector2)destino.position)
        {
            yendoHaciaB = !yendoHaciaB;
            sprite.flipX = !sprite.flipX; // girar visualmente
        }
    }
    // Update is called once per frame
    private void Update()
    {
        Mover();
    }
}
