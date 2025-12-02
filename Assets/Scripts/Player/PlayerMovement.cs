using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidadMovimiento = 6f;
    [Header("Dash")]
    [SerializeField] private float fuerzaDash = 10f;
    [SerializeField] private float duracionDash = 0.2f;
    [SerializeField] private float tiempoRecargaDash = 1f;

    private bool puedeDash = true;
    public int nivel = 1;
    private bool isDashing = false;

    private Vector2 entradaMovimiento;

    public Rigidbody2D rb;

    private SpriteRenderer sprite;
    public int salto = 1;
    public bool mirandoDerecha = true;

    [Header("Salto")]
    [SerializeField] private float fuerzaSalto = 7f;
    [Header("Suelo")]
    [SerializeField] private Transform detectorSuelo;
    [SerializeField] private float distanciaSuelo = 0.1f;
    [SerializeField] private LayerMask capaSuelo;

    private bool enSuelo = true;
    public float y;


    //[Header("Sonidos")]
    //[SerializeField] private AudioSource sonidoSalto;
    //[SerializeField] private AudioSource sonidoAndar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nivel = PlayerPrefs.GetInt("nivelJugador", 1); // Cargar nivel de jugador guardado
        rb = GetComponent<Rigidbody2D>();

        if (!sprite)
        {
            sprite = GetComponentInChildren<SpriteRenderer>();
        }

    }
    public bool EnSuelo()
    {
        return enSuelo;
    }

    public void Parar()
    {
        GetComponent<PlayerInput>().enabled = false;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }


    public void OnJump(InputValue valor)
    {
        if (PauseManager.InputsBloqueados) return;

        // NO saltar si no está en el suelo
        if (!enSuelo)
            return;

        // Reseteo de velocidad vertical antes de saltar
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

        // Salto
        rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
    }

    public void OnMove(InputValue valor)
    {
        if (PauseManager.InputsBloqueados) return;

        entradaMovimiento = valor.Get<Vector2>();
    }
    // Update is called once per frame
    void Update()
    {
        // Comprobar suelo
        ComprobarSuelo();

        // Girar sprite
        if (entradaMovimiento.x > 0 && !mirandoDerecha)
            Girar(false);
        else if (entradaMovimiento.x < 0 && mirandoDerecha)
            Girar(true);

        y = rb.linearVelocity.y;
    }
    void FixedUpdate()
    {
        if (PauseManager.InputsBloqueados)
            return;
        if (isDashing)
            return;
        rb.linearVelocity = new Vector2(entradaMovimiento.x * velocidadMovimiento, rb.linearVelocity.y);
    }

    private void Girar(bool aIzquierda)
    {
        if (PauseManager.InputsBloqueados) return;

        mirandoDerecha = !mirandoDerecha;
        if (sprite) sprite.flipX = aIzquierda;
    }

    private void ComprobarSuelo()
    {
        enSuelo = Physics2D.Raycast(detectorSuelo.position, Vector2.down, distanciaSuelo, capaSuelo);
    }

    private IEnumerator DoDash()
    {
        puedeDash = false;
        isDashing = true;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0;

        // Dirección según hacia donde mire
        float direction = mirandoDerecha ? 1f : -1f;

        // Velocidad del dash
        rb.linearVelocity = new Vector2(direction * fuerzaDash, 0);

        // Animación
        GetComponent<PlayerAnimations>().AnimacionDash();

        yield return new WaitForSeconds(duracionDash);

        // Restaurar el movimiento
        rb.gravityScale = originalGravity;
        isDashing = false;

        yield return new WaitForSeconds(tiempoRecargaDash);

        puedeDash = true;
    }

    private void OnDash(InputValue value)
    {
        if (PauseManager.InputsBloqueados) return;
        int idEscena = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

        // SOLO permitir dash en nivel 2 y 3
        if (idEscena == 0)
            return;
        if (!puedeDash) return;
        if(!value.isPressed) return;
        StartCoroutine(DoDash());
    }

}
