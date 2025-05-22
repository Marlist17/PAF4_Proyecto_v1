using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class SceneMusicConfig
    {
        [Tooltip("Nombre exacto de la escena (como en Build Settings)")]
        public string sceneName;

        [Header("Configuración Primera Visita")]
        public AudioClip firstTimeMusic;
        [Tooltip("¿La música inicial debe repetirse?")]
        public bool loopFirstTime = true;

        [Header("Configuración Visitas Posteriores")]
        public AudioClip regularMusic;
        [Tooltip("¿La música normal debe repetirse?")]
        public bool loopRegular = true;

        [Header("Configuración Adicional")]
        [Tooltip("Duración del fade entre canciones")]
        public float crossfadeDuration = 1f;
        [Tooltip("Volumen específico para esta escena (opcional)")]
        [Range(0f, 1f)] public float customVolume = -1f;
    }
    [Header("Escenas donde detener la música")]
    public string[] scenesToStopMusic;

    [Header("Efectos de sonido")]
    public AudioClip puertas;

    public AudioClip sonidoTransformacion;
    public AudioClip sonidoMuerte;
    public AudioClip error;
    public AudioClip agarrar;
    public AudioClip dejar;

    public AudioClip baldosas;

    [Header("Configuración Global")]
    [Range(0f, 1f)] public float defaultMusicVolume = 0.5f;
    public bool debugLogs = false;

    [Header("Música por Escena")]
    public List<SceneMusicConfig> sceneMusicList = new List<SceneMusicConfig>();

    // Audio Sources
    private AudioSource soundEffectSource;
    private AudioSource musicSource1;
    private AudioSource musicSource2;

    // Estado
    private HashSet<string> visitedScenes = new HashSet<string>();
    public static AudioManager Instance { get; private set; }


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioSources();
            SceneManager.sceneLoaded += OnSceneLoaded;

            if (debugLogs) Debug.Log("AudioManager inicializado");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializeAudioSources()
    {
        soundEffectSource = gameObject.AddComponent<AudioSource>();
        soundEffectSource.playOnAwake = false;

        musicSource1 = gameObject.AddComponent<AudioSource>();
        musicSource2 = gameObject.AddComponent<AudioSource>();

        musicSource1.playOnAwake = musicSource2.playOnAwake = false;
        musicSource1.loop = musicSource2.loop = true;
        musicSource1.volume = musicSource2.volume = 0f;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;

        // Verificar si debemos detener la música en esta escena
        bool shouldStopMusic = false;
        foreach (string sceneToStop in scenesToStopMusic)
        {
            if (sceneName == sceneToStop)
            {
                shouldStopMusic = true;
                break;
            }
        }

        if (shouldStopMusic)
        {
            if (debugLogs) Debug.Log($"Deteniendo música en escena: {sceneName}");
            StopMusic();
        }
        else
        {
            // Comportamiento normal - reproducir música de la escena
            PlayMusicForScene(sceneName);
        }

        
    }
    /*void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;
        PlayMusicForScene(sceneName);
    }*/

    public void PlayMusicForScene(string sceneName)
    {
        SceneMusicConfig config = sceneMusicList.Find(x => x.sceneName == sceneName);

        if (config != null)
        {
            bool isFirstVisit = !visitedScenes.Contains(sceneName);
            AudioClip clipToPlay = isFirstVisit ? config.firstTimeMusic : config.regularMusic;
            bool shouldLoop = isFirstVisit ? config.loopFirstTime : config.loopRegular;
            float volume = config.customVolume >= 0 ? config.customVolume : defaultMusicVolume;
            float fadeTime = config.crossfadeDuration;

            if (clipToPlay != null)
            {
                if (debugLogs) Debug.Log($"Reproduciendo para {sceneName} (primera vez: {isFirstVisit}): {clipToPlay.name}");
                PlayMusic(clipToPlay, shouldLoop, volume, fadeTime);
            }

            if (isFirstVisit)
            {
                visitedScenes.Add(sceneName);
                if (debugLogs) Debug.Log($"Escena {sceneName} marcada como visitada");
            }
        }
        else if (debugLogs)
        {
            Debug.Log($"No se encontró configuración de música para: {sceneName}");
        }
    }
   

    public void PlayMusic(AudioClip clip, bool loop, float volume, float fadeTime)
    {
        if (clip == null) return;

        AudioSource targetSource = musicSource1.isPlaying ? musicSource2 : musicSource1;
        AudioSource otherSource = targetSource == musicSource1 ? musicSource2 : musicSource1;

        targetSource.clip = clip;
        targetSource.loop = loop;
        targetSource.volume = 0f;
        targetSource.Play();

        StopAllCoroutines();
        StartCoroutine(CrossFadeRoutine(targetSource, otherSource, volume, fadeTime));
    }

    IEnumerator CrossFadeRoutine(AudioSource newSource, AudioSource oldSource, float targetVolume, float duration)
    {
        float timer = 0f;
        float initialVolumeOld = oldSource.volume;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float ratio = timer / duration;

            newSource.volume = Mathf.Lerp(0f, targetVolume, ratio);
            oldSource.volume = Mathf.Lerp(initialVolumeOld, 0f, ratio);
            yield return null;
        }

        oldSource.Stop();
        newSource.volume = targetVolume;
    }

    public void StopMusic(float fadeOutTime = 1f)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutAllMusic(fadeOutTime));
    }

    IEnumerator FadeOutAllMusic(float duration)
    {
        float timer = 0f;
        float startVol1 = musicSource1.volume;
        float startVol2 = musicSource2.volume;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float ratio = timer / duration;

            musicSource1.volume = Mathf.Lerp(startVol1, 0f, ratio);
            musicSource2.volume = Mathf.Lerp(startVol2, 0f, ratio);
            yield return null;
        }

        musicSource1.Stop();
        musicSource2.Stop();
    }

    public void PlayDoorSound()
    {
        if (puertas != null)
        {
            soundEffectSource.PlayOneShot(puertas);
            if (debugLogs) Debug.Log("Sonido de puerta reproducido");
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
            soundEffectSource.PlayOneShot(clip);
            if (debugLogs) Debug.Log($"Sonido reproducido: {clip.name}");
        }
    }

    public void ResetSceneMemory()
    {
        visitedScenes.Clear();
        if (debugLogs) Debug.Log("Memoria de escenas reiniciada");
    }

    public void SetGlobalVolume(float volume)
    {
        defaultMusicVolume = Mathf.Clamp01(volume);
        musicSource1.volume = musicSource2.volume = defaultMusicVolume;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void PlayTransformSound()
    {
        PlaySound(sonidoTransformacion);
    }
    public void PlayDeath()
    {
        PlaySound(sonidoMuerte);
    }

    public void PlayError()
    {
        PlaySound(error);
    }
    public void PlayAgarrar()
    {
        PlaySound(agarrar);
    }
    public void PlayDejar()
    {
        PlaySound(dejar);
    }
    public void PlaySoundIndependent(AudioClip clip, float volumeScale = 1.0f)
    {
        if (clip == null) return;

        // Crear un GameObject temporal para el sonido
        GameObject tempSoundObj = new GameObject("TempAudio");
        AudioSource tempSource = tempSoundObj.AddComponent<AudioSource>();

        // Configurar para ignorar efectos de tiempo
        tempSource.ignoreListenerPause = true;
        tempSource.bypassEffects = true;
        tempSource.bypassListenerEffects = true;
        tempSource.bypassReverbZones = true;
        tempSource.pitch = 1.0f; // Fuerza pitch normal

        // Reproducir y destruir después
        tempSource.PlayOneShot(clip, volumeScale);
        Destroy(tempSoundObj, clip.length + 0.1f);
    }

}





