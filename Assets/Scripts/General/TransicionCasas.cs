using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; //Poner esta configuracion para hacer transición entre escenas (Todas las escenas)



public class TransicionCasas : MonoBehaviour
{
    [SerializeField] private float delay = 0.2f; // Ajusta este valor para hacerlo más rápido
    private bool isTransitioning = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTransitioning) return;

        if (collision.gameObject.tag == "CasaProtaExterior" && GameManager.Instance.TutorialRealizado)
        {
            StartCoroutine(TransicionConSonido(4));
        }
        else if (collision.gameObject.tag == "Callejon" && GameManager.Instance.ConversacionTonti)
        {
            GameManager.Instance.lugar = "CallejonInterior"; //Guardamos en que lugar vamos a parar 
            SceneManager.LoadScene(8);                       // Para poner unas coordenadas concretas 
        }                                                    //Quedándo en la entrada o salida
        else if (collision.gameObject.tag == "CallejonInterior")
        {
            GameManager.Instance.lugar = "Callejon";
            GameManager.Instance.TransicionLobby_2();
        }
        else if (collision.gameObject.tag == "CasaFrikisExterior" && GameManager.Instance.ConversacionTonti)
        {
            StartCoroutine(TransicionConSonido(9));
        }
        else if (collision.gameObject.tag == "CasaFrikis")
        {
            GameManager.Instance.lugar = "CasaFrikis";
            StartCoroutine(TransicionConSonido(9));
            GameManager.Instance.TransicionLobby_2();
        }
        else if (collision.gameObject.tag == "CasaRico")
        {
            GameManager.Instance.lugar = "CasaRico";
            StartCoroutine(TransicionConSonido(6));
            GameManager.Instance.TransicionLobby_2();
        }
        else if (collision.gameObject.tag == "ExteriorCasaRico" && GameManager.Instance.ConversacionTonti)
        {
            int casa = 6;
            SceneManager.LoadScene(casa); //Cargame la siguiente escena.
            StartCoroutine(TransicionConSonido(6));
        }
    }

    private IEnumerator TransicionConSonido(int sceneIndex)
    {
        isTransitioning = true;

        // Reproducir sonido de puerta
        AudioManager.Instance.PlayDoorSound();

        // Pequeña espera antes de cambiar de escena
        yield return new WaitForSeconds(delay);

        // Cargar escena mientras el sonido sigue reproduciéndose
        SceneManager.LoadScene(sceneIndex);

        // No necesitamos resetear isTransitioning porque se destruye este objeto al cargar la nueva escena
    }
}
