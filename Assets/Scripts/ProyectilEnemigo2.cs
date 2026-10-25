using UnityEngine;

public class ProyectilEnemigo2 : MonoBehaviour
{
    public float velocidad = 10f;
    public float tiempoDeVida = 2f;
    private float direccion = 1f;

    public void AsignarDireccion(float nuevaDireccion)
    {
        direccion = nuevaDireccion;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null && direccion < 0)
        {
            sr.flipX = true;
        }
    }

    void Start()
    {
        Destroy(gameObject, tiempoDeVida);
    }

    void Update()
    {
        transform.Translate(Vector2.right * direccion * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ControlVidas vidasJugador = collision.GetComponent<ControlVidas>();
            if (vidasJugador != null)
            {
                vidasJugador.PerderVida();
            }
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Suelo") || collision.CompareTag("Plataforma"))
        {
            Destroy(gameObject);
        }
    }
}