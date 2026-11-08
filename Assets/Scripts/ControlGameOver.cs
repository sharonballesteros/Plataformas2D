using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlGameOve : MonoBehaviour
{
    [Header("Interfaz de Game Over")]
    public GameObject panelGameOver;

    private bool pantallaActiva = false;

    void Start()
    {
        // Al iniciar el juego, ocultamos el panel
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(false);
        }
    }

    void Update()
    {
        // Si la pantalla está activa y se presiona la barra espaciadora, regresa al Nivel 1
        if (pantallaActiva && Input.GetKeyDown(KeyCode.Space))
        {
            ReiniciarAlNivelUno();
        }
    }

    public void MostrarGameOver()
    {
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);
            pantallaActiva = true;
        }
    }

    // Método auxiliar llamado desde la bandera para garantizar que funcione la tecla espacio
    public void HabilitarLecturaEspacio()
    {
        pantallaActiva = true;
    }

    void ReiniciarAlNivelUno()
    {
        Time.timeScale = 1f;

        // Reiniciar cerezas
        if (System.Type.GetType("RecogerCereza") != null)
        {
            RecogerCereza.ReiniciarContador();
        }

        SceneManager.LoadScene("Nivel1");
    }
}