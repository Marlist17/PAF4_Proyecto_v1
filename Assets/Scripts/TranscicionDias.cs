using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement; //Poner esta configuracion para hacer transición entre escenas (Todas las escenas)


public class TranscicionDias : MonoBehaviour
{
    public GameObject transcicion;

    void Start()
    {
        transcicion.SetActive(false);
    }

    void Update()
    {
        


    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CamaProta") && GameManager.Instance.TutorialRealizado)
        {
            Invoke("MostrarTransicion", 0.2f);
            Invoke("QuitarTransicion", 1f);
            GameManager.Instance.TutorialRealizado = false;
            GameManager.Instance.NochePasada = true;
        }
    }

    void MostrarTransicion()
    {
        if (transcicion != null)
        {

            transcicion.SetActive(true);
        }
    }

    void QuitarTransicion()
    {
        if (transcicion != null)
        {
            transcicion.SetActive(false);
        }
    }
}
