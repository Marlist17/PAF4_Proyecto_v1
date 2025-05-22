using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Transicion : MonoBehaviour
{
  
    public GameObject transcicion; //Hacemos referencia a la pantalla en negro
    private int escena;
    private void Awake()
    {
    }
    void Start()
    {
        transcicion.SetActive(false); //Al inicio no se muestra
        escena = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        if (GameManager.Instance.ConversacionListo && escena == 2 )
        {
            Invoke("MostrarTransicion", 0.2f); //Mostramos la transición
            GameManager.Instance.ConversacionListo = false;
        }
        if( escena == 6 && GameManager.Instance.muerteCabecilla)
        {
            MostrarTransicion(); //Mostramos la transición
            Invoke("QuitarTransicion", 1.5f); //La quitamos
            GameManager.Instance.muerteCabecilla = false;
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameManager.Instance.TutorialRealizado && !GameManager.Instance.NochePasada) //Si el jugador entra en el trigger tras realizar el tutorial, y es la priemra vez:
        {
            Invoke("MostrarTransicion", 0.2f); //Mostramos la transición
            Invoke("QuitarTransicion", 3f); //La quitamos
            GameManager.Instance.TutorialRealizado = false; //desactivamos esta condición
            GameManager.Instance.NochePasada = true; //Activamos esta otra condición
        }
    }

    void MostrarTransicion()
    {
        if (transcicion != null)
        {

            transcicion.SetActive(true); //Volvemos visible
        }
    }

    void QuitarTransicion()
    {
        if (transcicion != null)
        {
            transcicion.SetActive(false); //Volvemos invisible
        }
    }

}
