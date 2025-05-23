using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NiloDerrota : MonoBehaviour
{
    [SerializeField] Dialogos dialog; //Accedemos al script dialogos.
    private string[] lines =
    {
       " Qu�, �lista para otro canutillo?"
    };// Nuestras frases
    public string nombre; //EL nombre del NPC
    public GameObject icono; //Se�al de que puedes hablar con �l
    bool conversacionFinalizada = false; //Comprobaci�n de si la conversaci�n ha finalizado
    bool jugadorEnRango = false; // Variable para detectar si el jugador est� en el trigger
  

    void Start()
    {
       
        dialog.FueradeRango(); //Marcamos que estamos fuera del rango para que no se muestre el canvas
        icono.SetActive(false); //Volvemos false el icono
    }

    void Update()
    {

        dialog.PasarDialogo(); //Comprobamos todo el rato si el jugador pasa de di�logo

        // Verifica si el jugador est� en el �rea y presiona "E"
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E) && GameManager.Instance.tiempoCompletado)
        {
            
            icono.SetActive(false); //Desaparece el icono
            dialog.MostrarNombre(nombre); //Mostramos nombre
            conversacionFinalizada = dialog.ComenzarDialogo(lines, conversacionFinalizada); //Empezamos el dialogo y si ya se ha realizado una vez, llama al �ltimo dialogo

        }
        if (conversacionFinalizada && dialog.DialogoActivo == false)
        {
            VolverMinijuego();
            conversacionFinalizada = false; // Para que no se repita cada frame
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //Si ha entrado en el trigger
    {

        // Verifica si el objeto que entra es el jugador
        if (collision.CompareTag("Player"))
        {
            if (!GameManager.Instance.MinijuegoBatalla && GameManager.Instance.tiempoCompletado)
            {
                jugadorEnRango = true; //Marca que si est� en rango
                icono.SetActive(true); //Y vuelve visible el icono
            }
           
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

    public void VolverMinijuego()
    {
        int LugarBatalla = 7;
        SceneManager.LoadScene(LugarBatalla);
    }
}
