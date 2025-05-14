using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tonti : MonoBehaviour
{
    [SerializeField] Dialogos dialog; //aceder a las funciones de dialogo.

   
    private string[] mision1 =
    {
         "���Holi!!!", "Vaya amargado el Freud eh... menos mal que estoy yo aqu� para darle vidilla al asunto.",
         "�Bueno!�misi�n!", "Em... A ver... qu� te puedo pedir yo a ti...", "�Ah, ya s�!", "Gata, esc�chame atentamente...", "...",
         "Necesito... que me traigas...", "LA MEJOR CAJA DE TODO EL REINO.", "Ya est�, venga vete."
    };
    
    private string[] cajaNormal =
    {
        "�AY A VER LA CAJA!","...", "Mmm...", "Gata.", "Qu� es lo que no has entendido de \"la mejor caja de todo el reino\".",
        "Creo que es la caja m�s mediocre que he visto nunca.", "Y te aseguro que he visto muuuchas cajas...",
        "Vete y no vuelvas hasta que me traigas la caja perfecta."
    };
    
    private string[] cajaDorada =
    {
        "�GATA!", "QU� CO�O ES ESTO", "Ugh... creo que voy a vomitar...", "�De d�nde has sacado esa atrocidad...?",
        "Mira es que no quiero ni saberlo...", "Es tan limpia... y tan brillante y probablemente tan cara y valiosa...", "Realmente repulsivo...",
        "Saca esto de mi vista, por Dios...", "Que asco... vete a por otra caja. PERO VETE."
    };

   private string[] cajaSucia =
   {
        "...", "...", "Gata...", "Es...", "���ES PERFECTA!!!", "Oh cajita... d�nde has estado toda mi vida...", 
        "*muac* *slurp slurp* ah... *slurp* *muac*", "Ay...", "Perd�n me he dejado llevar un poco...", "Ejem.", "�Enhorabuena gata!�has superado la primera misi�n!", 
        "Ahora tienes que hablar con Freud. Creo que �l te va a dar la segunda...", "Ten cuidad�n, a saber que te manda ese enfermo..."};

private string nombre = "Keaton";
    public GameObject icono;

    bool conversacionFinalizada = false;
    bool jugadorEnRango = false; // Variable para detectar si el jugador est� en el trigger
    public CajasCOM.TipoCaja c; // Creamos una variable de tipo enum creada en el otro script
  
    void Start()
    {
        dialog.OcultarNombre();
        dialog.FueradeRango();
        icono.SetActive(false);
      
    }

   
    void Update() 
    {
        dialog.PasarDialogo();
        // Verifica si el jugador est� en el �rea y presiona "E"
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))
        {
            icono.SetActive(false);
            if (!GameManager.Instance.CajaObtenida && GameManager.Instance.ConversacionCabecilla)
            {
                dialog.MostrarNombre(nombre);
                conversacionFinalizada = dialog.ComenzarDialogo(mision1, conversacionFinalizada);
                GameManager.Instance.ConversacionTonti = true;
            }
            else if (!GameManager.Instance.Mision_1 && GameManager.Instance.CajaObtenida)
            {
                conversacionFinalizada = false;
                switch (GameManager.Instance.Caja) //Dependiendo de la enum que tenemos.
                {  
                    case CajasCOM.TipoCaja.CajaNormal: //Si es normal
                        {
                            GameObject objetoActivo = Inventario.instancia.ObtenerObjetoActivo();
                            GameManager.Instance.mensajeDejar = true; //Convertimos en true la variable MensajeDejar para usarlo en otro script y mostrar el mensaje por pantalla
                            dialog.MostrarNombre(nombre);
                            conversacionFinalizada = dialog.ComenzarDialogo(cajaNormal, conversacionFinalizada);
                            GameManager.Instance.CajaEntregada();
                            objetoActivo.SetActive(false);

                            break;
                        }
                    case CajasCOM.TipoCaja.CajaSucia:
                        {
                            GameObject objetoActivo = Inventario.instancia.ObtenerObjetoActivo();
                            GameManager.Instance.mensajeDejar = true; //Convertimos en true la variable MensajeDejar para usarlo en otro script y mostrar el mensaje por pantalla
                            dialog.MostrarNombre(nombre);
                            conversacionFinalizada = dialog.ComenzarDialogo(cajaSucia, conversacionFinalizada);
                            GameManager.Instance.CajaEntregada();
                            GameManager.Instance.Mision_1 = true;
                            objetoActivo.SetActive(false);
                            break;
                        }
                    case CajasCOM.TipoCaja.CajaOro: // Cambi� este de "CajaNormal" a "CajaOro"

                        {
                            GameObject objetoActivo = Inventario.instancia.ObtenerObjetoActivo();
                            GameManager.Instance.mensajeDejar = true; //Convertimos en true la variable MensajeDejar para usarlo en otro script y mostrar el mensaje por pantalla
                            dialog.MostrarNombre(nombre);
                            conversacionFinalizada = dialog.ComenzarDialogo(cajaDorada, conversacionFinalizada);
                            GameManager.Instance.CajaEntregada();
                            objetoActivo.SetActive(false);
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
            if (GameManager.Instance.Mision_1)
            {

                icono.SetActive(false);
            }
            else if(GameManager.Instance.ConversacionCabecilla)
            icono.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorEnRango = false;
            dialog.FueradeRango();
            icono.SetActive(false);
        }
    }
}
