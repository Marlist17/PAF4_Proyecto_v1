using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MuerteCabecilla : MonoBehaviour
{

    [SerializeField] GameObject transicion; //Variable para la transici�n de escena
    public GameObject icono; //Se�al de que puedes hablar con �l
    bool jugadorEnRango = false; // Variable para detectar si el jugador est� en el trigger
    
    void Start()
    {
        icono.SetActive(false); //Volvemos false el icono
        
    }

    void Update()
    {

      

        // Verifica si el jugador est� en el �rea y presiona "E"
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.muerteCabecilla = true;
        }

        if (GameManager.Instance.muerteCabecilla == true) 
        {
            transicion.SetActive(true); //Activamos la transici�n
            EsperaEscena(); //Llamamos a la funci�n de espera



        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //Si ha entrado en el trigger
    {

        // Verifica si el objeto que entra es el jugador
        if (collision.CompareTag("Player"))
        {
            jugadorEnRango = true; //Marca que si est� en rango
            icono.SetActive(true); //Y vuelve visible el icono
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //Si sale del trigger
    {
        if (collision.CompareTag("Player")) //Y es el jugador
        {
            jugadorEnRango = false; //Volvemos falsa la condici�n
            icono.SetActive(false); //Y vuelve invisible el icono
        }
    }
    public void EsperaEscena()
    {
        Invoke("CambiarEscena", 2f);


    }
    private void CambiarEscena()
    {
        SceneManager.LoadScene(10); // Cambia a la escena deseada
    }
}
