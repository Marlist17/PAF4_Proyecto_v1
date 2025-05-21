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
            StartCoroutine(TransicionConSonido(3));
        }
        else if (collision.gameObject.tag == "Callejon" && GameManager.Instance.ConversacionTonti)
        {
            GameManager.Instance.lugar = "CallejonInterior";
            SceneManager.LoadScene(8);
        }
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
            StartCoroutine(TransicionConSonido(9));
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
/*public class TransicionCasas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CasaProtaExterior" && GameManager.Instance.TutorialRealizado)
        {

            int casa = 3;
            SceneManager.LoadScene(casa); //Cargame la siguiente escena.
        }
        else if (collision.gameObject.tag == "Callejon" && GameManager.Instance.ConversacionTonti)
        {

            int sala = 8;
            SceneManager.LoadScene(sala); //Cargame la siguiente escena.
        }
        else if (collision.gameObject.tag == "CallejonInterior")
        {
            GameManager.Instance.lugar = "Callejon";
            GameManager.Instance.TransicionLobby_2();

        }
        else if (collision.gameObject.tag == "CasaFrikisExterior" && GameManager.Instance.ConversacionTonti)
        {

            int sala = 10;
            SceneManager.LoadScene(sala); //Cargame la siguiente escena.
        }
        else if (collision.gameObject.tag == "CasaFrikis")
        {

            GameManager.Instance.lugar = "CasaFrikis";
            GameManager.Instance.TransicionLobby_2();
        }
        else if (collision.gameObject.tag == "CasaRico")
        {
            GameManager.Instance.lugar = "CasaRico";
            GameManager.Instance.TransicionLobby_2();
        }
        else if (collision.gameObject.tag == "ExteriorCasaRico" && GameManager.Instance.ConversacionTonti)
        {
            int casa = 7;
            SceneManager.LoadScene(casa); //Cargame la siguiente escena.
        }
    }
}*/