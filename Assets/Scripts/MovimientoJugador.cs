using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float velocidad = 8f;
    public float velocidadSalto = 15f;

    [Header("Detección de Suelo")]
    public LayerMask capaSuelo;
    private CapsuleCollider2D capsuleCollider;

    [Header("Referencias")]
    public SpriteRenderer spriteRenderer; // Arrastra aquí el SpriteRenderer de tu hijo "imagen"

    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();

        // Si no asignaste el SpriteRenderer en el inspector, lo busca automáticamente en un hijo llamado "imagen"
        if (spriteRenderer == null)
        {
            Transform hijoImagen = transform.Find("imagen");
            if (hijoImagen != null)
            {
                spriteRenderer = hijoImagen.GetComponent<SpriteRenderer>();
            }
            else
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
        }
    }

    private void Update()
    {
        // 1. Movimiento horizontal
        float entradaHorizontal = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(entradaHorizontal * velocidad, rb.linearVelocity.y);

        // 2. Animación de correr
        bool estaCorriendo = entradaHorizontal != 0;
        animator.SetBool("estaCorriendo", estaCorriendo);

        // 3. Verificamos si está en el suelo
        bool enSuelo = EstaEnSuelo();

        // 4. Salto con barra espaciadora
        if (Input.GetKeyDown(KeyCode.Space) && enSuelo)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, velocidadSalto);
        }

        // 5. Animación de salto
        animator.SetBool("estaSaltando", !enSuelo);

        // 6. Flip horizontal del personaje (equivalente a tu último gráfico)
        if (entradaHorizontal == -1f)
        {
            spriteRenderer.flipX = true; // Mira hacia la izquierda
        }
        else if (entradaHorizontal == 1f)
        {
            spriteRenderer.flipX = false; // Mira hacia la derecha
        }
    }

    private bool EstaEnSuelo()
    {
        if (capsuleCollider == null) return false;

        Vector2 origen = (Vector2)capsuleCollider.bounds.center;
        Vector2 tamaño = capsuleCollider.bounds.size;

        RaycastHit2D hit = Physics2D.BoxCast(origen, tamaño, 0f, Vector2.down, 0.1f, capaSuelo);

        return hit.collider != null;
    }
}