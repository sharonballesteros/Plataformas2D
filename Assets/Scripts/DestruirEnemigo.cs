using UnityEngine;

public class DestruirEnemigo : MonoBehaviour
{
    public GameObject explosionPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.name.Contains("Player") || collision.gameObject.layer == LayerMask.NameToLayer("mataEnemigo"))
        {
            Debug.Log("¡Enemigo derrotado y explotando!");

            // 1. Buscamos la posición visual exacta del sprite de la rana en movimiento
            Vector3 posicionReal = transform.parent.position;
            SpriteRenderer spriteVisual = transform.parent.GetComponentInChildren<SpriteRenderer>();

            if (spriteVisual != null)
            {
                posicionReal = spriteVisual.transform.position;
            }

            // 2. Impulso de rebote para el jugador
            Rigidbody2D rbJugador = collision.GetComponent<Rigidbody2D>();
            if (rbJugador != null)
            {
                rbJugador.linearVelocity = new Vector2(rbJugador.linearVelocity.x, 7f);
            }

            // 3. Creamos la explosión y aseguramos que se vea hasta adelante (Order in Layer alto)
            if (explosionPrefab != null)
            {
                GameObject explosion = Instantiate(explosionPrefab, posicionReal, Quaternion.identity);

                // Forzamos el orden visual para que ningún fondo la tape
                SpriteRenderer srExplosion = explosion.GetComponent<SpriteRenderer>();
                if (srExplosion != null)
                {
                    srExplosion.sortingOrder = 50;
                }
            }

            // 4. Desactiva el script de control del padre
            ControlEnemigo controlPadre = transform.parent.GetComponent<ControlEnemigo>();
            if (controlPadre != null)
            {
                controlPadre.enabled = false;
            }

            // 5. Desactiva todos los colisionadores del enemigo
            Collider2D[] cols = transform.parent.GetComponentsInChildren<Collider2D>();
            foreach (Collider2D c in cols)
            {
                c.enabled = false;
            }

            // 6. Destruye al enemigo completo
            Destroy(transform.parent.gameObject, 0.05f);
        }
    }
}
