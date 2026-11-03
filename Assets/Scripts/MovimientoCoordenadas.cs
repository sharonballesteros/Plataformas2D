using UnityEngine;

public class MovimientoCoordenadas : MonoBehaviour
{
    public float velocidad = 2f;
    public float minX;
    public float maxX;

    private int direccion = -1; // -1 izquierda, 1 derecha
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        // Mover usando la física (Rigidbody2D)
        rb.linearVelocity = new Vector2(direccion * velocidad, rb.linearVelocity.y);

        // Si toca el límite izquierdo y va hacia la izquierda, cambia a la derecha
        if (transform.position.x <= minX && direccion == -1)
        {
            direccion = 1;
        }
        // Si toca el límite derecho y va hacia la derecha, cambia a la izquierda
        else if (transform.position.x >= maxX && direccion == 1)
        {
            direccion = -1;
        }

        // Voltear el sprite correctamente
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = (direccion > 0);
        }
    }
}