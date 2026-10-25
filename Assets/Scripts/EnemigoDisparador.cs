using UnityEngine;

public class EnemigoDisparador : MonoBehaviour
{
    [Header("Configuracion de Movimiento")]
    public float velocidadPatrulla = 2f;
    public float distanciaPatrulla = 3f;
    public float distanciaVision = 5f;

    [Header("Configuracion de Disparo")]
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public float tiempoEntreDisparos = 2f;

    private Transform player;
    private Vector2 posicionInicial;
    private int direccion = 1;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;
    private float temporizadorDisparo;

    void Start()
    {
        posicionInicial = transform.position;
        GameObject obj = GameObject.FindWithTag("Player");
        if (obj != null) player = obj.transform;

        // Busca los componentes visuales en el objeto hijo automáticamente
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (player == null) return;

        float distancia = Vector2.Distance(transform.position, player.position);
        if (temporizadorDisparo > 0) temporizadorDisparo -= Time.deltaTime;

        if (distancia <= distanciaVision)
        {
            if (rb != null) rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
            if (animator != null) animator.SetBool("estaCaminando", false);
            if (spriteRenderer != null) spriteRenderer.flipX = player.position.x < transform.position.x;

            // Dispara si el temporizador llego a cero
            if (temporizadorDisparo <= 0f)
            {
                if (animator != null) animator.SetTrigger("atacar");
                temporizadorDisparo = tiempoEntreDisparos;
            }
        }
        else
        {
            if (rb != null) rb.linearVelocity = new Vector2(direccion * velocidadPatrulla, rb.linearVelocity.y);
            if (animator != null) animator.SetBool("estaCaminando", true);

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

    // Funcion llamada desde el Animation Event del disparo
    public void DispararBolita()
    {
        if (proyectilPrefab != null && puntoDisparo != null)
        {
            float direccionX = spriteRenderer.flipX ? -Mathf.Abs(puntoDisparo.localPosition.x) : Mathf.Abs(puntoDisparo.localPosition.x);
            Vector3 posicionReal = transform.position + new Vector3(direccionX, puntoDisparo.localPosition.y, 0);
            Quaternion rotacion = spriteRenderer.flipX ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
            
            Instantiate(proyectilPrefab, posicionReal, rotacion);
        }
    }
}