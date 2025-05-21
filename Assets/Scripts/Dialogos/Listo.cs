using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Listo : MonoBehaviour
{
    [Header("Configuraci�n Di�logo")]
    [SerializeField] Dialogos dialog;
    private string[] InicioMision2 =
    {
        "�Keaton me acaba de llamar enfermo?", "...", "En fin.", "La segunda misi�n tendr� lugar en un sitio un poco... distinto.",
        "Te prometo que no doler� mucho."
    };

    [Header("Configuraci�n Transici�n")]
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

            // Cambiamos a la versi�n con sonido
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

    // Nuevo m�todo para manejar el sonido y el cambio de escena
    private void ReproducirSonidoYCambiarEscena()
    {
        // Reproducir sonido de golpe
        if (sonidoGolpe != null)
        {
            AudioManager.Instance.PlaySound(sonidoGolpe);
        }

        // Cambiar de escena despu�s de un breve delay
        Invoke("CambioEscena", 1.2f); // Ajusta este valor seg�n necesites
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




/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Listo : MonoBehaviour
{
    [SerializeField] Dialogos dialog; //Accedemos al script dialogos.
    private string[] InicioMision2 =
    {
        "�Keaton me acaba de llamar enfermo?", "...", "En fin.", "La segunda misi�n tendr� lugar en un sitio un poco... distinto.",
        "Te prometo que no doler� mucho." 
    };

    private string nombre = "Freud"; //EL nombre del NPC
    public GameObject icono; //Se�al de que puedes hablar con �l
    bool conversacionFinalizada = false; //Comprobaci�n de si la conversaci�n ha finalizado
    bool jugadorEnRango = false; // Variable para detectar si el jugador est� en el trigger
    bool transicion = false;
    void Start()
    {
        dialog.FueradeRango(); //Marcamos que estamos fuera del rango para que no se muestre el canvas
        icono.SetActive(false); //Volvemos false el icono
    }

    void Update()
    {

        dialog.PasarDialogo(); //Comprobamos todo el rato si el jugador pasa de di�logo
         if (conversacionFinalizada && dialog.DialogoActivo == false && !transicion)
        {
            GameManager.Instance.ConversacionListo = true;
            transicion = true;
            GameManager.Instance.movimiento = true;
            Invoke("CambioEscena", 1.2f);
           
        }
        // Verifica si el jugador est� en el �rea y presiona "E"
        else if (jugadorEnRango && Input.GetKeyDown(KeyCode.E) && GameManager.Instance.Mision_1)
        {
            GameManager.Instance.movimiento = false;
            icono.SetActive(false); //Desaparece el icono
            dialog.MostrarNombre(nombre); //Mostramos nombre
            conversacionFinalizada = dialog.ComenzarDialogo(InicioMision2, conversacionFinalizada); //Empezamos el dialogo y si ya se ha realizado una vez, llama al �ltimo dialogo
        }
        
        
           
        
    }

    private void OnTriggerEnter2D(Collider2D collision) //Si ha entrado en el trigger
    {

        // Verifica si el objeto que entra es el jugador
        if (collision.CompareTag("Player") && GameManager.Instance.Mision_1)
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
            if (dialog != null)
            {
                dialog.FueradeRango();
            }
            if (icono != null)
            {
                icono.SetActive(false);
            }
        }
    }

    public void CambioEscena()
    {
            int sala = 6;
            SceneManager.LoadScene(sala); //Cargame la siguiente escena.
    }
}*/
