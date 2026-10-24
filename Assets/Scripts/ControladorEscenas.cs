using UnityEngine;
using UnityEngine.SceneManagement;



public class ControladorEscenas : MonoBehaviour

{

    // Método equivalente al evento "RecargarEscena" de tus nodos[span_9](start_span)[span_9](end_span)

    public void RecargarEscena()

    {

        Debug.Log("Recargando escena...");


        // Puedes usar el nombre exacto de tu escena actual (ej. "SampleScene")[span_10](start_span)[span_10](end_span)

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


        // O si prefieres por índice:

        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }

}