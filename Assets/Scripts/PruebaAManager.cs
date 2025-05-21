using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PruebaAManager : MonoBehaviour
{
    // Configuración de música por escena
    [System.Serializable]
    public class SceneMusic
    {
        public string sceneName; // Nombre exacto de la escena

        public AudioClip firstVisitMusic; // Música para la primera vez
        public bool loopFirstMusic = true;

        public AudioClip regularMusic; // Música para visitas normales
        public bool loopRegularMusic = true;

        public float fadeDuration = 1.0f; // Tiempo de transición
        [Range(0f, 1f)] public float volume = 0.5f; // Volumen para esta escena
    }

    // Sonidos del juego
    public AudioClip doorSound;
    public AudioClip transformationSound;
    public AudioClip errorSound;
    public AudioClip grabSound;
    public AudioClip dropSound;
    public AudioClip tileSound;

    // Configuración general
    [Range(0f, 1f)] public float globalVolume = 0.5f;
    public bool debugMode = false;

    // Lista de música para cada escena
    public List<SceneMusic> sceneMusics = new List<SceneMusic>();

    // Fuentes de audio
    private AudioSource soundEffectsSource;
    private AudioSource musicSource1;
    private AudioSource musicSource2;

    // Control de escenas visitadas
    private List<string> visitedScenes = new List<string>();

    // Instancia única
    public static PruebaAManager instance;

    void Awake()
    {
        // Asegurar que solo haya un AudioManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SetupAudioSources();
            SceneManager.sceneLoaded += OnNewSceneLoaded;

            if (debugMode) Debug.Log("AudioManager iniciado");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void SetupAudioSources()
    {
        // Configurar las fuentes de audio
        soundEffectsSource = gameObject.AddComponent<AudioSource>();
        soundEffectsSource.playOnAwake = false;

        musicSource1 = gameObject.AddComponent<AudioSource>();
        musicSource2 = gameObject.AddComponent<AudioSource>();

        musicSource1.playOnAwake = false;
        musicSource2.playOnAwake = false;
        musicSource1.loop = true;
        musicSource2.loop = true;
        musicSource1.volume = 0f;
        musicSource2.volume = 0f;
    }

    void OnNewSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForCurrentScene(scene.name);
    }

    public void PlayMusicForCurrentScene(string sceneName)
    {
        // Buscar la música para esta escena
        SceneMusic musicConfig = null;

        foreach (SceneMusic config in sceneMusics)
        {
            if (config.sceneName == sceneName)
            {
                musicConfig = config;
                break;
            }
        }

        if (musicConfig != null)
        {
            bool isFirstVisit = !visitedScenes.Contains(sceneName);
            AudioClip musicToPlay = isFirstVisit ? musicConfig.firstVisitMusic : musicConfig.regularMusic;
            bool shouldLoop = isFirstVisit ? musicConfig.loopFirstMusic : musicConfig.loopRegularMusic;
            float volume = musicConfig.volume > 0 ? musicConfig.volume : globalVolume;
            float fadeTime = musicConfig.fadeDuration;

            if (musicToPlay != null)
            {
                if (debugMode) Debug.Log("Reproduciendo música: " + musicToPlay.name);
                StartMusic(musicToPlay, shouldLoop, volume, fadeTime);
            }

            if (isFirstVisit)
            {
                visitedScenes.Add(sceneName);
                if (debugMode) Debug.Log("Primera visita a " + sceneName);
            }
        }
        else if (debugMode)
        {
            Debug.Log("No hay música configurada para: " + sceneName);
        }
    }

    public void StartMusic(AudioClip musicClip, bool loop, float volume, float fadeTime)
    {
        if (musicClip == null) return;

        // Determinar qué fuente de audio usar
        AudioSource newSource = musicSource1.isPlaying ? musicSource2 : musicSource1;
        AudioSource oldSource = newSource == musicSource1 ? musicSource2 : musicSource1;

        // Configurar la nueva música
        newSource.clip = musicClip;
        newSource.loop = loop;
        newSource.volume = 0f;
        newSource.Play();

        // Iniciar el fade
        StopAllCoroutines();
        StartCoroutine(FadeBetweenMusic(newSource, oldSource, volume, fadeTime));
    }

    IEnumerator FadeBetweenMusic(AudioSource incoming, AudioSource outgoing, float targetVolume, float duration)
    {
        float time = 0f;
        float startVolumeOut = outgoing.volume;

        while (time < duration)
        {
            time += Time.deltaTime;
            float progress = time / duration;

            incoming.volume = Mathf.Lerp(0f, targetVolume, progress);
            outgoing.volume = Mathf.Lerp(startVolumeOut, 0f, progress);

            yield return null;
        }

        outgoing.Stop();
        incoming.volume = targetVolume;
    }

    public void StopAllMusic(float fadeOutTime = 1f)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutMusic(fadeOutTime));
    }

    IEnumerator FadeOutMusic(float duration)
    {
        float time = 0f;
        float startVol1 = musicSource1.volume;
        float startVol2 = musicSource2.volume;

        while (time < duration)
        {
            time += Time.deltaTime;
            float progress = time / duration;

            musicSource1.volume = Mathf.Lerp(startVol1, 0f, progress);
            musicSource2.volume = Mathf.Lerp(startVol2, 0f, progress);

            yield return null;
        }

        musicSource1.Stop();
        musicSource2.Stop();
    }

    // Métodos para efectos de sonido
    public void PlayDoorSound()
    {
        if (doorSound != null)
        {
            soundEffectsSource.PlayOneShot(doorSound);
            if (debugMode) Debug.Log("Sonido de puerta reproducido");
        }
    }

    public float GetSoundDuration()
    {
        return doorSound != null ? doorSound.length : 0f;
    }

    public void PlaySound(AudioClip sound)
    {
        if (sound != null)
        {
            soundEffectsSource.PlayOneShot(sound);
            if (debugMode) Debug.Log("Reproduciendo sonido: " + sound.name);
        }
    }

    // Funciones adicionales
    public void ResetVisitedScenes()
    {
        visitedScenes.Clear();
        if (debugMode) Debug.Log("Historial de escenas reiniciado");
    }

    public void SetGlobalVolume(float newVolume)
    {
        globalVolume = Mathf.Clamp(newVolume, 0f, 1f);
        musicSource1.volume = globalVolume;
        musicSource2.volume = globalVolume;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnNewSceneLoaded;
    }
}