using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MCabecilla : MonoBehaviour
{
    public GameObject icono; // Se�al de que puedes hablar con �l
    bool jugadorEnRango = false;
    [SerializeField] AudioClip sonidoMuerte;
    [SerializeField] GameObject transicion;
    [SerializeField] private float delaySonido = 0.001f;

    // Variable para controlar si ya se activ� la secuencia de muerte
    private bool muerteActivada = false;

    void Start()
    {
        icono.SetActive(false);
    }

    void Update()
    {
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E) && !muerteActivada)
        {
            muerteActivada = true;
            GameManager.Instance.muerteCabecilla = true;
            StartCoroutine(SequenciaMuerte());
        }
    }

    IEnumerator SequenciaMuerte()
    {
        // Forzar tiempo normal antes del sonido
        Time.timeScale = 1f;

        // Reproducir sonido independientemente del timeScale
        if (sonidoMuerte != null)
        {
            AudioManager.Instance.PlaySoundIndependent(sonidoMuerte);
        }
        transicion.SetActive(true); // Activar la transici�n
        // Esperar mientras suena el efecto
        yield return new WaitForSecondsRealtime(3f); // Usar tiempo real

        // Cambiar de escena
        CambioEscena();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorEnRango = true;
            icono.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorEnRango = false;
            icono.SetActive(false);
        }
    }

    public void CambioEscena()
    {
        int sala = 10;
        SceneManager.LoadScene(sala);
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MCabecilla : MonoBehaviour
{

    public GameObject icono; //Se�al de que puedes hablar con �l
    bool jugadorEnRango = false; // Variable para detectar si el jugador est� en el trigger
    [SerializeField] AudioClip sonidoMuerte;
    [SerializeField] GameObject transicion; // Asigna el clip de audio en el Inspector

    [SerializeField] private float delaySonido = 0.001f; // Tiempo antes del sonido
    void Start()
    {
        icono.SetActive(false); //Volvemos false el icono

    }

    void Update()
    {



        // Verifica si el jugador est� en el �rea y presiona "E"
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.muerteCabecilla = true;

        }
        if (GameManager.Instance.muerteCabecilla == true)
        {
            Invoke("ReproducirSonidoYCambiarEscena", delaySonido);
            //AudioManager.Instance.PlaySound(sonidoMuerte);
            //Escena(); //Llamamos a la funci�n de espera
            //Escena(); //Llamamos a la funci�n de espera

        }

    }

    private void OnTriggerEnter2D(Collider2D collision) //Si ha entrado en el trigger
    {

        // Verifica si el objeto que entra es el jugador
        if (collision.CompareTag("Player"))
        {
            jugadorEnRango = true; //Marca que si est� en rango
            icono.SetActive(true); //Y vuelve visible el icono
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //Si sale del trigger
    {
        if (collision.CompareTag("Player")) //Y es el jugador
        {
            jugadorEnRango = false; //Volvemos falsa la condici�n
            icono.SetActive(false); //Y vuelve invisible el icono
        }
    }

    private void ReproducirSonidoYCambiarEscena()
    {
        // Reproducir sonido de golpe
        if (sonidoMuerte != null)
        {
            sonidoMuerte.pitch = 1f;
            AudioManager.Instance.PlaySound(sonidoMuerte);
        }

        // Cambiar de escena despu�s de un breve delay
        Invoke("CambioEscena", 1.2f);
    }
    public void CambioEscena()
    {

        int sala = 10;
        SceneManager.LoadScene(sala);
    }
}*/
