using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Mover : MonoBehaviour
{
    private float VelocidadMovimiento = 3f;
   
    // Start is called before the first frame update
    void Start()
    {
        
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

        float movimientoY = Input.GetAxis("Vertical") * VelocidadMovimiento * Time.deltaTime; //Si pulso felcha arriba (1) , flecha abajo (-1) y si no pulso nada (0);
        transform.Translate(0, movimientoY, 0);
        float movimientoX = Input.GetAxis("Horizontal") * VelocidadMovimiento * Time.deltaTime; //Normalizamos el tiempo de frame
        transform.Translate(movimientoX, 0, 0);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            if (collision.gameObject.tag == "CasaNPC") //Si esta variable es verdadera (solo cuando se ha cogido el objeto)
            {
                GameManager.Instance.HablarNPC = false; //Resetear por si sale de escena.
                Invoke("TransicionLobby", 1f);

            }
            if (collision.gameObject.tag == "CasaProta" && GameManager.Instance.TutorialRealizado)
            {

            Invoke("TransicionLobby", 1f);

        }
        //Poner que coga la escena y si es diferente del del lobby ( va al lobby siempre)
        


    }
    void TransicionLobby()
    {
        GameManager.Instance.TransicionLobby();
    }
}

    

