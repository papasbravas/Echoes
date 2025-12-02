using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class VidasPlayer : MonoBehaviour
{
    [Header("Imagenes")]
    [SerializeField] private List<Image> hearts = new();

    [Header("Sprites")]
    [SerializeField] private Sprite heartFull;
    [SerializeField] private Sprite heartEmpty;

    [Header("Config")]
    [SerializeField] private int maxHealth = 3;
    public int currentHealth;

    private void Awake()
    {
        SetHealth(maxHealth);
    }

    public void SetHealth(int health)
    {
        currentHealth = Mathf.Clamp(health, 0, maxHealth);

        for (int i = 0; i < hearts.Count; i++)
        {
            // Si está fuera del máximo, se oculta
            if (i >= maxHealth)
            {
                hearts[i].gameObject.SetActive(false);
                continue;
            }

            // Corazón lleno o vacío según la vida
            hearts[i].sprite = (i < currentHealth) ? heartFull : heartEmpty;
        }
    }

    public void LoseLife(int amount = 1)
    {
        currentHealth -= amount;
        SetHealth(currentHealth);
    }

    public void EarnLife(int amount = 1)
    {
        currentHealth += amount;
        SetHealth(currentHealth);
    }
}
