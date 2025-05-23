using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MCabecilla : MonoBehaviour
{
    public GameObject icono; // Señal de que puedes hablar con él
    bool jugadorEnRango = false;
    [SerializeField] AudioClip sonidoMuerte;
    [SerializeField] GameObject transicion;
    [SerializeField] private float delaySonido = 0.001f;

    // Variable para controlar si ya se activó la secuencia de muerte
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
        transicion.SetActive(true); // Activar la transición
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

