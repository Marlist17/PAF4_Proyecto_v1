using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Poner esta configuracion para hacer transición entre escenas (Todas las escenas)

public class TransicionCasas : MonoBehaviour
{
    public AudioClip miClip;
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
        else if (collision.gameObject.tag == "CasaRico")
        {
            AudioManager.Instance.PlaySound(miClip);
            GameManager.Instance.lugar = "CasaRico";
            GameManager.Instance.TransicionLobby_2();
        }
        else if (collision.gameObject.tag == "ExteriorCasaRico" && GameManager.Instance.ConversacionTonti)
        {
            int casa = 7;
            SceneManager.LoadScene(casa); //Cargame la siguiente escena.
        }
     

    }

}


