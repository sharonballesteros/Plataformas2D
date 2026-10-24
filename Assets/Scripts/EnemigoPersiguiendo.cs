using UnityEngine;

public class EnemigoPersiguiendo : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float velocidad = 3.5f;
    public float distanciaDeteccion = 8f; // Rango para empezar a perseguir

    private Transform player;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    void Start()
    {
        // Busca al jugador automáticamente por la etiqueta "Player"
        GameObject objetoJugador = GameObject.FindWithTag("Player");
        if (objetoJugador != null)
        {
            player = objetoJugador.transform;
        }

        // Busca los componentes en el objeto hijo (como 'eneee')
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player == null) return;

        float distanciaAlPlayer = Vector2.Distance(transform.position, player.position);

        // Si el jugador entra en el rango de detección, lo persigue
        if (distanciaAlPlayer <= distanciaDeteccion)
        {
            // Calcula la dirección hacia el jugador
            float direccionX = player.position.x > transform.position.x ? 1f : -1f;

            if (rb != null)
            {
                rb.linearVelocity = new Vector2(direccionX * velocidad, rb.linearVelocity.y);
            }

            // Activa la animación de correr/caminar
            if (animator != null)
            {
                animator.SetBool("estaCaminando", true);
            }

            // Voltea el sprite usando el FlipX según la posición del jugador
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = player.position.x < transform.position.x;
            }
        }
        else
        {
            // Si el jugador está lejos, se detiene
            if (rb != null)
            {
                rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            }

            if (animator != null)
            {
                animator.SetBool("estaCaminando", false);
            }
        }
    }

    // Si el enemigo toca al jugador por contacto directo, le quita vida
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