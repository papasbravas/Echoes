using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
// Arreglar animacion de salto y correr
public class PlayerAnimations : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] private Animator animator;
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    [SerializeField] private GameObject hitboxMelee;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        if (hitboxMelee != null)
            hitboxMelee.SetActive(false);
    }

    public void AnimacionMuerte()
    {
        animator.SetTrigger("Muerte");
    }

    public void OnMove(InputValue value)
    {
        float movimientoHorizontal = value.Get<Vector2>().x;
        animator.SetFloat("x", Mathf.Abs(movimientoHorizontal));
        //animator.SetFloat("x", value.Get<Vector2>().x);
        //animator.SetFloat("x", Mathf.Abs(rb.linearVelocity.x));
    }

    public void AnimacionVida()
    {
        animator.SetTrigger("VidaNueva");
    }

    public void AnimacionDash()
    {
        animator.SetTrigger("Dash");
    }

    public void AnimacionDisparo()
    {
        animator.ResetTrigger("Shoot");
        animator.SetTrigger("Shoot");
    }
    public void OnShoot(InputValue value)
    {
        if (PauseManager.InputsBloqueados) return;

        if (value.isPressed)
        {
            AnimacionDisparo();
        }
    }
    public void AnimacionMelee()
    {
        animator.SetTrigger("Melee");
        if (hitboxMelee != null)
            StartCoroutine(ActivarHitbox());
    }

    public void AnimacionDaño()
    {
        animator.SetTrigger("Hurted");
    }
    
    public void OnMelee(InputValue value)
    {
        if (PauseManager.InputsBloqueados) return;

        if (value.isPressed)
        {
            AnimacionMelee();
        }
    }
    private IEnumerator ActivarHitbox()
    {
        hitboxMelee.SetActive(true);
        yield return new WaitForSeconds(0.1f); // duración del golpe activo
        hitboxMelee.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("y", rb.linearVelocity.y);

        animator.SetBool("enSuelo", playerMovement.EnSuelo());
    }
}
