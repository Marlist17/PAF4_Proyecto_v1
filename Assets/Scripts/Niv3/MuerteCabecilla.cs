using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MuerteCabecilla : MonoBehaviour
{
    
    public GameObject icono; //Se�al de que puedes hablar con �l
    bool jugadorEnRango = false; // Variable para detectar si el jugador est� en el trigger
    AudioClip sonidoMuerte;
    [SerializeField] GameObject transicion; // Asigna el clip de audio en el Inspector

    [SerializeField] private float delaySonido = 0.5f; // Tiempo antes del sonido
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
            ReproducirSonidoYCambiarEscena(); //Llamamos a la funci�n de espera
            //AudioManager.Instance.PlaySound(sonidoMuerte);
            //Escena(); //Llamamos a la funci�n de espera
            //Escena(); //Llamamos a la funci�n de espera

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

    private void ReproducirSonidoYCambiarEscena()
    {
        transicion.SetActive(true); // Activa la transici�n
        // Reproducir sonido de golpe
        if (sonidoMuerte != null)
        {
            AudioManager.Instance.PlayDeath();
        }

        // Cambiar de escena despu�s de un breve delay
        CambioEscena();
    }
    public void CambioEscena()
    {
        EsperarYEjecutar(); //Llamamos a la funci�n de espera
        int sala = 10;
        SceneManager.LoadScene(sala);
    }
    IEnumerator EsperarYEjecutar()
    {
        Debug.Log("Inicio de la espera");

        yield return new WaitForSeconds(4f); // Espera 2.5 segundos

        Debug.Log("Pasaron 2.5 segundos");

    }

    /*public void Escena()
    {
        EsperarYEjecutar(); //Llamamos a la funci�n de espera
        SceneManager.LoadScene(10); // Cambia a la escena deseada


    }
    IEnumerator EsperarYEjecutar()
    {


        yield return new WaitForSeconds(3f); // Espera 2.5 segundos
        SceneManager.LoadScene(10);


    }*/


}
