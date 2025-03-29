using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovRaton : MonoBehaviour
{
    Rigidbody2D rb; //Declaramos la variable para acceder a los componentes de nuestro rigidBody2D
    [SerializeField] float velocidadRaton = 5f; //Creamos una variable que controle la velocidad del rat�n
    Vector2 direccionMov =  Vector2.zero; //Variable que guarda la direcci�n del mov del rat�n
    private bool PisadoBaldosaTutorial = false; //Variable que controle si ha pasado la baldosa inicial (sirve para mostrar como funcionan las baldosas)
    public bool SobreBaldosa = false; //Variable para controlar si est� en una de las baldosas correspondientes para la elecci�n de movimiento
    public int NumAleatorio; //Variable que almacene el n� aleatorio que salga
    public GameObject Victoria; //variable que contiene el objeto con el mensaje de victoria.


    //IMPORTANTE A TENER EN CUENTA LAS ESCALAS, MANTENERLAS EN 1 --> CAMBAIR TAMA�O POR PX (UNIT)

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //Buscamos el componente de rigidBody para acceder a sus componentes.
        rb.velocity = Vector2.zero; // Inicializamos la velocidad de nuestro personaje en 0.

        //Hacemos que inicie un mov hacia abajo (para mostrar la mec�nica principal del juego)
        transform.localRotation = Quaternion.Euler(0, 0, 0); //Inicializamos la POSICI�N de nuestro personaje (mirando hacia abajo)
        direccionMov = Vector2.down; //Inicializamos la direcci�n en la que va a comenzar a moverse
        Victoria.SetActive(false); //Volvemos invisible el mensaje de victoria.
    }

    void Update() //Cada frame se ejecutara el siguiente c�digo
    {
        if (SobreBaldosa) //Si est� sobre una baldosa:
        {
            if (Input.GetKey(KeyCode.UpArrow)) //Si pulsa la flecha de arriba.
            {

                transform.localRotation = Quaternion.Euler(0, 0, 180); //El sprite se girar� para esa direcci�n.

                direccionMov = Vector2.up; //El sprite se mover� para esa direcci�n
            }
            else if (Input.GetKey(KeyCode.DownArrow)) //Si pulsa la flecha de abajo.
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);  //El sprite se girar� para esa direcci�n.

                direccionMov = Vector2.down; //El sprite se mover� para esa direcci�n

            }
            else if (Input.GetKey(KeyCode.LeftArrow)) //Si pulsa la flecha de la izquierda.
            {
                transform.localRotation = Quaternion.Euler(0, 0, -90);  //El sprite se girar� para esa direcci�n.

                direccionMov = Vector2.left; //El sprite se mover� para esa direcci�n
            }
            else if (Input.GetKey(KeyCode.RightArrow)) //Si pulsa la flecha de la derecha.
            {
                transform.localRotation = Quaternion.Euler(0, 0, 90);  //El sprite se girar� para esa direcci�n.

                direccionMov = Vector2.right; //El sprite se mover� para esa direcci�n

            }
        }
      
     
    }

  
    void FixedUpdate()
    {
        rb.velocity = velocidadRaton * direccionMov; //Se ir� actualizando la velocidad en la direcci�n correspondiente (para el movimiento del personaje)
    }
    private void OnTriggerExit2D(Collider2D collision) //Cuando sale del trigger:
    {
        SobreBaldosa = false; //Marcamos qu eya no est� pisando la baldosa.

        if (collision.CompareTag("Pared")) //Si colisiona con la pared del escenario:
        {
           
            //cambiamos la direcci�on del movimiento:
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
            else if (direccionMov.y == 1) // Se mueve hacia arriba (positivo porque cambia la direcci�n)
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


