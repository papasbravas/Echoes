using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Entra");
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        collision.transform.SetParent(null);
    }
}
