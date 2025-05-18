using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Tooltip("Clip que se reproducirá al tocar este objeto")]
    public AudioClip puertas;
    private AudioSource audioSource;
    public static AudioManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
            audioSource.playOnAwake = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayDoorSound()
    {
        if (puertas != null)
        {
            audioSource.PlayOneShot(puertas);
        }
    }

    public float GetSoundDuration()
    {
        return puertas != null ? puertas.length : 0f;
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            GetComponent<AudioSource>().PlayOneShot(clip);
        }
    }
}




/*using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Tooltip("Clip que se reproducirá al tocar este objeto")]
    public AudioClip puertas;
    private AudioSource puerta;
    public static AudioManager Instance;
    public GameObject casaRicos;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        // Usa el AudioSource existente o lo agrega
        AudioSource puerta = casaRicos.GetComponent<AudioSource>();
        if (puerta == null)
            puerta = gameObject.AddComponent<AudioSource>();

        puerta.playOnAwake = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // O elimina esta línea para que funcione con cualquier cosa
        {
                puerta.PlayOneShot(puertas);
        }
        // Opcional: verifica que el que entra tenga cierto tag
        /*if (other.CompareTag("Player")) // O elimina esta línea para que funcione con cualquier cosa
        {
            if (puertas != null)
                puerta.PlayOneShot(puertas);
        }
    }
    public void PlaySound(AudioClip clip)
    {
        puerta.PlayOneShot(clip);
    }
}*/

