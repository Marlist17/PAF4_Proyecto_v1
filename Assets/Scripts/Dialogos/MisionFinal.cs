using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MisionFinal : MonoBehaviour
{
    [SerializeField] Dialogos dialog; //Accedemos al script dialogos.

    private string[] Mision3 = 
    { 
        "¡¡Mi gatita preciosa, hola!!", "¿Qué tal?¿Te lo has pasado bien con el puzzle de Freud? Estoy seguro de que no...",
         "Que orgulloso estoy... que lejos has llegado, sierva mía... ", "Bueno, supongo que te has dado cuenta de que tengo a tu amiguito aquí al lado...", 
         "¡No te preocupes! Solo está inconsciente... por ahora.", "Mei.", "Posees un cuchillo entre tus zarpas.", 
         "En tu última misión, deberás mostrar lealtad eterna hacia tus reyes a través de un sacrificio.", "Sé que suena injusto, pero tu acción es necesaria para que el ciclo del reinado continúe...", 
         "Adelante, no tengas miedo y acércate. Te aseguro que no sufrirá." 
    };

    private string nombre = "Schrödinger";
    private bool dialogoActivado = false;
    bool conversacionFinalizada = false; //Comprobación de si la conversación ha finalizado
    bool primeraVez = false;
   
    void Start()
    {
        dialog.FueradeRango(); //Marcamos que estamos fuera del rango para que no se muestre el canvas

    }

    void Update()
    {

        dialog.PasarDialogo(); //Comprobamos todo el rato si el jugador pasa de diálogo   
        if (conversacionFinalizada && !dialog.DialogoActivo)
        {
            GameManager.Instance.movimiento = true;
        

        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //Si ha entrado en el trigger
    {
        if(!primeraVez)
        {
            GameManager.Instance.movimiento = false;
            // Verifica si el objeto que entra es el jugador
            if (collision.CompareTag("Player") && !dialogoActivado)
            {
                dialogoActivado = true;

                dialog.MostrarNombre(nombre); //Mostramos nombre
                conversacionFinalizada = dialog.ComenzarDialogo(Mision3, conversacionFinalizada); //Empezamos el dialogo y si ya se ha realizado una vez, llama al último dialogo

            }

            primeraVez = true;

        }
      
    }

    private void OnTriggerExit2D(Collider2D collision) //Si sale del trigger
    {
        if (collision.CompareTag("Player")) //Y es el jugador
        {
            
            dialog.FueradeRango(); //Ponemos que estamos fuera del rango
           
        }
    }
}
