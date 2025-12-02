using UnityEngine;

public class LevelEnemyManager : MonoBehaviour
{
    [Header("Orbe de recompensa")]
    [SerializeField] private GameObject orbeRecompensa;

    private int enemigosRestantes;

    void Awake()
    {
        // Aseguramos que esté desactivado desde el inicio
        if (orbeRecompensa != null)
            orbeRecompensa.SetActive(false);
    }

    void Start()
    {
        // Detectar automáticamente todos los enemigos que tienen el script EnemiesAnimation
        var enemigos = FindObjectsOfType<EnemiesAnimation>();
        enemigosRestantes = enemigos.Length;

        Debug.Log($"[LevelEnemyManager] Enemigos detectados: {enemigosRestantes}");

        // Si no hay enemigos, activar orbe inmediatamente (opcional según tu diseño)
        if (enemigosRestantes <= 0)
        {
            ActivarOrbe();
        }
    }

    // Llamado por cada enemigo al morir
    public void EnemigoEliminado()
    {
        enemigosRestantes--;
        Debug.Log($"[LevelEnemyManager] Enemigo eliminado. Restan: {enemigosRestantes}");

        if (enemigosRestantes <= 0)
            ActivarOrbe();
    }

    private void ActivarOrbe()
    {
        if (orbeRecompensa == null) return;

        if (!orbeRecompensa.activeSelf)
        {
            orbeRecompensa.SetActive(true);
            Debug.Log("[LevelEnemyManager] ¡Orbe activado!");
        }
        else
        {
            Debug.Log("[LevelEnemyManager] Orbe ya estaba activo.");
        }
    }
}
