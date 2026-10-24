using UnityEngine;

public class ProyectilJugador : MonoBehaviour
{
    public float velocidad = 15f;
    public int danio = 1;
    public float tiempoDeVida = 3f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * velocidad;
        Destroy(gameObject, tiempoDeVida);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemigo") || col.gameObject.name.Contains("Ene"))
        {
            // Busca el script de destruccion en el objeto golpeado o en sus hijos
            DestruirEnemigo enemigo = col.GetComponent<DestruirEnemigo>();
            if (enemigo == null) 
            {
                enemigo = col.GetComponentInChildren<DestruirEnemigo>();
            }

            if (enemigo != null)
            {
                enemigo.Explotar();
            }
            else
            {
                Destroy(col.gameObject);
            }

            DestruirProyectil();
        }
        else if (col.CompareTag("Escenario") || col.CompareTag("Suelo") || col.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            DestruirProyectil();
        }
    }

    void DestruirProyectil()
    {
        Destroy(gameObject);
    }
}