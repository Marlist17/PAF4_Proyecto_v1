using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Poner esta configuracion para hacer transición entre escenas (Todas las escenas)


public class Mover : MonoBehaviour
{

    private float VelocidadMovimiento = 3f; 
    Animator anim;
    public static GameObject instancia; //Creamos una gameObject

    void Awake()
    {
        
    }
    void Start()
    {
        anim = GetComponent<Animator>();

    }


    void Update()
    {
        

    }
    void FixedUpdate()
    {
        MoverPersonaje();

    }
    void MoverPersonaje()
    {

        float movimientoY = Input.GetAxis("Vertical") * VelocidadMovimiento * Time.deltaTime; // Almacenamos la cantidad de mov en el eje vertical que vamos a ejercer mediante inputs (teclas)
        transform.Translate(0, movimientoY, 0); //Lo movemos en esa dirección
        float movimientoX = Input.GetAxis("Horizontal") * VelocidadMovimiento * Time.deltaTime; // Almacenamos la cantidad de mov en el eje horizontal que vamos a ejercer mediante inputs (teclas)
        transform.Translate(movimientoX, 0, 0); //Lo movemos en esa dirección

        if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("ProtaCamAbajo", true);
        }
        else
        {
            anim.SetBool("ProtaCamAbajo", false);
        }

        if (Input.GetKey(KeyCode.W))
        {
            anim.SetBool("ProtaCamArriba", true);
        }
        else
        {
            anim.SetBool("ProtaCamArriba", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("ProtaCamDerecha", true);
        }
        else
        {
            anim.SetBool("ProtaCamDerecha", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("ProtaCamIzquierda", true);
        }
        else
        {
            anim.SetBool("ProtaCamIzquierda", false);
        }
    }


 

}


