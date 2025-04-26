using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Poner esta configuracion para hacer transición entre escenas (Todas las escenas)

public class Inventario : MonoBehaviour
{
    public static Inventario instancia;

    public GameObject cajaNormal;
    public GameObject cajaSucia;
    public GameObject cajaOro;
    private GameObject objeto;
    void Awake()
    {

        // Verifica si ya hay una instancia
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            // Si ya hay una instancia, destruye esta para evitar duplicados
            Destroy(this.gameObject);
        }


    }
    void Start()
    {
        cajaNormal.SetActive(false);
        cajaOro.SetActive(false);
        cajaSucia.SetActive(false);
    }

    public void MeterObjetoInventario(GameObject objetoInteractivo) //Función para almacenar que objeto hemos clickado
    {
        objeto = objetoInteractivo;

        if (objeto.CompareTag("CajaNormal"))
        {
            cajaNormal.SetActive(true);
        }
        else if (objeto.CompareTag("CajaSucia"))
        {
            cajaSucia.SetActive(true);
        }
        else if (objeto.CompareTag("CajaOro"))
        {
            cajaOro.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int escenaActual = SceneManager.GetActiveScene().buildIndex;

        if (escenaActual == 4)
        {
            instancia.gameObject.SetActive(false);
        }
    }
 
}
