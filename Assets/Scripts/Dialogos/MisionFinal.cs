using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MisionFinal : MonoBehaviour
{
    [SerializeField] Dialogos dialog; //Accedemos al script dialogos.

    private string[] Mision3 = 
    { 
        "��Mi gatita preciosa, hola!!", "�Qu� tal?�Te lo has pasado bien con el puzzle de Freud? Estoy seguro de que no...",
         "Que orgulloso estoy... que lejos has llegado, sierva m�a... ", "Bueno, supongo que te has dado cuenta de que tengo a tu amiguito aqu� al lado...", 
         "�No te preocupes! Solo est� inconsciente... por ahora.", "Mei.", "Posees un cuchillo entre tus zarpas.", 
         "En tu �ltima misi�n, deber�s mostrar lealtad eterna hacia tus reyes a trav�s de un sacrificio.", "S� que suena injusto, pero tu acci�n es necesaria para que el ciclo del reinado contin�e...", 
         "Adelante, no tengas miedo y ac�rcate. Te aseguro que no sufrir�." 
    };

    private string nombre = "Schr�dinger";
    private bool dialogoActivado = false;
    bool conversacionFinalizada = false; //Comprobaci�n de si la conversaci�n ha finalizado
    bool primeraVez = false;
   
    void Start()
    {
        dialog.FueradeRango(); //Marcamos que estamos fuera del rango para que no se muestre el canvas

    }

    void Update()
    {

        dialog.PasarDialogo(); //Comprobamos todo el rato si el jugador pasa de di�logo   
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
                conversacionFinalizada = dialog.ComenzarDialogo(Mision3, conversacionFinalizada); //Empezamos el dialogo y si ya se ha realizado una vez, llama al �ltimo dialogo

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
