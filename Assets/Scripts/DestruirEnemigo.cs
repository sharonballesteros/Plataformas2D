using UnityEngine;

public class DestruirEnemigo : MonoBehaviour
{
    public GameObject explosionPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.name.Contains("Player") || collision.gameObject.layer == LayerMask.NameToLayer("mataEnemigo"))
        {
            Rigidbody2D rbJugador = collision.GetComponent<Rigidbody2D>();
            if (rbJugador != null)
            {
                rbJugador.linearVelocity = new Vector2(rbJugador.linearVelocity.x, 7f);
            }

            Explotar();
        }
    }

    public void Explotar()
    {
        Vector3 posicionReal = transform.parent != null ? transform.parent.position : transform.position;
        SpriteRenderer spriteVisual = transform.parent != null ? transform.parent.GetComponentInChildren<SpriteRenderer>() : GetComponentInChildren<SpriteRenderer>();

        if (spriteVisual != null)
        {
            posicionReal = spriteVisual.transform.position;
        }

        if (explosionPrefab != null)
        {
            GameObject explosion = Instantiate(explosionPrefab, posicionReal, Quaternion.identity);
            SpriteRenderer srExplosion = explosion.GetComponent<SpriteRenderer>();
            if (srExplosion != null)
            {
                srExplosion.sortingOrder = 50;
            }

            // Llama al administrador para emitir el sonido de la explosion
            ControladorSonido controladorSonido = FindFirstObjectByType<ControladorSonido>();
            if (controladorSonido != null)
            {
                controladorSonido.ReproducirExplosion();
            }
        }

        if (transform.parent != null)
        {
            // Apaga el script del enemigo blanco si lo tiene
            ControlEnemigo controlPadre = transform.parent.GetComponent<ControlEnemigo>();
            if (controlPadre != null) controlPadre.enabled = false;

            // Apaga el script del enemigo azul (Ene3) si este script estuviera en él
            EnemigoDisparador2 controlDisparador = transform.parent.GetComponent<EnemigoDisparador2>();
            if (controlDisparador != null) controlDisparador.enabled = false;

            // Desactiva únicamente los colliders de ESTE enemigo en específico
            Collider2D[] cols = transform.parent.GetComponentsInChildren<Collider2D>();
            foreach (Collider2D c in cols) c.enabled = false;

            // Destruye exclusivamente al objeto padre de este script
            Destroy(transform.parent.gameObject, 0.05f);
        }
        else
        {
            Destroy(gameObject, 0.05f);
        }
    }
}