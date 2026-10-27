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
    public int saltosMaximos = 2; 
    private int saltosRealizados; 

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
        animator = GetComponent<Animator>();

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

        if (enSuelo && rb.linearVelocity.y <= 0.1f)
        {
            saltosRealizados = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && saltosRealizados < saltosMaximos)
        {
            // El primer salto usa el 100% de fuerza, los siguientes usan el 50%
            float fuerzaSalto = (saltosRealizados == 0) ? velocidadSalto : velocidadSalto * 0.5f;
            
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, fuerzaSalto);
            saltosRealizados++;

            ControladorSonido controladorSonido = FindFirstObjectByType<ControladorSonido>();
            if (controladorSonido != null) controladorSonido.ReproducirSalto();

            if (saltosRealizados > 1)
            {
                animator.SetTrigger("activarDobleSalto");
            }
        }

        if (Input.GetKeyDown(KeyCode.F) && Time.time > ultimoDisparo + cadenciaAtaque)
        {
            Disparar();
            ultimoDisparo = Time.time;
        }

        animator.SetBool("estaSaltando", !enSuelo);

        if (entradaHorizontal != 0)
        {
            spriteRenderer.flipX = entradaHorizontal < 0;
        }
    }

   void Disparar()
    {
        if (proyectilPrefab != null && puntoDisparo != null)
        {
            if (animator != null) animator.SetTrigger("disparar");
        }
    }

   public void LanzarPiedra()
    {
        if (proyectilPrefab != null && puntoDisparo != null)
        {
            float direccionX = spriteRenderer.flipX ? -Mathf.Abs(puntoDisparo.localPosition.x) : Mathf.Abs(puntoDisparo.localPosition.x);
            Vector3 posicionReal = transform.position + new Vector3(direccionX, puntoDisparo.localPosition.y, 0);
            Quaternion rotacion = spriteRenderer.flipX ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
            
            Instantiate(proyectilPrefab, posicionReal, rotacion);

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
        // Reducimos el ancho de la caja al 60% para que no roce las paredes laterales
        Vector2 tamaño = new Vector2(capsuleCollider.bounds.size.x * 0.6f, capsuleCollider.bounds.size.y);
        
        RaycastHit2D hit = Physics2D.BoxCast(origen, tamaño, 0f, Vector2.down, 0.1f, capaSuelo);

        return hit.collider != null;
    }
}