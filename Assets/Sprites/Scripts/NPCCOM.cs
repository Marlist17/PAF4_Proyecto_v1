
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Poner esta configuracion para hacer transición entre escenas (Todas las escenas)


public class NPCCOM : MonoBehaviour
{
    [SerializeField] Dialogos dialog;
    public string[] lines; // Nuestras frases
    public string nombre;
    public int indice;
    bool conversacionFinalizada = false;
    bool jugadorEnRango = false; // Variable para detectar si el jugador está en el trigger
    public int escenaActual;

    void Start()
    {
        dialog.FueradeRango();
        indice = 0;
        escenaActual = SceneManager.GetActiveScene().buildIndex;
        
    }

    void Update()
    {
       
        dialog.PasarDialogo();

        // Verifica si el jugador está en el área y presiona "E"
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))
        {
            if(escenaActual == 3)
            {
                GameManager.Instance.HablarNPC = true;
                dialog.LimpiarDialogos();

            }
            dialog.MostrarNombre(nombre);
            conversacionFinalizada = dialog.ComenzarDialogo(lines, conversacionFinalizada);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        // Verifica si el objeto que entra es el jugador
        if (collision.CompareTag("Player"))
        {
            jugadorEnRango = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorEnRango = false;
            dialog.FueradeRango();
        }
    }
}

