using UnityEngine;
using System.Collections;

public class EnemiesAnimation : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] private Animator animator;
    private Rigidbody2D rb;
    private SlimeMovement enemyMovement;
    private Collider2D enemyCollider;

    [Header("Vida y Muerte")]
    [SerializeField] private int vida = 2;
    [SerializeField] private float tiempoDestruccion = 1f;
    [SerializeField] private AudioSource sonidoMuerte;

    private bool muriendo = false;
    private LevelEnemyManager levelManager;
    private bool yaNotificado = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyMovement = GetComponent<SlimeMovement>();
        enemyCollider = GetComponent<Collider2D>();
        levelManager = FindObjectOfType<LevelEnemyManager>();
    }

    public void RecibirDaño(int daño)
    {
        if (muriendo) return;

        vida -= daño;
        if (vida <= 0)
        {
            StartCoroutine(Morir());
        }
        else
        {
            animator.SetTrigger("Daño");
        }
    }

    private IEnumerator Morir()
    {
        muriendo = true;

        // Desactivar movimiento y colisiones
        if (enemyMovement != null)
            enemyMovement.enabled = false;

        if (enemyCollider != null)
            enemyCollider.enabled = false;

        // Activar animación de muerte
        animator.SetTrigger("Muerte");

        // Reproducir sonido de muerte
        if (sonidoMuerte != null)
        {
            sonidoMuerte.Play();
        }

        // Esperar a que la animación termine
        yield return new WaitForSeconds(tiempoDestruccion);
        if (!yaNotificado)
        {
            yaNotificado = true;
            if (levelManager != null)
            {
                levelManager.EnemigoEliminado();
            }
            else
            {
                Debug.LogWarning($"[EnemiesAnimation] No se encontró LevelEnemyManager para el enemigo {name}.");
            }
        }
        Destroy(gameObject);
    }

    // Método que puede ser llamado desde la bala
    public void AnimacionMuerte()
    {
        StartCoroutine(Morir());
    }
}
