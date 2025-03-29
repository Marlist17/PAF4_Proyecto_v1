using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovRaton : MonoBehaviour
{
    Rigidbody2D rb; //Declaramos la variable para acceder a los componentes de nuestro rigidBody2D
    [SerializeField] float velocidadRaton = 5f; //Creamos una variable que controle la velocidad del ratón
    Vector2 direccionMov =  Vector2.zero; //Variable que guarda la dirección del mov del ratón
    private bool PisadoBaldosaTutorial = false; //Variable que controle si ha pasado la baldosa inicial (sirve para mostrar como funcionan las baldosas)
    public bool SobreBaldosa = false; //Variable para controlar si está en una de las baldosas correspondientes para la elección de movimiento
    public int NumAleatorio; //Variable que almacene el nº aleatorio que salga
    public GameObject Victoria; //variable que contiene el objeto con el mensaje de victoria.


    //IMPORTANTE A TENER EN CUENTA LAS ESCALAS, MANTENERLAS EN 1 --> CAMBAIR TAMAÑO POR PX (UNIT)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Buscamos el componente de rigidBody para acceder a sus componentes.
        rb.velocity = Vector2.zero; // Inicializamos la velocidad de nuestro personaje en 0.

        //Hacemos que inicie un mov hacia abajo (para mostrar la mecánica principal del juego)
        transform.localRotation = Quaternion.Euler(0, 0, 0); //Inicializamos la POSICIÓN de nuestro personaje (mirando hacia abajo)
        direccionMov = Vector2.down; //Inicializamos la dirección en la que va a comenzar a moverse
        Victoria.SetActive(false); //Volvemos invisible el mensaje de victoria.
    }

    void Update() //Cada frame se ejecutara el siguiente código
    {
        if (SobreBaldosa) //Si está sobre una baldosa:
        {
            if (Input.GetKey(KeyCode.UpArrow)) //Si pulsa la flecha de arriba.
            {

                transform.localRotation = Quaternion.Euler(0, 0, 180); //El sprite se girará para esa dirección.

                direccionMov = Vector2.up; //El sprite se moverá para esa dirección
            }
            else if (Input.GetKey(KeyCode.DownArrow)) //Si pulsa la flecha de abajo.
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);  //El sprite se girará para esa dirección.

                direccionMov = Vector2.down; //El sprite se moverá para esa dirección

            }
            else if (Input.GetKey(KeyCode.LeftArrow)) //Si pulsa la flecha de la izquierda.
            {
                transform.localRotation = Quaternion.Euler(0, 0, -90);  //El sprite se girará para esa dirección.

                direccionMov = Vector2.left; //El sprite se moverá para esa dirección
            }
            else if (Input.GetKey(KeyCode.RightArrow)) //Si pulsa la flecha de la derecha.
            {
                transform.localRotation = Quaternion.Euler(0, 0, 90);  //El sprite se girará para esa dirección.

                direccionMov = Vector2.right; //El sprite se moverá para esa dirección

            }
        }
      
     
    }

  
    void FixedUpdate()
    {
        rb.velocity = velocidadRaton * direccionMov; //Se irá actualizando la velocidad en la dirección correspondiente (para el movimiento del personaje)
    }
    private void OnTriggerExit2D(Collider2D collision) //Cuando sale del trigger:
    {
        SobreBaldosa = false; //Marcamos qu eya no está pisando la baldosa.

        if (collision.CompareTag("Pared")) //Si colisiona con la pared del escenario:
        {
           
            //cambiamos la direcciçon del movimiento:
            direccionMov = -direccionMov;

            Debug.Log(direccionMov);
         
            if (direccionMov.x == -1)  // Se mueve a la izquierda
            {
              
                transform.localRotation = Quaternion.Euler(0, 0, -90);
            }
            else if (direccionMov.x == 1) // Se mueve a la derecha
            {
       
                transform.localRotation = Quaternion.Euler(0, 0, 90);
            }
            else if (direccionMov.y == -1) // Se mueve hacia abajo
            {
       
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            else if (direccionMov.y == 1) // Se mueve hacia arriba (positivo porque cambia la dirección)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 180);
            }
            
        }
        
   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Baldosa_Flecha_Tutorial"))
        { 
            if (!PisadoBaldosaTutorial)
            {
                PisadoBaldosaTutorial = true;
            }
            else
            {
                SobreBaldosa = true;
                direccionMov = Vector2.zero;

            }
            
        }
        else if (collision.CompareTag("Baldosa_Flecha"))
        {
            SobreBaldosa = true;
            direccionMov = Vector2.zero;
        }

        else if (collision.CompareTag("Laterales"))
        {
            NumAleatorio = Random.Range(0, 2);
            if (NumAleatorio == 1)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 180);
                direccionMov = Vector2.up;

            }
            else
            {

                transform.localRotation = Quaternion.Euler(0, 0, 0);
                direccionMov = Vector2.down;

            }
            NumAleatorio = -1;
        }
       else if (collision.CompareTag("BordeSuperior"))
       {
            NumAleatorio = Random.Range(0, 2);
            if (NumAleatorio == 1)
            {
                transform.localRotation = Quaternion.Euler(0, 0, -90);
                direccionMov = Vector2.left;

            }
            else
            {

                transform.localRotation = Quaternion.Euler(0, 0, 90);
                direccionMov = Vector2.right;

            }
            NumAleatorio = -1;


       }

        else if (collision.CompareTag("Meta"))
        {

            direccionMov = Vector2.zero;
            Victoria.SetActive(true);

        }
    }

}


