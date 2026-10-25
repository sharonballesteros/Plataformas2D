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
            // Guardamos la distancia inicial desde el centro para usarla como offset seguro
            offsetDisparoOriginalX = Mathf.Abs(puntoDisparo.localPosition.x);
            if (offsetDisparoOriginalX == 0f) offsetDisparoOriginalX = 0.5f; // Valor por defecto si estaba en 0
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
            // Detener movimiento al atacar
            if (rb != null) rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

            if (animatorHijo != null)
            {
                animatorHijo.SetBool("estaCaminando", false);
            }

            // Gira hacia el jugador (corregido con '!')
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
            // Movimiento de patrulla
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

            // Voltear el sprite según la dirección de patrulla
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
            // Determina la dirección según el estado de flipX del sprite
            float dirProj = (spriteRendererHijo != null && spriteRendererHijo.flipX) ? -1f : 1f;

            // Posiciona el punto de disparo de forma segura al frente sin romper el transform
            float posXSegura = offsetDisparoOriginalX * dirProj;
            puntoDisparo.localPosition = new Vector3(posXSegura, puntoDisparo.localPosition.y, puntoDisparo.localPosition.z);

            // Instancia la bolita
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

    // --- NUEVO: MÉTODOS PARA MORIR AL RECIBIR EL DISPARO DEL PLAYER ---

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si choca con el proyectil del jugador (asegúrate de que el proyectil tenga el Tag "ProyectilJugador")
        if (collision.gameObject.CompareTag("ProyectilJugador"))
        {
            Destroy(collision.gameObject); // Destruye el proyectil del jugador
            Destroy(gameObject);           // Destruye al enemigo azul completo (Ene3)
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Por si el proyectil del jugador usa IsTrigger
        if (collision.CompareTag("ProyectilJugador"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}