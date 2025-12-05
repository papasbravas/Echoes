using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    [Header("Paneles del Menú")]
    [SerializeField] private GameObject panelPrincipal;
    [SerializeField] private GameObject panelOpciones;

    [Header("Música")]
    [SerializeField] private AudioSource musicaFondo;
    //[SerializeField] private AudioSource musicaNivel1;
    //[SerializeField] private AudioSource musicaNivel2;
    //[SerializeField] private AudioSource musicaNivel3;
    [SerializeField] private float duracionFade = 2f;

    [Header("Opciones de Sonido")]
    [SerializeField] private Slider sliderVolumen;
    private float volumenActual = 1f;

    void Start()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Solo activar los paneles automáticamente si estamos en el MENU PRINCIPAL (escena 1)
        if (sceneIndex == 1)
        {
            if (panelPrincipal != null && panelOpciones != null)
            {
                panelPrincipal.SetActive(true);
                panelOpciones.SetActive(false);
            }

            // Música del menú principal
            if (musicaFondo != null && !musicaFondo.isPlaying)
            {
                musicaFondo.loop = true;
                musicaFondo.Play();
            }
        }
        else
        {
            // Si estamos en un nivel: NO activar paneles del menú principal
            if (panelPrincipal != null) panelPrincipal.SetActive(false);
            if (panelOpciones != null) panelOpciones.SetActive(false);
        }

        // Configurar slider volumen
        if (sliderVolumen != null)
        {
            volumenActual = PlayerPrefs.GetFloat("Volumen", 1f);
            sliderVolumen.value = volumenActual;
            ActualizarVolumen(volumenActual);
            sliderVolumen.onValueChanged.AddListener(ActualizarVolumen);
        }
    }

    // --- AJUSTES DE VOLUMEN ---
    public void ActualizarVolumen(float valor)
    {
        volumenActual = valor;
        AudioListener.volume = volumenActual;
        PlayerPrefs.SetFloat("Volumen", volumenActual);
    }

    // --- NAVEGACIÓN ENTRE PANELES ---
    public void AbrirOpciones()
    {
        if(panelPrincipal != null)
        {
            panelPrincipal.SetActive(false);
        }
        //panelPrincipal.SetActive(false);
        panelOpciones.SetActive(true);
    }

    public void VolverAlMenu()
    {
        panelPrincipal.SetActive(true);
        panelOpciones.SetActive(false);
    }

    // --- INICIAR JUEGO ---
    public void Load()
    {
        StartCoroutine(FadeOutYLoad());
    }

    private IEnumerator FadeOutYLoad()
    {
        if (musicaFondo != null)
        {
            float volumenInicial = musicaFondo.volume;

            for (float t = 0; t < duracionFade; t += Time.deltaTime)
            {
                musicaFondo.volume = Mathf.Lerp(volumenInicial, 0, t / duracionFade);
                yield return null;
            }

            musicaFondo.volume = 0;
            musicaFondo.Stop();
        }

        SceneManager.LoadScene(2);
    }

    // --- SALIR DEL JUEGO ---
    public void Salir()
    {
        Debug.Log("Saliendo del juego...");

        // Directiva de preprocesador
        #if UNITY_EDITOR
                // Si estamos en el editor de Unity, usamos el comando para detener el juego.
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                        // Si estamos en un ejecutable (Build), cerramos la aplicación.
                        Application.Quit();
        #endif
    }
    public void CargarNivel1()
    {
        StartCoroutine(FadeOutYLoad());
        //musicaNivel1.Play();
        PlayerPrefs.SetInt("nivelJugador", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }

    public void CargarNivel2()
    {
        StartCoroutine(FadeOutYLoad());
        //musicaNivel2.Play();
        PlayerPrefs.SetInt("nivelJugador", 2);
        PlayerPrefs.Save();
        SceneManager.LoadScene(3);
        Time.timeScale = 1f;
        PauseManager.InputsBloqueados = false;
    }

    public void CargarNivel3()
    {
        StartCoroutine(FadeOutYLoad());
        //musicaNivel3.Play();
        PlayerPrefs.SetInt("nivelJugador", 3);
        PlayerPrefs.Save();
        SceneManager.LoadScene(4);
        Time.timeScale = 1f;
        PauseManager.InputsBloqueados = false;
    }

    public void Victoria()
    {
        SceneManager.LoadScene(5);
    }

    public void VolverAlMenuPrincipal()
    {
        SceneManager.LoadScene(1);
    }
}

