using UnityEngine;

public class ControlEnemigo : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.name == "Player")
        {
            ControlVidas vidasJugador = collision.gameObject.GetComponent<ControlVidas>();
            if (vidasJugador != null)
            {
                vidasJugador.PerderVida();
                Debug.Log("¡El enemigo quitó una vida!");
            }
        }
    }
}