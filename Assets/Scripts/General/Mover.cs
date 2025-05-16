using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Poner esta configuracion para hacer transición entre escenas (Todas las escenas)


public class Mover : MonoBehaviour
{

    private float VelocidadMovimiento = 3f; 
    Animator anim;
    public static GameObject instancia; //Creamos una gameObject
    Rigidbody2D rb;
    void Awake()
    {
        
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (SceneManager.GetActiveScene().buildIndex == 2) //Miramos si estamos en la plaza
        {   
            
            if (GameManager.Instance.lugar == "CasaRico") //Dependiendo de donde hemos salido movemos las coordenadas del jugador
            transform.position = new Vector3(-8.62f, -0.50f, 0f);
        }
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
        if (!GameManager.Instance.movimiento)
        {
            rb.velocity = Vector2.zero;
           
            anim.SetBool("ProtaCamArriba", false);
            anim.SetBool("ProtaCamAbajo", false);
            anim.SetBool("ProtaCamDerecha", false);
            anim.SetBool("ProtaCamIzquierda", false);
 
            return;
        }
        float moverx = Input.GetAxis("Horizontal"); //Obtenemos el mov en el eje horizontal
        float movery = Input.GetAxis("Vertical"); //Obtenemos el mov en el eje vertical
        rb.velocity = new Vector2(moverx * VelocidadMovimiento, movery * VelocidadMovimiento); //Aplicamos al riggidBody
       

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


