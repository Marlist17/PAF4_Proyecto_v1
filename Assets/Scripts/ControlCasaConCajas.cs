using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Poner esta configuracion para hacer transición entre escenas (Todas las escenas)


public class ControlCasaConCajas : MonoBehaviour
{
    [SerializeField] GameObject caja;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int escenaActual = SceneManager.GetActiveScene().buildIndex; //Miramos en que escena estamos
        if (escenaActual == 5 && GameManager.Instance.cajaSuciaCogida) //Si volvemos ha casa tras haber cogido la ofrenda:
        {
            Destroy(caja); //Lo destruimos para que no vuelva a aparecer y no lo veamos
           

        }

    }
}
