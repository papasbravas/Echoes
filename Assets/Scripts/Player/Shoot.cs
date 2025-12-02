using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [Header("Disparo")]
    [SerializeField] private GameObject firePrefab;
    [SerializeField] private Transform firePointRight;
    [SerializeField] private Transform firePointLeft;

    private PlayerMovement playerMove;
    private PlayerAnimations playerAnim;
    // Arreglar cantidad de disparos
    [SerializeField] public float fireRate = 0.5f;
    private float nextFire = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerMove = GetComponent<PlayerMovement>();
        playerAnim = GetComponent<PlayerAnimations>();
    }

    public void OnShoot(InputValue valor)
    {
        if (!valor.isPressed)
        {
            return;
        }
        if (firePrefab == null || firePointRight == null || firePointLeft == null)
        {
            return;
        }

        if (playerMove.EnSuelo())
        {
            if(Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                StartCoroutine("Disparo");
            }

        }
        else
        {
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                StartCoroutine("Disparo");
            }
        }
    }

    private IEnumerator Disparo()
    {
        playerAnim.AnimacionDisparo();
        yield return new WaitForSecondsRealtime(0.5f);
        if (playerMove.mirandoDerecha)
        {
            Instantiate(firePrefab, firePointRight.position, firePointRight.rotation);
        }
        else
        {
            Instantiate(firePrefab, firePointLeft.position, firePointLeft.rotation);
        }
    }
}
