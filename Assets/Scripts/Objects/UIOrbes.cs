using UnityEngine;
using TMPro;

public class UIOrbes : MonoBehaviour
{
    public TextMeshProUGUI texto;
    private ContadorOrbs contador;

    void Start()
    {
        contador = FindObjectOfType<ContadorOrbs>(); // Encuentra el componente ContadorOrbs en la escena
        ActualizarUI();
    }

    public void ActualizarUI()
    {
        int recogidos = contador.totalOrbes - contador.orbsRestantes; // Cálculo de orbes recogidos
        texto.text = recogidos + " / " + contador.totalOrbes; // Actualiza el texto con el formato "recogidos / total"
    }
}
