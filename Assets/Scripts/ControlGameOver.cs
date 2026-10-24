using UnityEngine;

public class ControlGameOver : MonoBehaviour
{
    private GameObject player;
    private bool gameOverActivo = false;

    void Start()
    {
        // Busca al jugador automáticamente en la escena
        GameObject encontrado = GameObject.FindWithTag("Player");
        if (encontrado != null)
        {
            player = encontrado;
        }

        // Asegura que la pantalla empiece oculta
        gameObject.SetActive(false);
    }

    void Update()
    {
        // Si el Game Over está activo y presionas la barra espaciadora (Jump)
        if (gameOverActivo && Input.GetButtonDown("Jump"))
        {
            // Busca el ControladorJuego moderno y ejecuta la recarga
            ControladorJuego controlador = FindFirstObjectByType<ControladorJuego>();
            if (controlador != null)
            {
                controlador.RecargarEscena();
            }
            else
            {
                // Alternativa directa si no encuentra el script
                UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
            }
        }
    }

    // Método que activa el Game Over (llámalo cuando las vidas lleguen a 0)
    public void MuerteJugador()
    {
        gameOverActivo = true;

        if (player != null)
        {
            player.SetActive(false); // Oculta al jugador
        }

        gameObject.SetActive(true); // Muestra la pantalla de Game Over
    }
}