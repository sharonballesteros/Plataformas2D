using UnityEngine;

public class ControlSiguienteNivel : MonoBehaviour
{
    private GameObject player;
    private bool nivelCompletado = false;

    [Header("Referencia al contenedor interno")]
    public GameObject contenedorVisual; // Arrastra aquí "ContenedorSiguienteNivel"

    void Start()
    {
        // 1. Busca al jugador en la escena automáticamente
        GameObject encontrado = GameObject.FindWithTag("Player");
        if (encontrado != null)
        {
            player = encontrado;
        }

        // 2. Oculta la pantalla y el contenedor al iniciar
        gameObject.SetActive(false);
        if (contenedorVisual != null)
        {
            contenedorVisual.SetActive(false);
        }
    }

    void Update()
    {
        // 3. Si la pantalla está activa y presionas la barra espaciadora (Jump)
        if (nivelCompletado && Input.GetButtonDown("Jump"))
        {
            ControladorJuego controlador = FindFirstObjectByType<ControladorJuego>();
            if (controlador != null)
            {
                controlador.SiguienteNivel();
            }
            else
            {
                int escenaActual = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
                UnityEngine.SceneManagement.SceneManager.LoadScene(escenaActual + 1);
            }
        }
    }

    // Este método se activa desde la Bandera/Meta cuando el jugador gana
    public void MostrarPantallaSiguienteNivel()
    {
        nivelCompletado = true;

        if (player != null)
        {
            player.SetActive(false); // Oculta al jugador
        }

        // Activa tanto el objeto principal como el contenedor visual
        gameObject.SetActive(true);
        if (contenedorVisual != null)
        {
            contenedorVisual.SetActive(true);
        }
    }
}