using UnityEngine;
using TMPro; // Necesario para usar TextMeshPro

public class RecogerCereza : MonoBehaviour
{
    // Variable estática para llevar el conteo compartido de cerezas
    public static int cerezas = 0;

    // Referencia opcional al texto de la UI (puedes arrastrar el objeto de texto aquí en el Inspector)
    public static TextMeshProUGUI textoUI;

    private void Start()
    {
        // Si el texto de la UI no está asignado, intenta buscarlo automáticamente por su nombre en la escena
        if (textoUI == null)
        {
            GameObject objetoTexto = GameObject.Find("txtCerezas"); // Reemplaza con el nombre exacto de tu objeto de texto si es diferente
            if (objetoTexto != null)
            {
                textoUI = objetoTexto.GetComponent<TextMeshProUGUI>();
            }
        }
        ActualizarUI();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto que colisiona es el jugador
        if (collision.CompareTag("Player"))
        {
            // 1. Suma 1 a las cerezas
            cerezas++;

            // 2. Actualiza el texto en la pantalla
            ActualizarUI();

            // --- NUEVO: Reproducir sonido de cereza ---
            ControladorSonido controladorSonido = FindFirstObjectByType<ControladorSonido>();
            if (controladorSonido != null)
            {
                controladorSonido.ReproducirCereza();
            }

            // 3. Destruye la cereza
            Destroy(gameObject);
        }
    }

    private void ActualizarUI()
    {
        if (textoUI != null)
        {
            textoUI.text = cerezas.ToString();
        }
    }
}