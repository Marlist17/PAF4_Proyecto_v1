
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Poner esta configuracion para hacer transici�n entre escenas (Todas las escenas)


public class NPCCOM : MonoBehaviour
{
    [SerializeField] Dialogos dialog; //Accedemos al script dialogos.
    public string[] lines; // Nuestras frases
    public string nombre; //EL nombre del NPC
    public GameObject icono; //Se�al de que puedes hablar con �l
    bool conversacionFinalizada = false; //Comprobaci�n de si la conversaci�n ha finalizado
    bool jugadorEnRango = false; // Variable para detectar si el jugador est� en el trigger
    private int escenaActual; //Comprobamos escena en la que estamos

    void Start()
    {
        Debug.Log("Nombre en Start(): " + nombre);
        dialog.FueradeRango(); //Marcamos que estamos fuera del rango para que no se muestre el canvas
        escenaActual = SceneManager.GetActiveScene().buildIndex; //Miramos en que escena estamos
        icono.SetActive(false); //Volvemos false el icono
    }

    void Update()
    {
       
        dialog.PasarDialogo(); //Comprobamos todo el rato si el jugador pasa de di�logo

        // Verifica si el jugador est� en el �rea y presiona "E"
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))
        {
           
            dialog.LimpiarDialogos();
            if (nombre == "Ryo" && escenaActual == 1) //Si hemos hablado con ryo:
            {
                GameManager.Instance.HablarNPC = true; //Se volver� true la condici�n
            }
            icono.SetActive(false); //Desaparece el icono
            if(escenaActual == 9 || escenaActual == 6) //Si la escena es aquella en la que si o si es necesario hablar con el NPC para interactuar con el objeto:
            {
                dialog.LimpiarDialogos(); //Pos si acaso limpiamos posibles di�logos

            }
            
            Debug.Log(nombre);
            dialog.MostrarNombre(nombre); //Mostramos nombre
            conversacionFinalizada = dialog.ComenzarDialogo(lines, conversacionFinalizada); //Empezamos el dialogo y si ya se ha realizado una vez, llama al �ltimo dialogo
        }

        if (!dialog.DialogoActivo && conversacionFinalizada && (escenaActual == 6 || escenaActual == 9))
        {
            if (escenaActual == 9 && nombre == "Ryo")
            {
                GameManager.Instance.HablarNPC = true;
            }
            else if (escenaActual == 6)
                GameManager.Instance.HablarNPC = true;
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
            dialog.FueradeRango(); //Ponemos que estamos fuera del rango
            icono.SetActive(false); //Y vuelve invisible el icono
        }
    }
}

