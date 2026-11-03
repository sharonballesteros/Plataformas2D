using UnityEngine;

public class PatrullaPlataforma : MonoBehaviour
{
    public float velocidad = 2f;
    public float minX;
    public float maxX;
    private int direccion = -1; // -1 izquierda, 1 derecha
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        // Mover al enemigo de forma directa y fluida
        transform.Translate(Vector2.right * direccion * velocidad * Time.deltaTime);

        // Cambiar de dirección al llegar al límite izquierdo
        if (transform.position.x <= minX)
        {
            direccion = 1; // Cambiar a la derecha
        }
        // Cambiar de dirección al llegar al límite derecho
        else if (transform.position.x >= maxX)
        {
            direccion = -1; // Cambiar a la izquierda
        }

        // Voltear el sprite según la dirección
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = (direccion < 0);
        }
    }
}