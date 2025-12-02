using System.Collections;
using TMPro;
using UnityEngine;

public class Datos : MonoBehaviour
{
    [Header("UI")] public Canvas canvas;

    public static Datos Instance;

    public int vidas;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

   

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
