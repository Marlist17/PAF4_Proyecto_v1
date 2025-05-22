using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Listo : MonoBehaviour
{
    [Header("Configuración Diálogo")]
    [SerializeField] Dialogos dialog;
    private string[] InicioMision2 =
    {
        "¿Keaton me acaba de llamar enfermo?", "...", "En fin.", "La segunda misión tendrá lugar en un sitio un poco... distinto.",
        "Te prometo que no dolerá mucho."
    };

    [Header("Configuración Transición")]
    public AudioClip sonidoGolpe; // Asignar desde el Inspector
    [SerializeField] private float delaySonido = 0.5f; // Tiempo antes del sonido

    private string nombre = "Freud";
    public GameObject icono;
    bool conversacionFinalizada = false;
    bool jugadorEnRango = false;
    bool transicion = false;

    void Start()
    {
        dialog.FueradeRango();
        icono.SetActive(false);
    }

    void Update()
    {
        dialog.PasarDialogo();

        if (conversacionFinalizada && dialog.DialogoActivo == false && !transicion)
        {
            GameManager.Instance.ConversacionListo = true;
            transicion = true;
            GameManager.Instance.movimiento = true;

            // Cambiamos a la versión con sonido
            Invoke("ReproducirSonidoYCambiarEscena", delaySonido);
        }
        else if (jugadorEnRango && Input.GetKeyDown(KeyCode.E) && GameManager.Instance.Mision_1)
        {
            GameManager.Instance.movimiento = false;
            icono.SetActive(false);
            dialog.MostrarNombre(nombre);
            conversacionFinalizada = dialog.ComenzarDialogo(InicioMision2, conversacionFinalizada);
        }
    }

    // Nuevo método para manejar el sonido y el cambio de escena
    private void ReproducirSonidoYCambiarEscena()
    {
        // Reproducir sonido de golpe
        if (sonidoGolpe != null)
        {
            AudioManager.Instance.PlaySound(sonidoGolpe);
        }

        // Cambiar de escena después de un breve delay
        Invoke("CambioEscena", 1.2f); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameManager.Instance.Mision_1)
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
            if (dialog != null) dialog.FueradeRango();
            if (icono != null) icono.SetActive(false);
        }
    }

    public void CambioEscena()
    {
        int sala = 5;
        SceneManager.LoadScene(sala);
    }
}




