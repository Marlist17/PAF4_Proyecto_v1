using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Tooltip("Clip que se reproducirá al tocar este objeto")]
    public AudioClip soundClip;
    private AudioSource puerta;
    public static AudioManager Instance;
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
        puerta = GetComponent<AudioSource>();
        if (puerta == null)
            puerta = gameObject.AddComponent<AudioSource>();

        puerta.playOnAwake = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Opcional: verifica que el que entra tenga cierto tag
        if (other.CompareTag("Player")) // O elimina esta línea para que funcione con cualquier cosa
        {
            if (soundClip != null)
                puerta.PlayOneShot(soundClip);
        }
    }
    public void PlaySound(AudioClip clip)
    {
        puerta.PlayOneShot(clip);
    }
}

