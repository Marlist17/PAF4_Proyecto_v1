using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Poner esta configuracion para hacer transición entre escenas (Todas las escenas)

public class TransicionCasas : MonoBehaviour
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

            int casa = 1;
            SceneManager.LoadScene(casa); //Cargame la siguiente escena.
        }
        else if (collision.gameObject.tag == "CasaRico")
        {
            GameManager.Instance.TransicionLobby();
        }
        else if (collision.gameObject.tag == "ExteriorCasaRico" && GameManager.Instance.NochePasada)
        {
            int casa = 5;
            SceneManager.LoadScene(casa); //Cargame la siguiente escena.
        }
    }
}