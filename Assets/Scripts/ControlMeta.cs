using UnityEngine;

public class ControlMeta : MonoBehaviour
{
    [Header("Interfaz de Fin de Nivel")]
    public GameObject contenedorFinNivel; // Arrastra aquí PantallaSiguienteNivel

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.gameObject.name == "Player")
        {
            Debug.Log("fin de nivel");

            // --- ¡AQUÍ REPRODUCIMOS EL AUDIO DE VICTORIA! ---
            ControladorSonido controladorSonido = FindFirstObjectByType<ControladorSonido>();
            if (controladorSonido != null)
            {
                controladorSonido.ReproducirVictoria();
            }

            // 1. Oculta al jugador
            collision.gameObject.SetActive(false);

            // 2. Muestra el panel de fin de nivel
            if (contenedorFinNivel != null)
            {
                contenedorFinNivel.SetActive(true);

                // Activa la lógica de control del siguiente nivel
                ControlSiguienteNivel scriptSiguiente = contenedorFinNivel.GetComponent<ControlSiguienteNivel>();
                if (scriptSiguiente != null)
                {
                    scriptSiguiente.MostrarPantallaSiguienteNivel();
                }
            }
        }
    }
}