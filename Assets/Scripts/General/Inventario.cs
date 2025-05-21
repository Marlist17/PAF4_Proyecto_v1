using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Poner esta configuracion para hacer transición entre escenas (Todas las escenas)


public class Inventario : MonoBehaviour
{
    public static Inventario instancia;

    public GameObject cajaNormal;
    public GameObject cajaSucia;
    public GameObject cajaOro;
    public GameObject sardina;
    public GameObject cuchillo;

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
        sardina.SetActive(false);
        cuchillo.SetActive(false);
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
        else if (objeto.CompareTag("Sardina"))
        {
            sardina.SetActive(true);
        }
        else if (objeto.CompareTag("Cuchillo"))
        {
            cuchillo.SetActive(true);
        }
    }
    // Esta función te da el objeto visible en el inventario
    public GameObject ObtenerObjetoActivo()
    {
        if (cajaNormal.activeSelf) return cajaNormal;
        if (cajaSucia.activeSelf) return cajaSucia;
        if (cajaOro.activeSelf) return cajaOro;
        if (sardina.activeSelf) return sardina;
        if (cuchillo.activeSelf) return cuchillo;
        return null;
    }



    // Update is called once per frame
    void Update()
    {
       
    }

}
