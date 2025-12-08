using UnityEngine;

public class DetectStomp : MonoBehaviour
{
    [SerializeField] private float fuerzaRebote = 2f;
    private EnemiesAnimation enemy;
    //[SerializeField] private AudioSource sonidoStomp;

    void Start()
    {
        enemy = GetComponentInParent<EnemiesAnimation>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        // Matar enemigo
        if (enemy != null)
        {
            //sonidoStomp.Play();
            enemy.AnimacionMuerte();
        }
            
        // Rebotar al jugador
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0); // reset Y
            rb.AddForce(Vector2.up * fuerzaRebote, ForceMode2D.Impulse);
        }
    }
}
