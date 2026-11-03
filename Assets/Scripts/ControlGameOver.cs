using UnityEngine;

public class ControlGameOve : MonoBehaviour
{
    [Header("Interfaz de Game Over")]
    public GameObject panelGameOver; // <-- Esta variable aparecerá en el Inspector

    void Start()
    {
        // Al iniciar el juego, normalmente quieres que el panel esté oculto
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(false);
        }
    }

    public void MostrarGameOver()
    {
        // Activa todo el contenedor (texto, fondo, botones) cuando mueras
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);
        }
    }
}