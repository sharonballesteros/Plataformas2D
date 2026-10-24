using UnityEngine;

public class AutoDestruir : MonoBehaviour
{
    public float tiempoVida = 0.5f; // Tiempo que permanece visible antes de borrarse

    void Start()
    {
        Destroy(gameObject, tiempoVida);
    }
}