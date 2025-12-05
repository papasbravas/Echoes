using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [Header("Músicas")]
    //public AudioSource menuMusic;
    public AudioSource nivel1Music;
    public AudioSource nivel2Music;
    public AudioSource nivel3Music;

    private AudioSource musicaActual;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene escena, LoadSceneMode modo)
    {
        CambiarMusicaSegunEscena(escena.buildIndex);
    }

    private void CambiarMusicaSegunEscena(int index)
    {
        // Parar música previa
        if (musicaActual != null)
            musicaActual.Stop();

        switch (index)
        {

            case 0: // Nivel 1
                musicaActual = nivel1Music;
                break;

            case 3: // Nivel 2
                musicaActual = nivel2Music;
                break;

            case 4: // Nivel 3
                musicaActual = nivel3Music;
                break;
        }

        musicaActual.Play();
    }
}
