using UnityEngine;

public class EnemigoPersiguiendo : MonoBehaviour
{
    [Header("Configuración de Movimiento y Patrulla")]
    public float velocidadPatrulla = 2f;
    public float distanciaPatrulla = 3f;

    [Header("Configuración de Ataque")]
    public float distanciaAtaque = 3.5f;

    private Transform player;
    private Vector2 posicionInicial;
    private int direccion = 1;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rbPadre;

    void Start()
    {
        posicionInicial = transform.position;

        GameObject objetoJugador = GameObject.FindWithTag("Player");
        if (objetoJugador != null)
        {
            player = objetoJugador.transform;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rbPadre = GetComponentInParent<Rigidbody2D>();
    }

    void Update()
    {
        if (player == null) return;

        float distanciaAlPlayer = Vector2.Distance(transform.position, player.position);

        // Si el jugador entra al rango, se detiene y ataca
        if (distanciaAlPlayer <= distanciaAtaque)
        {
            if (rbPadre != null)
            {
                rbPadre.linearVelocity = new Vector2(0f, rbPadre.linearVelocity.y);
            }

            if (animator != null)
            {
                animator.SetBool("estaCaminando", false);
                animator.SetTrigger("atacar");
            }

            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = player.position.x < transform.position.x;
            }

            // Si está lo suficientemente CERCA, le quita vida de inmediato
            if (distanciaAlPlayer <= 1.5f)
            {
                ControlVidas vidasJugador = player.GetComponent<ControlVidas>();
                if (vidasJugador != null)
                {
                    vidasJugador.PerderVida();
                }
            }
        }
        else
        {
            // Vuelve a patrullar si el jugador se aleja
            if (rbPadre != null)
            {
                rbPadre.linearVelocity = new Vector2(direccion * velocidadPatrulla, rbPadre.linearVelocity.y);
            }

            if (animator != null)
            {
                animator.SetBool("estaCaminando", true);
            }

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

    public void DispararBolita()
    {
        Debug.Log("¡Bolita disparada!");
    }
}