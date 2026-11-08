using UnityEngine;

public class RecogerPocion : MonoBehaviour
{
    [Header("Duración de la invulnerabilidad")]
    public float duracionInvulnerabilidad = 5.0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto que colisiona es el jugador
        if (collision.CompareTag("Player") || collision.gameObject.name == "Player")
        {
            // Busca el componente ControlVidas en el jugador
            ControlVidas controlVidas = collision.GetComponent<ControlVidas>();
            if (controlVidas != null)
            {
                // Inicia la corrutina de invulnerabilidad en el script de vidas
                controlVidas.StartCoroutine(controlVidas.ActivarInvulnerabilidadPicos(duracionInvulnerabilidad));
                Debug.Log("¡Poción recogida! Invulnerable a los picos por " + duracionInvulnerabilidad + " segundos.");
            }

            // --- OPCIONAL: Reproducir sonido de poción si tienes un controlador de sonido ---
            ControladorSonido controladorSonido = FindFirstObjectByType<ControladorSonido>();
            if (controladorSonido != null)
            {
                // controladorSonido.ReproducirPocion(); // Descomenta si creas este método en tu sonido
            }

            // Destruye la poción de la escena
            Destroy(gameObject);
        }
    }
}