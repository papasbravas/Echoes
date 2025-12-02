using UnityEngine;

public class ItemTP : MonoBehaviour
{
    public Vector2 destinoTP;         // Punto al que se teletransporta
    public string tagJugador = "Player";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagJugador))
        {
            // Teletransportar jugador
            other.transform.position = destinoTP;

            // Destruir el objeto recogido
            Destroy(gameObject);
        }
    }
}
