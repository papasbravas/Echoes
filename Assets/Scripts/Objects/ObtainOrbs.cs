using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ObtainOrbs : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;

    public HiddenPath path;
    public bool activaElCamino = false;

    [Header("UI")]
    public GameObject victoryCanvas; // Asigna el Canvas de victoria desde el Inspector
    private bool isPaused = false;
    public static bool InputsBloqueados = false; // Variable estática para bloquear inputs
    void Start()
    {
        var rb = GetComponent<Rigidbody2D>(); // Obtener el Rigidbody2D si existe
        if (rb != null)
        {
            rb.gravityScale = 0;
        }

        // Aseguramos que el Canvas de victoria esté desactivado al iniciar
        if (victoryCanvas != null)
        {
            victoryCanvas.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) // Detecta colisiones con el jugador
    {
        if (other.CompareTag("Player")) // Verifica si el objeto que colisiona es el jugador
        {
            ContadorOrbs contador = other.GetComponent<ContadorOrbs>(); // Obtener el componente ContadorOrbs del jugador
            if (contador != null)
            {
                contador.orbsRestantes--; // Disminuye el número de orbes
                UIOrbes ui = FindObjectOfType<UIOrbes>();
                if (ui != null)
                    ui.ActualizarUI();
                if (contador.orbsRestantes <= 0) // Verifica si se han recogido todos los orbes
                {
                    Debug.Log("Victoria");

                    if (victoryCanvas != null) // Verifica si el Canvas de victoria está asignado
                    {
                        victoryCanvas.SetActive(true); // Activamos el Canvas de victoria
                        Time.timeScale = 0f; // Pausa el juego
                        AudioListener.pause = true;

                        PauseManager.InputsBloqueados = true;  // Bloquea inputs

                        isPaused = true;
                    }
                }
            }

            if (audioSource != null) // Reproduce el sonido de recogida si está asignado
            {
                audioSource.Play();
                Destroy(gameObject, audioSource.clip.length); // Destruye el objeto después de que termine el sonido
            }
            else
            {
                // Si no hay sonido, destruye el objeto inmediatamente
                if (activaElCamino && path != null) // Activa el camino oculto si corresponde
                {
                    path.OnObjectPickedUp();
                }

                Destroy(gameObject);
            }
        }
    }
}
