using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyCollider : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float tiempoEsperaMuerte = 1.5f;
    [SerializeField] private AudioSource sonidoMuerte;

    private PlayerMovement playerMove;
    private PlayerAnimations playerAnimation;

    [SerializeField] private AudioSource sonidoDaño;

    private bool inmune = false;

    private VidasPlayer vidasPlayer;

    void Start()
    {
        playerMove = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<PlayerAnimations>();

        vidasPlayer = FindObjectOfType<VidasPlayer>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy") && !inmune)
        {
            sonidoDaño.Play();
            StartCoroutine(RecibirDaño());
        }

        if (other.collider.CompareTag("dead"))
        {
            sonidoMuerte.Play();
            StartCoroutine(MorirPorCaida());
        }
    }

    private IEnumerator RecibirDaño()
    {
        inmune = true;

        // Restar una vida real
        vidasPlayer?.LoseLife(1);

        if (vidasPlayer.currentHealth > 0)
        {
            // Solo daño
            playerAnimation.AnimacionDaño();
            yield return new WaitForSeconds(0.8f);
            inmune = false;
        }
        else
        {
            // Muerte real
            if (sonidoMuerte != null) sonidoMuerte.Play();

            playerAnimation.AnimacionMuerte();
            playerMove.Parar();

            yield return new WaitForSecondsRealtime(tiempoEsperaMuerte);

            SceneManager.LoadScene(1);
        }
    }

    private IEnumerator MorirPorCaida()
    {
        playerAnimation.AnimacionMuerte();
        playerMove.Parar();

        if (sonidoMuerte != null)
            sonidoMuerte.Play();

        yield return new WaitForSecondsRealtime(tiempoEsperaMuerte);

        SceneManager.LoadScene(1);
    }
}
