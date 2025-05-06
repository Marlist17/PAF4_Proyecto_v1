using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tonti : MonoBehaviour
{
    [SerializeField] Dialogos dialog; //aceder a las funciones de dialogo.

   
    public string[] lines; // Nuestras frases
    public string[] respuesta_1; //Las respuestas dependiendo del objeto
    public string[] respuesta_2; //Las respuestas dependiendo del objeto
    public string[] respuesta_3; //Las respuestas dependiendo del objeto

    public string nombre;

    bool conversacionFinalizada = false;
    bool jugadorEnRango = false; // Variable para detectar si el jugador está en el trigger
    public CajasCOM.TipoCaja c; // Creamos una variable de tipo enum creada en el otro script
    bool ObjetoObtenido = false;
    void Start()
    {
        dialog.OcultarNombre();
        dialog.FueradeRango();
      
    }

   
    void Update() 
    {
        dialog.PasarDialogo();
        // Verifica si el jugador está en el área y presiona "E"
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))
        {
            if(!GameManager.Instance.CajaObtenida)
            {
                dialog.MostrarNombre(nombre);
                conversacionFinalizada = dialog.ComenzarDialogo(lines, conversacionFinalizada);
            }
            else
            {
                switch (GameManager.Instance.Caja) //Dependiendo de la enum que tenemos.
                {
                    case CajasCOM.TipoCaja.CajaNormal: //Si es normal
                        {
                            dialog.MostrarNombre(nombre);
                            dialog.DialogoOpciones(respuesta_1); //Dirá esta respuesta
                            GameManager.Instance.CajaEntregada();
                            break;
                        }
                    case CajasCOM.TipoCaja.CajaSucia:
                        {
                            dialog.MostrarNombre(nombre);
                            dialog.DialogoOpciones(respuesta_2);
                            GameManager.Instance.CajaEntregada();
                            break;
                        }
                    case CajasCOM.TipoCaja.CajaOro: // Cambié este de "CajaNormal" a "CajaOro"
                        {
                            dialog.MostrarNombre(nombre);
                            dialog.DialogoOpciones(respuesta_3);
                            GameManager.Instance.CajaEntregada();
                            break;
                        }
                }
            }
           
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
