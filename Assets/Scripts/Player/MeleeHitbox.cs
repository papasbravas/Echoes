using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>(); // Ahora sí obtiene el Animator
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (animator != null)
                animator.SetTrigger("Colision");

            // Llamar a la función de daño del enemigo
            EnemiesAnimation enemigo = other.GetComponent<EnemiesAnimation>();
            if (enemigo != null)
            {
                enemigo.RecibirDaño(2); // 1 punto de daño por golpe
            }

            // Destruir tras la animación
            //Destroy(gameObject, 0.3f);
        }
    }
}
