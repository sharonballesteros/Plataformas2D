using UnityEngine;

public class ProyectilEnemigo : MonoBehaviour
{
    public float velocidad = 10f;
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
        // Busca el script de vidas en el objeto tocado o en su padre
        ControlVidas vidasJugador = col.GetComponent<ControlVidas>();
        if (vidasJugador == null) 
        {
            vidasJugador = col.GetComponentInParent<ControlVidas>();
        }

        // Si encuentra el script de vidas, hace danio y se destruye
        if (vidasJugador != null)
        {
            vidasJugador.PerderVida();
            Destroy(gameObject);
        }
        // Si golpea las paredes o el suelo
        else if (col.CompareTag("Escenario") || col.CompareTag("Suelo") || col.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}