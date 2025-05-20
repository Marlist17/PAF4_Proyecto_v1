using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nilo : MonoBehaviour
{
    [SerializeField] Dialogos dialog; //Accedemos a las funciones del di�logo

    private string[] lines = //Nuestras lineas
    {
        "Pst.",
        "Pst...",
        "T�, s� t�...",
        "�Quieres birlarme la caja, mu�eca?",
        "Hey, tranquila� No te voy a hacer nada. Pero no te puedo dar mi caja as� de gratis, �sabes tronca?",
        "Me llaman Nilo, de nombre Fenta. Encantado.",
        "Mira� creo que t� y yo podemos llegar a un acuerdo�",
        "Sabes a qu� me dedico, �no?",
        "Exacto. Catnip.",
        "Ya, ya� s� que no es el trabajo m�s digno, pero se necesita dinero para mantener este body� y �ltimamente ando algo justillo�",
        "Al loro, me traes un mill�n de napos y la caja es toda tuya �qu� me dices?",
        "�No?",
        "�Qu� tal mil?",
        "�Cien?",
        "�Uno?",
        "�No espera ya s�!",
        "Batalla",
        "de porros.",
        "Si te calzas un canuto antes que yo te llevas la caja.",
        "Te mola m�s esta idea eh� Si ya sab�a yo que eras de las m�as�",
        "Venga, al l�o."
    };

    private string nombreInicio = "???"; //Los nombres del personaje
    private string nombre = "Nilo";
    private bool conversacionIniciada = false; //Condici�n de ver si hemos iniciado la conversaci�n para manejar el di�logo
    private bool conversacionFinalizada = false;
    private bool Transicion = false; //Condicion para maneja rle cambio de escenas
    void Start()
    {

        dialog.FueradeRango(); //Al principio ocultamos y reseteamos todo
    }

    void Update()
    {
        if (Transicion) //Si esto es true cambiamos de escena
        {
            Invoke("TransicionBatalla", 0.4f);
        }

        dialog.PasarDialogo(); //Comprobamos si estamos pasando di�logo.
        if (conversacionIniciada) //Si el di�logo se ha activado (este en concreto): 
        {
            int indiceActual = dialog.index; //Guardamos el �ndice actual de nuestro array de frases
            string nombreActual = determinarNombreSegunIndice(indiceActual); //Almacenamos el nombre que debemos poner en determinada l�nea
            dialog.MostrarNombre(nombreActual); //Ponemos el nombre correspondiente
        }
        if (GameManager.Instance.cajaSuciaCogida && !GameManager.Instance.MinijuegoBatalla && !GameManager.Instance.tiempoCompletado) //Hemos clickado en la caja sucia sin tener ning�n otro objeto
        {
           
            if (!conversacionIniciada) //Si no hay conversaci�n iniciada:
            {
                conversacionFinalizada = dialog.ComenzarDialogo(lines, conversacionFinalizada); //Empezamos la conversaci�n
                conversacionIniciada = true; //Marcamos true la condici�n.
            }

                    

        }

    }

    public string determinarNombreSegunIndice(int indiceDialogo) //Funci�n para determinar el nombre dependiendo del �ndice
    {
        if (indiceDialogo < 6) //Si es durante estas cuantas l�neas:
            return nombreInicio; //Devolver� este nombre
        else if (indiceDialogo > 6 && indiceDialogo < 20) //durante estas tantas l�neas
        {
            
            return nombre; //devolvera este otro nombre;
        }
        else if (indiceDialogo == 20) //Si estamos en la �ltima l�nea:
        {
            Transicion = true; //Vovlemos verdadera la condici�n:
            return nombre;
        }
        else
            return nombre; //De manera predeterminada
    }

    public void TransicionBatalla() //Funci�n para cargar otra escena:
    {
        int LugarBatalla = 7;
        SceneManager.LoadScene(LugarBatalla);
    }
}
