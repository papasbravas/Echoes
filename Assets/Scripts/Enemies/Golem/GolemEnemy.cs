using UnityEngine;

public class GolemMovement : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private Transform puntoA;
    [SerializeField] private Transform puntoB;
    [SerializeField] private float velocidad = 3f;

    private bool yendoHaciaB = true;
    private SpriteRenderer sprite;

    [Header("Ataque")]
    [SerializeField] private GameObject efectoAtaque;
    [SerializeField] private Transform puntoAtaque; // Child delante del golem
    [SerializeField] private float tiempoEntreAtaques = 5f;
    private float cooldown;
    [SerializeField] private Animator animator;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        if (animator == null) animator = GetComponent<Animator>();
        cooldown = tiempoEntreAtaques;

        // Si no hay puntos asignados, crearlos
        if (puntoA == null) puntoA = CrearPunto("PuntoA", transform.position);
        if (puntoB == null) puntoB = CrearPunto("PuntoB", transform.position + Vector3.left * 4f);
    }

    private Transform CrearPunto(string nombre, Vector3 pos)
    {
        GameObject go = new GameObject(nombre + "_" + name);
        go.transform.position = pos;
        return go.transform;
    }

    private void Update()
    {
        Mover();
        GestionarAtaque();
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

    private void GestionarAtaque()
    {
        cooldown -= Time.deltaTime;
        if (cooldown <= 0)
        {
            Ataque();
            cooldown = tiempoEntreAtaques;
        }
    }

    private void Ataque()
    {
        if (animator != null) animator.SetTrigger("Ataque");

        if (efectoAtaque != null && puntoAtaque != null)
        {
            // instanciamos el proyectil en el puntoAtaque
            GameObject fx = Instantiate(efectoAtaque, puntoAtaque.position, Quaternion.identity);

            // determinar direcci�n seg�n el sprite
            Vector2 dir = sprite.flipX ? Vector2.left : Vector2.right;

            // si tiene AttackProjectile
            AttackProjectile proj = fx.GetComponent<AttackProjectile>();
            if (proj != null)
                proj.SetDirection(dir);
            else
            {
                Rigidbody2D rb = fx.GetComponent<Rigidbody2D>();
                if (rb != null)
                    rb.linearVelocity = dir * 3f; // velocidad del proyectil
            }

            // ajustar escala X para que mire hacia el frente
            Vector3 s = fx.transform.localScale;
            s.x = Mathf.Abs(s.x) * (sprite.flipX ? -1f : 1f);
            fx.transform.localScale = s;
        }
    }
}
