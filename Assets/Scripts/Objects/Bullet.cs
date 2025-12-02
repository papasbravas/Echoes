using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Ajustes de movimiento")][SerializeField] private int velocidad;
    [Header("Tiempo de vida")][SerializeField] private int tiempoVida;
    private Rigidbody2D rb;
    [Header("Sonidos")][SerializeField] private AudioSource sonidoDisparo;
    [SerializeField] private AudioSource sonidoExplosion;
    [Header("Efectos")][SerializeField] private GameObject efectoImpacto;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (sonidoDisparo != null)
        {
            sonidoDisparo.Play();
        }
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * velocidad;
        Destroy(gameObject, tiempoVida);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            animator.SetTrigger("Colision");

            // Llamar a la función de daño del enemigo
            EnemiesAnimation enemigo = other.GetComponent<EnemiesAnimation>();
            if (enemigo != null)
            {
                enemigo.RecibirDaño(1); // 1 punto de daño por disparo
            }

            if (sonidoExplosion != null)
                sonidoExplosion.Play();

            Destroy(gameObject, 0.3f);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
