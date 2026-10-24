using UnityEngine;

public class ProyectilJugador : MonoBehaviour
{
    public float velocidad = 12f;
    private Rigidbody2D rb;
    public int direccion = 1; // 1 para derecha, -1 para izquierda

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Se destruye automáticamente a los 3 segundos si no choca con nada
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(velocidad * direccion, rb.linearVelocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si choca con un enemigo que tenga un script de destrucción o vida
        if (collision.gameObject.layer == LayerMask.NameToLayer("enemigo") || collision.CompareTag("Enemigo"))
        {
            // Destruye al enemigo o le quita vida
            Destroy(collision.transform.root.gameObject); // Destruye al padre del enemigo
            Destroy(gameObject); // Destruye el proyectil
        }
    }
}