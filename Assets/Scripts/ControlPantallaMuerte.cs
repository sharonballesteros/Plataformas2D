using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlPantallaMuerte : MonoBehaviour
{
    [Header("Referencia al panel visual de Game Over")]
    public GameObject contenedorVisual; // Tu objeto ContenedorGameOver

    private bool juegoPausadoPorMuerte = false;

    void Start()
    {
        // Al iniciar la escena, nos aseguramos de ocultar la pantalla de muerte
        if (contenedorVisual != null)
        {
            contenedorVisual.SetActive(false);
        }
    }

    void Update()
    {
        // Si la pantalla de muerte está activa y el jugador presiona la barra espaciadora...
        if (juegoPausadoPorMuerte && Input.GetKeyDown(KeyCode.Space))
        {
            ReiniciarNivel();
        }
    }

    // Método que llama ControlVidas cuando te quedas sin vidas
    public void MostrarPantallaMuerte()
    {
        if (contenedorVisual != null)
        {
            contenedorVisual.SetActive(true); // Muestra el panel y el texto del espacio
        }

        juegoPausadoPorMuerte = true;

        // Opcional: Si quieres pausar el tiempo del juego para que los enemigos se detengan
        Time.timeScale = 0f;
    }

    void ReiniciarNivel()
    {
        // MUY IMPORTANTE: Regresar el tiempo a la normalidad antes de recargar
        Time.timeScale = 1f;

        // Recarga la escena actual desde cero (vidas, cerezas y enemigos limpios)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}