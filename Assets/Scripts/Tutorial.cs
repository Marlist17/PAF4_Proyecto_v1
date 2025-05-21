using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] Dialogos dialog;
    public string[] lines; // Nuestras frases
    private string nombre = "Mei";
    private string[] mensajeMei =
    {
        "Tengo sueño..."
    };
    [SerializeField] bool conversacionFinalizada = false;
    bool FueradeEscena = false;
    HashSet<GameObject> gameobjects;

    [SerializeField] private float delayBeforeTransition = 0.2f; // Añadido para controlar el delay del sonido
    private bool isTransitioning = false; // Añadido para controlar transiciones

    void Start()
    {
        dialog.FueradeRango();
        gameobjects = new();
    }

    void Update()
    {
        dialog.PasarDialogo();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTransitioning || gameobjects.Contains(collision.gameObject))
            return;

        gameobjects.Add(collision.gameObject);
        dialog.FueradeRango();

        if (GameManager.Instance.ObjetoObtenido)
        {
            StartCoroutine(TransitionWithSound(0)); // 0 será el índice de escena de lobby
        }
        else
        {
            if (GameManager.Instance.NochePasada)
            {
                StartCoroutine(TransitionWithSound_2(0)); // 0 será el índice de escena de lobby
            }
            else if (GameManager.Instance.TutorialRealizado)
            {
                dialog.MostrarNombre(nombre);
                conversacionFinalizada = dialog.ComenzarDialogo(mensajeMei, conversacionFinalizada);
                return;
            }
            else
            {
                conversacionFinalizada = dialog.ComenzarDialogo(lines, conversacionFinalizada);
            }
        }
    }

    private IEnumerator TransitionWithSound(int sceneIndex)
    {
        isTransitioning = true;

        // Reproducir sonido de puerta
        AudioManager.Instance.PlayDoorSound();

        // Pequeña espera antes de cambiar de escena
        yield return new WaitForSeconds(delayBeforeTransition);

        // Ejecutar transición
        GameManager.Instance.TransicionLobby();
        FueradeEscena = true;

        isTransitioning = false;
    }

    private IEnumerator TransitionWithSound_2(int sceneIndex)
    {
        isTransitioning = true;

        // Reproducir sonido de puerta
        AudioManager.Instance.PlayDoorSound();

        // Pequeña espera antes de cambiar de escena
        yield return new WaitForSeconds(delayBeforeTransition);

        // Ejecutar transición
        GameManager.Instance.TransicionLobby_2();
        FueradeEscena = true;

        isTransitioning = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gameobjects.Remove(collision.gameObject);
        if (!FueradeEscena)
            dialog.FueradeRango();
    }
}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] Dialogos dialog;
    public string[] lines; // Nuestras frases
    private string nombre = "Mei";
    private string[] mensajeMei =
    {
        "Tengo sueño..."
    };
    [SerializeField] bool conversacionFinalizada = false;
    bool FueradeEscena = false;
    HashSet<GameObject> gameobjects;
    void Start()
    {
        dialog.FueradeRango();
        gameobjects = new();
    }

    void Update()
    {
        dialog.PasarDialogo();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameobjects.Contains(collision.gameObject))
            return;
       gameobjects.Add(collision.gameObject);
        dialog.FueradeRango();

        if (GameManager.Instance.ObjetoObtenido) //Si esta variable es verdadera (solo cuando se ha cogido el objeto)
        {
            GameManager.Instance.TransicionLobby();
            FueradeEscena = true;
          
        }
        else
        {
          
                
            if (GameManager.Instance.NochePasada)
            {
                GameManager.Instance.TransicionLobby_2();
                FueradeEscena = true;
                return; //detiene la ejecución siguiente para poder transicionar al lobby
            }
            else if (GameManager.Instance.TutorialRealizado)
            {
                dialog.MostrarNombre(nombre);
                conversacionFinalizada = dialog.ComenzarDialogo(mensajeMei, conversacionFinalizada);
                return; //detiene la ejecución siguiente para poder transicionar al lobby
            }
            else
                conversacionFinalizada = dialog.ComenzarDialogo(lines, conversacionFinalizada);
           
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {  
        gameobjects.Remove(collision.gameObject);
        if(!FueradeEscena)
            dialog.FueradeRango();

    }

}*/
