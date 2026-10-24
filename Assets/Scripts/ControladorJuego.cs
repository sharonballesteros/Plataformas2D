using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ControladorJuego : MonoBehaviour
{
    [Header("Lista de Niveles (GameObjects en la escena)")]
    public List<GameObject> listadoNiveles;

    [Header("Control de Índice")]
    public static int numeroNivel = 0; // Guarda el nivel actual

    void Start()
    {
        // 1. Oculta todos los niveles de inicio
        foreach (GameObject nivel in listadoNiveles)
        {
            if (nivel != null)
            {
                nivel.SetActive(false);
            }
        }

        // 2. Muestra únicamente el nivel actual según el índice
        if (listadoNiveles.Count > 0 && numeroNivel < listadoNiveles.Count)
        {
            if (listadoNiveles[numeroNivel] != null)
            {
                listadoNiveles[numeroNivel].SetActive(true);
                Debug.Log("Activando nivel índice: " + numeroNivel);
            }
        }
    }

    // Método que se activa al llegar a la meta
    public void SiguienteNivel()
    {
        numeroNivel++;
        Debug.Log("Avanzando al índice de nivel: " + numeroNivel);

        // Si aún hay niveles en la lista, recarga la escena actual para mostrarlos
        if (numeroNivel < listadoNiveles.Count)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.Log("¡Juego completado!");
            // Opcional: Volver al menú principal o reiniciar el índice a 0
            numeroNivel = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // Método para recargar la escena en caso de Game Over
    public void RecargarEscena()
    {
        Debug.Log("Reiniciando nivel...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}