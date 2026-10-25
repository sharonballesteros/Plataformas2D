using UnityEngine;

public class ControlEnemigo : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.name.Contains("Player"))
        {
            ControlVidas vidasJugador = collision.gameObject.GetComponent<ControlVidas>();
            if (vidasJugador != null)
            {
                vidasJugador.PerderVida();
                
                // Empuja al jugador hacia atras para que no se quede pegado al enemigo
                Rigidbody2D rbJugador = collision.gameObject.GetComponent<Rigidbody2D>();
                if (rbJugador != null)
                {
                    Vector2 direccionRebote = (collision.transform.position - transform.position).normalized;
                    rbJugador.linearVelocity = new Vector2(direccionRebote.x * 6f, 5f);
                }
            }
        }
    }
}