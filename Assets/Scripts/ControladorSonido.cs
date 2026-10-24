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

    [Header("Sonidos de Ataque")]
    public AudioClip sonidoDisparo;
    public AudioClip sonidoExplosion;

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
        // Reproduce la musica de fondo en bucle al iniciar
        if (audioSource != null && clipMusic != null)
        {
            audioSource.clip = clipMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    public void ReproduceSonido(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void ReproducirDisparo()
    {
        if (sonidoDisparo != null)
        {
            audioSource.PlayOneShot(sonidoDisparo); 
        }
    }

    public void ReproducirExplosion()
    {
        if (sonidoExplosion != null)
        {
            audioSource.PlayOneShot(sonidoExplosion);
        }
    }

    // Metodos rapidos
    public void ReproducirSalto() { ReproduceSonido(clipJump); }
    public void ReproducirCereza() { ReproduceSonido(clipCereza); }
    public void ReproducirMuerte() { ReproduceSonido(clipMuerte); }
    public void ReproducirVictoria() { ReproduceSonido(clipWin); }
}