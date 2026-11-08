using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    // Detecta cuando el jugador entra al área del pozo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto que colisionó tiene la etiqueta "Player"
        if (collision.CompareTag("Player"))
        {
            // Opción A: Reiniciar la escena actual (Respawn rápido)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            // Opción B (Alternativa): Destruir al jugador si no usas recarga de escena
            // Destroy(collision.gameObject);
        }
    }
}