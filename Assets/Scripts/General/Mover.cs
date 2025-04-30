using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

    private float VelocidadMovimiento = 3f;
    Animator anim;

    // Start is called before the first frame update
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

        float movimientoY = Input.GetAxis("Vertical") * VelocidadMovimiento * Time.deltaTime; //Si pulso felcha arriba (1) , flecha abajo (-1) y si no pulso nada (0);
        transform.Translate(0, movimientoY, 0);
        float movimientoX = Input.GetAxis("Horizontal") * VelocidadMovimiento * Time.deltaTime; //Normalizamos el tiempo de frame
        transform.Translate(movimientoX, 0, 0);

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


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "CasaNPC") //Si esta variable es verdadera (solo cuando se ha cogido el objeto)
        {
            GameManager.Instance.HablarNPC = false; //Resetear por si sale de escena.
            Invoke("TransicionLobby", 1f);

        }
        if (collision.gameObject.tag == "CasaProta" && GameManager.Instance.TutorialRealizado)
        {

            GameManager.Instance.TransicionLobby();

        }

        //Poner que coga la escena y si es diferente del del lobby ( va al lobby siempre)



    }

}


