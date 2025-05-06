using System.Collections;
using UnityEngine;

public class TranscicionDias : MonoBehaviour
{
    public GameObject transcicion;

    void Start()
    {
        transcicion.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CamaProta") && GameManager.Instance.TutorialRealizado)
        {
            Invoke("MostrarTransicion", 0.2f);
            Invoke("QuitarTransicion", 1f);
            GameManager.Instance.TutorialRealizado = false;
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
