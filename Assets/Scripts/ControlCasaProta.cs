using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Poner esta configuracion para hacer transición entre escenas (Todas las escenas)

public class ControlCasaProta : MonoBehaviour
{
    public GameObject sardina; //recibimos el prop presente
    public GameObject transicion; //recibimos el objeto de la transicion
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int escenaActual = SceneManager.GetActiveScene().buildIndex; //Miramos en que escena estamos
        if (escenaActual == 1 && GameManager.Instance.TutorialRealizado) //Si volvemos ha casa tras haber cogido la ofrenda:
        {
            Destroy(sardina); //Lo destruimos para que no vuelva a aparecer y no lo veamos
            Destroy(transicion); //Al igual que la transición que ya la tenemos creada y con don't destroy
        

        }
    }
}
