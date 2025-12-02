using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject menuPausa;

    private bool isPaused = false;
    public static bool InputsBloqueados = false;


    // Llamado por el botón
    public void TogglePause()
    {
        if (isPaused) Reanudar();
        else Pausar();
    }

    private void Pausar()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true;

        PauseManager.InputsBloqueados = true;  // Bloquea inputs

        isPaused = true;
    }

    public void Reanudar()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false;

        PauseManager.InputsBloqueados = false; // Desbloquea inputs

        isPaused = false;
    }

}
