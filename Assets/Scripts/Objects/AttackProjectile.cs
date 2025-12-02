using UnityEngine;

public class AttackProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float lifeTime = 2f;
    [SerializeField] private int damage = 1;
    private Vector2 direction = Vector2.right;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SetDirection(Vector2 dir)
    {
        // invertir dirección
        direction = -dir.normalized;

        // ajustar rotación si tu FX necesita orientarse
        if (direction.x < 0)
        {
            Vector3 s = transform.localScale;
            s.x = -Mathf.Abs(s.x);
            transform.localScale = s;
        }
        else
        {
            Vector3 s = transform.localScale;
            s.x = Mathf.Abs(s.x);
            transform.localScale = s;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ejemplo: aplicar daño al jugador
        if (other.CompareTag("Player"))
        {
            // implementa daño si tu player tiene método público
            // other.GetComponent<PlayerHealth>()?.TakeDamage(damage);
        }

        // destruir al impactar en cualquier cosa (opcional: filtrar)
        Destroy(gameObject);
    }
}
