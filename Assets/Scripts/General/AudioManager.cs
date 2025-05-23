using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Clase serializable para configurar m�sica por escena desde el Inspector
    [System.Serializable]
    public class SceneMusicConfig
    {
        [Tooltip("Nombre exacto de la escena como aparece en Build Settings")]
        public string sceneName; // Identificador �nico de la escena

        [Header("Configuraci�n Primera Visita")]
        public AudioClip firstTimeMusic; // M�sica especial para primera visita
        [Tooltip("�La m�sica inicial debe repetirse en loop?")]
        public bool loopFirstTime = true; // Loop por defecto para experiencia continua

        [Header("Configuraci�n Visitas Posteriores")]
        public AudioClip regularMusic; // M�sica para visitas repetidas
        [Tooltip("�La m�sica normal debe repetirse en loop?")]
        public bool loopRegular = true; // Loop por defecto para m�sica repetitiva

        [Header("Configuraci�n Adicional")]
        [Tooltip("Duraci�n en segundos del fade entre canciones")]
        public float crossfadeDuration = 1f; // Tiempo est�ndar para transici�n suave
        [Tooltip("Volumen espec�fico para esta escena (sobrescribe el global)")]
        [Range(0f, 1f)] public float customVolume = -1f; // Valor negativo indica usar volumen global
    }

    // Escenas donde la m�sica debe detenerse completamente
    [Header("Escenas donde detener la m�sica")]
    public string[] scenesToStopMusic; // Lista configurable en el Inspector

    // Efectos de sonido comunes predefinidos para acceso r�pido
    [Header("Efectos de sonido")]
    public AudioClip puertas; // Sonido de puertas abri�ndose/cerr�ndose
    public AudioClip sonidoTransformacion; // Efecto para transformaciones
    public AudioClip sonidoMuerte; // Sonido de muerte de cabecilla
    public AudioClip error; // Sonido de error/acci�n inv�lida
    public AudioClip agarrar; // Sonido al recoger objetos
    public AudioClip dejar; // Sonido al soltar objetos
    public AudioClip baldosas; // Sonido de pisadas/pasos

    // Configuraci�n global del sistema de audio
    [Header("Configuraci�n Global")]
    [Range(0f, 1f)] public float defaultMusicVolume = 0.5f; // Volumen base ajustable
    public bool debugLogs = false; // Habilita logs detallados para depuraci�n

    // Lista completa de configuraciones musicales por escena
    [Header("M�sica por Escena")]
    public List<SceneMusicConfig> sceneMusicList = new List<SceneMusicConfig>();

    // Componentes de AudioSource para manejar sonidos
    private AudioSource soundEffectSource; // Fuente dedicada a efectos de sonido
    private AudioSource musicSource1; // Primera fuente para m�sica (usada en crossfading)
    private AudioSource musicSource2; // Segunda fuente para m�sica (alternativa para crossfading)

    // Estado interno del sistema
    private HashSet<string> visitedScenes = new HashSet<string>(); // Registro de escenas visitadas
    public static AudioManager Instance { get; private set; } // Instancia singleton

    // Inicializaci�n del singleton al cargar el script
    void Awake()
    {
        // Patr�n singleton - solo una instancia permitida
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persiste entre escenas
            InitializeAudioSources(); // Configura componentes de audio
            SceneManager.sceneLoaded += OnSceneLoaded; // Suscribe al evento de cambio de escena

            if (debugLogs) Debug.Log("AudioManager inicializado");
        }
        else
        {
            // Destruye instancias duplicadas
            Destroy(gameObject);
        }
    }

    // Configuraci�n inicial de los componentes AudioSource
    void InitializeAudioSources()
    {
        // Fuente para efectos de sonido
        soundEffectSource = gameObject.AddComponent<AudioSource>();
        soundEffectSource.playOnAwake = false; // No reproducir autom�ticamente

        // Dos fuentes de m�sica para permitir crossfading
        musicSource1 = gameObject.AddComponent<AudioSource>();
        musicSource2 = gameObject.AddComponent<AudioSource>();

        // Configuraci�n com�n para fuentes musicales
        musicSource1.playOnAwake = musicSource2.playOnAwake = false; // Control manual
        musicSource1.loop = musicSource2.loop = true; // Loop activado por defecto
        musicSource1.volume = musicSource2.volume = 0f; // Inicio silenciado
    }

    // Manejador de evento cuando se carga una nueva escena
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;

        // Verifica si esta escena requiere silenciar la m�sica
        bool shouldStopMusic = false;
        foreach (string sceneToStop in scenesToStopMusic)
        {
            if (sceneName == sceneToStop)
            {
                shouldStopMusic = true;
                break; // Salida temprana del bucle
            }
        }

        if (shouldStopMusic)
        {
            if (debugLogs) Debug.Log($"Deteniendo m�sica en escena: {sceneName}");
            StopMusic(); // Detiene toda la m�sica
        }
        else
        {
            // Reproduce m�sica adecuada para la escena
            PlayMusicForScene(sceneName);
        }
    }

    // Selecciona y reproduce la m�sica apropiada para una escena
    public void PlayMusicForScene(string sceneName)
    {
        // Busca configuraci�n para esta escena
        SceneMusicConfig config = sceneMusicList.Find(x => x.sceneName == sceneName);

        if (config != null)
        {
            // Determina si es primera visita
            bool isFirstVisit = !visitedScenes.Contains(sceneName);

            // Selecciona clip y configuraci�n basado en estado de visita
            AudioClip clipToPlay = isFirstVisit ? config.firstTimeMusic : config.regularMusic;
            bool shouldLoop = isFirstVisit ? config.loopFirstTime : config.loopRegular;
            float volume = config.customVolume >= 0 ? config.customVolume : defaultMusicVolume;
            float fadeTime = config.crossfadeDuration;

            if (clipToPlay != null)
            {
                if (debugLogs) Debug.Log($"Reproduciendo para {sceneName} (primera vez: {isFirstVisit}): {clipToPlay.name}");
                PlayMusic(clipToPlay, shouldLoop, volume, fadeTime);
            }

            // Registra primera visita
            if (isFirstVisit)
            {
                visitedScenes.Add(sceneName);
                if (debugLogs) Debug.Log($"Escena {sceneName} marcada como visitada");
            }
        }
        else if (debugLogs)
        {
            Debug.Log($"No se encontr� configuraci�n de m�sica para: {sceneName}");
        }
    }

    // Reproduce una pista musical con transici�n suave
    public void PlayMusic(AudioClip clip, bool loop, float volume, float fadeTime)
    {
        if (clip == null) return; // Validaci�n de entrada

        // Selecciona fuente inactiva para nueva canci�n
        AudioSource targetSource = musicSource1.isPlaying ? musicSource2 : musicSource1;
        AudioSource otherSource = targetSource == musicSource1 ? musicSource2 : musicSource1;

        // Configura nueva canci�n
        targetSource.clip = clip;
        targetSource.loop = loop;
        targetSource.volume = 0f; // Inicia silenciado para fade in
        targetSource.Play();

        // Inicia crossfade
        StopAllCoroutines(); // Detiene transiciones previas
        StartCoroutine(CrossFadeRoutine(targetSource, otherSource, volume, fadeTime));
    }

    // Corrutina para transici�n suave entre pistas
    IEnumerator CrossFadeRoutine(AudioSource newSource, AudioSource oldSource, float targetVolume, float duration)
    {
        float timer = 0f;
        float initialVolumeOld = oldSource.volume; // Captura volumen inicial

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float ratio = timer / duration; // Progreso normalizado (0-1)

            // Interpola vol�menes
            newSource.volume = Mathf.Lerp(0f, targetVolume, ratio);
            oldSource.volume = Mathf.Lerp(initialVolumeOld, 0f, ratio);

            yield return null; // Espera al siguiente frame
        }

        oldSource.Stop(); // Detiene fuente antigua completamente
        newSource.volume = targetVolume; // Asegura volumen final exacto
    }

    // Detiene toda la m�sica con fade out
    public void StopMusic(float fadeOutTime = 1f)
    {
        StopAllCoroutines(); // Cancela transiciones en curso
        StartCoroutine(FadeOutAllMusic(fadeOutTime)); // Inicia fade out
    }

    // Corrutina para atenuar y detener toda la m�sica
    IEnumerator FadeOutAllMusic(float duration)
    {
        float timer = 0f;
        float startVol1 = musicSource1.volume; // Volumen inicial fuente 1
        float startVol2 = musicSource2.volume; // Volumen inicial fuente 2

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float ratio = timer / duration;

            // Aten�a ambas fuentes simult�neamente
            musicSource1.volume = Mathf.Lerp(startVol1, 0f, ratio);
            musicSource2.volume = Mathf.Lerp(startVol2, 0f, ratio);

            yield return null;
        }

        // Detiene completamente ambas fuentes
        musicSource1.Stop();
        musicSource2.Stop();
    }

    // M�todos espec�ficos para efectos de sonido comunes
    public void PlayDoorSound()
    {
        if (puertas != null)
        {
            soundEffectSource.PlayOneShot(puertas);
            if (debugLogs) Debug.Log("Sonido de puerta reproducido");
        }
    }

    // Obtiene duraci�n del sonido de puerta (�til para sincronizaci�n)
    public float GetSoundDuration()
    {
        return puertas != null ? puertas.length : 0f;
    }

    // M�todo gen�rico para reproducir cualquier sonido
    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            soundEffectSource.PlayOneShot(clip);
            if (debugLogs) Debug.Log($"Sonido reproducido: {clip.name}");
        }
    }

    // Reinicia el registro de escenas visitadas
    public void ResetSceneMemory()
    {
        visitedScenes.Clear();
        if (debugLogs) Debug.Log("Memoria de escenas reiniciada");
    }

    // Ajusta volumen global de la m�sica
    public void SetGlobalVolume(float volume)
    {
        defaultMusicVolume = Mathf.Clamp01(volume); // Asegura valor entre 0-1
        musicSource1.volume = musicSource2.volume = defaultMusicVolume; // Aplica inmediatamente
    }

    // Limpieza al destruir el objeto
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Desuscribe evento
    }

    // M�todos espec�ficos para efectos comunes (mejoran legibilidad)
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

    // Reproduce sonido independientemente de pausa/efectos globales
    public void PlaySoundIndependent(AudioClip clip, float volumeScale = 1.0f)
    {
        if (clip == null) return;

        // Crea objeto temporal para el sonido
        GameObject tempSoundObj = new GameObject("TempAudio");
        AudioSource tempSource = tempSoundObj.AddComponent<AudioSource>();

        // Configuraci�n para ignorar efectos globales
        tempSource.ignoreListenerPause = true; // Suena aunque el juego est� pausado
        tempSource.bypassEffects = true; // Ignora efectos de audio
        tempSource.bypassListenerEffects = true; // Ignora efectos del listener
        tempSource.bypassReverbZones = true; // Ignora reverberaci�n
        tempSource.pitch = 1.0f; // Fuerza pitch normal (evita alteraciones)

        // Reproduce y programa autodestrucci�n
        tempSource.PlayOneShot(clip, volumeScale);
        Destroy(tempSoundObj, clip.length + 0.1f); // Peque�o margen adicional
    }
}





