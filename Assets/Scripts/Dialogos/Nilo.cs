using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nilo : MonoBehaviour
{
    [SerializeField] Dialogos dialog; //Accedemos a las funciones del diálogo

    private string[] lines = //Nuestras lineas
    {
        "Pst.",
        "Pst...",
        "Tú, sí tú...",
        "¿Quieres birlarme la caja, muñeca?",
        "Hey, tranquila… No te voy a hacer nada. Pero no te puedo dar mi caja así de gratis, ¿sabes tronca?",
        "Me llaman Nilo, de nombre Fenta. Encantado.",
        "Mira… creo que tú y yo podemos llegar a un acuerdo…",
        "Sabes a qué me dedico, ¿no?",
        "Exacto. Catnip.",
        "Ya, ya… sé que no es el trabajo más digno, pero se necesita dinero para mantener este body… y últimamente ando algo justillo…",
        "Al loro, me traes un millón de napos y la caja es toda tuya ¿qué me dices?",
        "¿No?",
        "¿Qué tal mil?",
        "¿Cien?",
        "¿Uno?",
        "¡No espera ya sé!",
        "Batalla",
        "de porros.",
        "Si te calzas un canuto antes que yo te llevas la caja.",
        "Te mola más esta idea eh… Si ya sabía yo que eras de las mías…",
        "Venga, al lío."
    };

    private string nombreInicio = "???"; //Los nombres del personaje
    private string nombre = "Nilo";
    private bool conversacionIniciada = false; //Condición de ver si hemos iniciado la conversación para manejar el diálogo
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

        dialog.PasarDialogo(); //Comprobamos si estamos pasando diálogo.
        if (conversacionIniciada) //Si el diálogo se ha activado (este en concreto): 
        {
            int indiceActual = dialog.index; //Guardamos el índice actual de nuestro array de frases
            string nombreActual = determinarNombreSegunIndice(indiceActual); //Almacenamos el nombre que debemos poner en determinada línea
            dialog.MostrarNombre(nombreActual); //Ponemos el nombre correspondiente
        }
        if (GameManager.Instance.cajaSuciaCogida && !GameManager.Instance.MinijuegoBatalla && !GameManager.Instance.tiempoCompletado) //Hemos clickado en la caja sucia sin tener ningún otro objeto
        {
           
            if (!conversacionIniciada) //Si no hay conversación iniciada:
            {
                conversacionFinalizada = dialog.ComenzarDialogo(lines, conversacionFinalizada); //Empezamos la conversación
                conversacionIniciada = true; //Marcamos true la condición.
            }

                    

        }

    }

    public string determinarNombreSegunIndice(int indiceDialogo) //Función para determinar el nombre dependiendo del índice
    {
        if (indiceDialogo < 6) //Si es durante estas cuantas líneas:
            return nombreInicio; //Devolverá este nombre
        else if (indiceDialogo > 6 && indiceDialogo < 20) //durante estas tantas líneas
        {
            
            return nombre; //devolvera este otro nombre;
        }
        else if (indiceDialogo == 20) //Si estamos en la última línea:
        {
            Transicion = true; //Vovlemos verdadera la condición:
            return nombre;
        }
        else
            return nombre; //De manera predeterminada
    }

    public void TransicionBatalla() //Función para cargar otra escena:
    {
        int LugarBatalla = 7;
        SceneManager.LoadScene(LugarBatalla);
    }
}
