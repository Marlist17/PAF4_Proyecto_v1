using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MuerteCabecilla : MonoBehaviour
{

   
    public GameObject icono; //Señal de que puedes hablar con él
    bool jugadorEnRango = false; // Variable para detectar si el jugador está en el trigger
  
    void Start()
    {
        icono.SetActive(false); //Volvemos false el icono
    }

    void Update()
    {

      

        // Verifica si el jugador está en el área y presiona "E"
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.muerteCabecilla = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //Si ha entrado en el trigger
    {

        // Verifica si el objeto que entra es el jugador
        if (collision.CompareTag("Player"))
        {
            jugadorEnRango = true; //Marca que si está en rango
            icono.SetActive(true); //Y vuelve visible el icono
        }
    }

    private void OnTriggerExit2D(Collider2D collision) //Si sale del trigger
    {
        if (collision.CompareTag("Player")) //Y es el jugador
        {
            jugadorEnRango = false; //Volvemos falsa la condición
            icono.SetActive(false); //Y vuelve invisible el icono
        }
    }
}
