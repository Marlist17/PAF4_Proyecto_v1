using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Poner esta configuracion para hacer transici�n entre escenas (Todas las escenas)


public class NiloMinijuego : MonoBehaviour
{
    [SerializeField] Dialogos dialog;

    private string[] respuesta_1 = {
        "Ugh...",
        "Joder, como fuma la condenada...",
        "Bien hecho, mu�eca. No es f�cil impresionarme, pero t� lo has conseguido con creces...",
        "Mira, me has ca�do en gracia. �Qu� te parece si te paso mi contacto? Quiz� puedo echarte una pata un d�a de estos...",
        "Eso s�, puede que a cambio te pida un poco de guita... ya sabes, para mantener el negocio a flote y tal...",
        "Hala, pilla tu caja y vete. Ha sido un placer, gatita."
    };

    private string[] respuesta_2 = {
        "Uy� casi lo consigues, cachorrita�",
        "Una pena. Si quieres otra ronda dame un toque �te hace?"
    };

    private string nombre = "Nilo";

    bool conversacionFinalizada = false;
    bool dialogoMostrado = false;

    void Start()
    {
        dialog.FueradeRango();
        GameManager.Instance.cajaSuciaCogida = false;
    }

    void Update()
    {
      
        dialog.PasarDialogo();
        // Mostrar di�logo de derrota
        if (GameManager.Instance.tiempoCompletado && !GameManager.Instance.MinijuegoBatalla && !dialogoMostrado)
        {
           
            dialog.MostrarNombre(nombre);
            conversacionFinalizada = dialog.ComenzarDialogo(respuesta_2, conversacionFinalizada);
            dialogoMostrado = conversacionFinalizada;
        }

        // Mostrar di�logo de victoria
        else if (GameManager.Instance.MinijuegoBatalla && !dialogoMostrado)
        {
            
            dialog.MostrarNombre(nombre);
            conversacionFinalizada = dialog.ComenzarDialogo(respuesta_1, conversacionFinalizada);
            dialogoMostrado = conversacionFinalizada;
        }
        if (!dialog.DialogoActivo && GameManager.Instance.MinijuegoBatalla || !dialog.DialogoActivo && GameManager.Instance.tiempoCompletado)
        {
            Invoke("CargarEscena", 0.2f);

        }
    }
    public void CargarEscena()
    {
        int callejon = 8;
        GameManager.Instance.cajaSuciaCogida = false;
        SceneManager.LoadScene(callejon); //Cargame la siguiente escena.

    }
}
