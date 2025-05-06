using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Poner esta configuracion para hacer transición entre escenas (Todas las escenas)

public class ControlCasaProta : MonoBehaviour
{
    public GameObject sardina;
    public GameObject transicion;
    public GameObject aviso1;
    public GameObject aviso2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int escenaActual = SceneManager.GetActiveScene().buildIndex; //secen ,amager accedes a la opcion de file (build settings) --> y busca el indice
        if (escenaActual == 1 && GameManager.Instance.TutorialRealizado)
        {
            Destroy(sardina);
            Destroy(transicion);
            Destroy(aviso1);
            Destroy(aviso2);

        }
    }
}
