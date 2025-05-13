using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MostrarAcciones : MonoBehaviour
{
    public GameObject accionCoger;
    public GameObject accionDejar;

    private bool mostrandoCoger = false;
    private bool mostrandoDejar = false;

    void Start()
    {
        accionCoger.SetActive(false);   
        accionDejar.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.mensajeCoger && !mostrandoCoger)
        {
            StartCoroutine(MostrarYOcultarAviso(accionCoger));
            GameManager.Instance.mensajeCoger = false;
            mostrandoCoger = true;
        }
        else if (GameManager.Instance.mensajeDejar && !mostrandoDejar)
        {
            StartCoroutine(MostrarYOcultarAviso(accionDejar));
            GameManager.Instance.mensajeDejar = false;

        }
        else
        {
            GameManager.Instance.mensajeCoger = false;
            GameManager.Instance.mensajeDejar = false;
        }
    }
    
    
     IEnumerator MostrarYOcultarAviso(GameObject aviso)
    {
        Mostrar(aviso);
        yield return new WaitForSeconds(0.5f);
        Ocultar(aviso);
    }
    public void Mostrar(GameObject aviso)
    {

        aviso.SetActive(true);

    }

    public void Ocultar(GameObject aviso)
    {

        aviso.SetActive(false);

    }
}
