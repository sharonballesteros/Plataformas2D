using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    [Header("Ataque a Distancia")]
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public float cadenciaAtaque = 0.5f;
    private float ultimoDisparo;

    [Header("Configuración de Movimiento")]
    public float velocidad = 8f;
    public float velocidadSalto = 18f;

    [Header("Detección de Suelo")]
    public LayerMask capaSuelo;
    private CapsuleCollider2D capsuleCollider;

    [Header("Referencias")]
    public SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private Animator animator;

   private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>(); // Toma el Animator del objeto principal (Player)

        // Busca únicamente el SpriteRenderer en el objeto hijo "imagen"
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

    private void Update()
    {
        float entradaHorizontal = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(entradaHorizontal * velocidad, rb.linearVelocity.y);

        animator.SetBool("estaCorriendo", entradaHorizontal != 0);

        bool enSuelo = EstaEnSuelo();

        if (Input.GetKeyDown(KeyCode.Space) && enSuelo)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, velocidadSalto);

            ControladorSonido controladorSonido = FindFirstObjectByType<ControladorSonido>();
            if (controladorSonido != null) controladorSonido.ReproducirSalto();
        }

        // Disparo con límite de cadencia
        if (Input.GetKeyDown(KeyCode.F) && Time.time > ultimoDisparo + cadenciaAtaque)
        {
            Disparar();
            ultimoDisparo = Time.time;
        }

        animator.SetBool("estaSaltando", !enSuelo);

        // Volteo del sprite
        if (entradaHorizontal != 0)
        {
            spriteRenderer.flipX = entradaHorizontal < 0;
        }
    }

   void Disparar()
    {
        // Solo activa la animación, la piedra aún no sale
        if (proyectilPrefab != null && puntoDisparo != null)
        {
            if (animator != null) animator.SetTrigger("disparar");
        }
    }

    // Esta función DEBE ser 'public' para que la animación pueda encontrarla
   public void LanzarPiedra()
    {
        if (proyectilPrefab != null && puntoDisparo != null)
        {
            float direccionX = spriteRenderer.flipX ? -Mathf.Abs(puntoDisparo.localPosition.x) : Mathf.Abs(puntoDisparo.localPosition.x);
            Vector3 posicionReal = transform.position + new Vector3(direccionX, puntoDisparo.localPosition.y, 0);
            Quaternion rotacion = spriteRenderer.flipX ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
            
            Instantiate(proyectilPrefab, posicionReal, rotacion);

            // --- NUEVO: Llamar al sonido ---
            ControladorSonido controladorSonido = FindFirstObjectByType<ControladorSonido>();
            if (controladorSonido != null)
            {
                controladorSonido.ReproducirDisparo();
            }
        }
    }

private bool EstaEnSuelo()
    {
        if (capsuleCollider == null) return false;

        Vector2 origen = (Vector2)capsuleCollider.bounds.center;
        Vector2 tamaño = capsuleCollider.bounds.size;
        
        // Lanza una caja invisible hacia abajo para detectar colisiones con la capa "Suelo"
        RaycastHit2D hit = Physics2D.BoxCast(origen, tamaño, 0f, Vector2.down, 0.1f, capaSuelo);

        return hit.collider != null;
    }
}