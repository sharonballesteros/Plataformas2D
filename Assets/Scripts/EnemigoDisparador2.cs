using UnityEngine;

public class EnemigoDisparador2 : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float velocidadPatrulla = 2f;
    public float tiempoPatrulla = 2.5f;
    public float distanciaVision = 25f;

    [Header("Configuración de Disparo")]
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public float tiempoEntreDisparos = 2.5f;

    private Transform player;
    private int direccion = 1;
    private float temporizadorPatrulla;
    private SpriteRenderer spriteRendererHijo;
    private Animator animatorHijo;
    private Rigidbody2D rb;
    private float temporizadorDisparo;
    private float offsetDisparoOriginalX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.freezeRotation = true;

        spriteRendererHijo = GetComponentInChildren<SpriteRenderer>();
        animatorHijo = GetComponentInChildren<Animator>();

        if (puntoDisparo != null)
        {
            offsetDisparoOriginalX = Mathf.Abs(puntoDisparo.localPosition.x);
            if (offsetDisparoOriginalX == 0f) offsetDisparoOriginalX = 0.5f;
        }

        temporizadorPatrulla = tiempoPatrulla;
    }

    void Update()
    {
        if (player == null)
        {
            GameObject obj = GameObject.FindWithTag("Player");
            if (obj != null) player = obj.transform;
            else return;
        }

        float distancia = Vector2.Distance(transform.position, player.position);

        if (temporizadorDisparo > 0)
        {
            temporizadorDisparo -= Time.deltaTime;
        }

        if (distancia <= distanciaVision)
        {
            if (rb != null) rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

            if (animatorHijo != null)
            {
                animatorHijo.SetBool("estaCaminando", false);
            }

            bool mirarIzquierda = player.position.x < transform.position.x;
            if (spriteRendererHijo != null)
            {
                spriteRendererHijo.flipX = !mirarIzquierda;
            }

            if (temporizadorDisparo <= 0f)
            {
                if (animatorHijo != null)
                {
                    animatorHijo.SetTrigger("atacar");
                }
                temporizadorDisparo = tiempoEntreDisparos;
            }
        }
        else
        {
            if (rb != null)
            {
                rb.linearVelocity = new Vector2(direccion * velocidadPatrulla, rb.linearVelocity.y);
            }

            if (animatorHijo != null)
            {
                animatorHijo.SetBool("estaCaminando", true);
            }

            temporizadorPatrulla -= Time.deltaTime;
            if (temporizadorPatrulla <= 0f)
            {
                direccion *= -1;
                temporizadorPatrulla = tiempoPatrulla;
            }

            if (spriteRendererHijo != null)
            {
                spriteRendererHijo.flipX = (direccion < 0);
            }
        }
    }

    public void DispararBolita()
    {
        if (proyectilPrefab != null && puntoDisparo != null)
        {
            float dirProj = (spriteRendererHijo != null && spriteRendererHijo.flipX) ? -1f : 1f;

            float posXSegura = offsetDisparoOriginalX * dirProj;
            puntoDisparo.localPosition = new Vector3(posXSegura, puntoDisparo.localPosition.y, puntoDisparo.localPosition.z);

            GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, Quaternion.identity);

            SpriteRenderer srProj = proyectil.GetComponent<SpriteRenderer>();
            if (srProj != null)
            {
                srProj.sortingOrder = 50;
            }

            ProyectilEnemigo2 scriptProj = proyectil.GetComponent<ProyectilEnemigo2>();
            if (scriptProj != null)
            {
                scriptProj.AsignarDireccion(dirProj);
            }
        }
    }

    // --- MUERTE AL RECIBIR EL DISPARO DEL JUGADOR (Detecta por nombre o componente de bala) ---

    // --- MUERTE AL RECIBIR EL DISPARO DEL JUGADOR USANDO TU TAG ---

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si el objeto que choca es el proyectil del jugador
        if (collision.gameObject.CompareTag("ProyectilJugador"))
        {
            Destroy(collision.gameObject); // Destruye la bala
            Destroy(gameObject);           // Destruye al enemigo azul (Ene3)
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Por si el proyectil usa IsTrigger
        if (collision.gameObject.CompareTag("ProyectilJugador"))
        {
            Destroy(collision.gameObject); // Destruye la bala
            Destroy(gameObject);           // Destruye al enemigo azul (Ene3)
        }
    }
}