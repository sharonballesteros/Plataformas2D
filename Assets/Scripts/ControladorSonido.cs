using UnityEngine;

public class ControladorSonido : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Tus Clips de Sonido")]
    public AudioClip clipCereza;
    public AudioClip clipJump;
    public AudioClip clipMuerte;
    public AudioClip clipWin;
    public AudioClip clipMusic;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Start()
    {
        // Reproduce la música de fondo en bucle al iniciar la escena
        if (audioSource != null && clipMusic != null)
        {
            audioSource.clip = clipMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    // Método para reproducir pasando el clip directamente
    public void ReproduceSonido(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // Métodos rápidos para llamarlos por código
    public void ReproducirSalto() { ReproduceSonido(clipJump); }
    public void ReproducirCereza() { ReproduceSonido(clipCereza); }
    public void ReproducirMuerte() { ReproduceSonido(clipMuerte); }
    public void ReproducirVictoria() { ReproduceSonido(clipWin); }
}