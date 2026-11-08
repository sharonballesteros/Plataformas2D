using UnityEngine;

public class ControlMeta : MonoBehaviour
{
    [Header("Configuración de Nivel")]
    public bool esUltimoNivel = false; // Desmarcado en niveles normales

    [Header("Interfaces (Niveles Normales)")]
    public GameObject contenedorFinNivel; // Arrastra aquí tu PantallaSiguienteNivel

    [Header("Pantalla de Game Over (Solo Último Nivel)")]
    public GameObject panelGameOverDirecto;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.gameObject.name == "Player")
        {
            Debug.Log("Fin de nivel alcanzado");

            // 1. Reproducir audio de victoria
            ControladorSonido controladorSonido = FindFirstObjectByType<ControladorSonido>();
            if (controladorSonido != null)
            {
                controladorSonido.ReproducirVictoria();
            }

            // 2. Ocultar al jugador
            collision.gameObject.SetActive(false);

            // 3. Evaluar si es el último nivel o uno normal
            if (esUltimoNivel)
            {
                if (panelGameOverDirecto != null)
                {
                    panelGameOverDirecto.SetActive(true);
                    foreach (Transform hijo in panelGameOverDirecto.transform)
                    {
                        hijo.gameObject.SetActive(true);
                    }

                    ControlGameOve controlGO = panelGameOverDirecto.GetComponent<ControlGameOve>();
                    if (controlGO != null)
                    {
                        controlGO.HabilitarLecturaEspacio();
                    }
                }
            }
            else
            {
                // Muestra el panel normal de siguiente nivel a la fuerza
                if (contenedorFinNivel != null)
                {
                    contenedorFinNivel.SetActive(true);

                    // Forzamos también a encender los hijos por si alguno está apagado
                    foreach (Transform hijo in contenedorFinNivel.transform)
                    {
                        hijo.gameObject.SetActive(true);
                    }

                    ControlSiguienteNivel scriptSiguiente = contenedorFinNivel.GetComponent<ControlSiguienteNivel>();
                    if (scriptSiguiente != null)
                    {
                        scriptSiguiente.MostrarPantallaSiguienteNivel();
                    }
                }
                else
                {
                    Debug.LogError("¡Falta asignar el Contenedor Fin Nivel en el Inspector de la bandera!");
                }
            }
        }
    }
}