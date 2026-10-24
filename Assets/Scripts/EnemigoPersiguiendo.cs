using UnityEngine;

public class EnemigoPatrulla : MonoBehaviour
{
    [Header("Configuración de Patrulla")]
    public float velocidad = 2f;
    public float distanciaPatrulla = 3f; // Qué tan lejos se mueve a la derecha y izquierda desde su punto inicial

    private Vector2 posicionInicial;
    private int direccion = 1; // 1 para derecha, -1 para izquierda
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;

    void Start()
    {
        posicionInicial = transform.position;
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Mueve al enemigo en la dirección actual
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(direccion * velocidad, rb.linearVelocity.y);
        }

        // Activa la animación de caminar
        if (animator != null)
        {
            animator.SetBool("estaCaminando", true);
        }

        // Revisa si se pasó del límite derecho o izquierdo para cambiar de dirección
        if (transform.position.x > posicionInicial.x + distanciaPatrulla)
        {
            direccion = -1;
            if (spriteRenderer != null) spriteRenderer.flipX = true;
        }
        else if (transform.position.x < posicionInicial.x - distanciaPatrulla)
        {
            direccion = 1;
            if (spriteRenderer != null) spriteRenderer.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ControlVidas vidasJugador = collision.gameObject.GetComponent<ControlVidas>();
            if (vidasJugador != null)
            {
                vidasJugador.PerderVida();
            }
        }
    }
}