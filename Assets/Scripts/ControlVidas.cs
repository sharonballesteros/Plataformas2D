using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ControlVidas : MonoBehaviour
{
    public static int vidas = 3;
    public List<GameObject> vidasUI;

    private Animator animator;

    private void Start()
    {
        // Busca el Animator en este objeto o en su hijo "imagen"
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Transform hijoImagen = transform.Find("imagen");
            if (hijoImagen != null)
            {
                animator = hijoImagen.GetComponent<Animator>();
            }
        }

        ActualizarVidasUI();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detecta daño por colisión física si toca "peligros" o "enemigo"
        if (collision.gameObject.layer == LayerMask.NameToLayer("peligros") ||
            collision.gameObject.layer == LayerMask.NameToLayer("enemigo"))
        {
            PerderVida();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. Detección de Daño por Trigger en la capa "peligros" o "enemigo"
        if (collision.gameObject.layer == LayerMask.NameToLayer("peligros") ||
            collision.gameObject.layer == LayerMask.NameToLayer("enemigo"))
        {
            PerderVida();
        }

        // 2. Detección de Coleccionables por Trigger usando la capa "diamante"
        if (collision.gameObject.layer == LayerMask.NameToLayer("diamante"))
        {
            AumentarVida();
            Destroy(collision.gameObject); // Destruye el coleccionable al tocarlo
        }
    }

    public void PerderVida()
    {
        if (vidas > 0)
        {
            vidas--;
            ActualizarVidasUI();
            StartCoroutine(EfectoDanio());

            // --- NUEVO: Reproducir sonido de muerte/daño ---
            ControladorSonido controladorSonido = FindFirstObjectByType<ControladorSonido>();
            if (controladorSonido != null)
            {
                controladorSonido.ReproducirMuerte();
            }

            // Si las vidas llegan a 0...
            if (vidas <= 0)
            {
                ControladorJuego controlador = FindFirstObjectByType<ControladorJuego>();
                if (controlador != null)
                {
                    controlador.RecargarEscena();
                }
            }
        }
    }

    void AumentarVida()
    {
        int vidasMaximas = 5; // Límite máximo de vidas según tus elementos en UI
        if (vidas < vidasMaximas)
        {
            vidas++;
            ActualizarVidasUI();
            Debug.Log("¡Vida aumentada! Vidas actuales: " + vidas);
        }
    }

    IEnumerator EfectoDanio()
    {
        if (animator != null)
        {
            animator.SetBool("colisionPeligro", true);
        }

        yield return new WaitForSeconds(1.0f);

        if (animator != null)
        {
            animator.SetBool("colisionPeligro", false);
        }
    }

    public void ActualizarVidasUI()
    {
        for (int i = 0; i < vidasUI.Count; i++)
        {
            if (vidasUI[i] != null)
            {
                vidasUI[i].SetActive(i < vidas);
            }
        }
    }
}