using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Poner esta configuracion para hacer transición entre escenas (Todas las escenas)

public class TransicionLobby : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision) //Se acceda a un sitio se activa)
    {
        
       
            Invoke("cargarEscena", 1f); //Llama a la funcion despues de 2 segundos. (Para que haya espera al cambiar de escena)
        
      

    }
    void cargarEscena()
    {
        int escenaActual = SceneManager.GetActiveScene().buildIndex; //secen ,amager accedes a la opcion de file (build settings) --> y busca el indice
        int lobby = 1;
        SceneManager.LoadScene(lobby); //Cargame la siguiente escena.
    }

}

