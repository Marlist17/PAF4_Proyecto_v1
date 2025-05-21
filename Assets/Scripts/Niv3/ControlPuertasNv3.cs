using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPuertasNv3 : MonoBehaviour
{
    Animator anim; // Referencia al componente Animator de la puerta
    [SerializeField] Collider2D collider; // Collider para la puerta

    void Start()
    {
        anim = GetComponent<Animator>(); // Obtenemos el componente Animator de la puerta
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(GameManager.Instance.baldosaRota == 48)
        {
            Debug.Log("Has conseguido superar el tercer nivel");
            anim.SetBool("cerrada", false); // Desactivamos la animación de puerta cerrada
            anim.SetBool("abrir", true); // Activamos la animación de abrir puerta
            collider.enabled = false;
            Debug.Log("Puerta del nv 3 abriendose...");

            GameManager.Instance.baldosaRota = 0; //Reseteamos para los proximos niveles.
        }
        else if (GameManager.Instance.baldosaRota == 27)
        {
            Debug.Log("Has conseguido superar el segundo nivel");
            anim.SetBool("cerrada", false); // Desactivamos la animación de puerta cerrada
            anim.SetBool("abrir", true); // Activamos la animación de abrir puerta
            collider.enabled = false;
            Debug.Log("Puerta del nv 2 abriendose...");
            GameManager.Instance.baldosaRota = 0; //Reseteamos para los proximos niveles.
        }
        else if (GameManager.Instance.baldosaRota == 13)
        {
            Debug.Log("Has conseguido superar el pprimer nivel");
            anim.SetBool("cerrada", false); // Desactivamos la animación de puerta cerrada
            anim.SetBool("abrir", true); // Activamos la animación de abrir puerta
            collider.enabled = false;
            Debug.Log("Puerta del nv 1 abriendose...");
            GameManager.Instance.baldosaRota = 0; //Reseteamos para los proximos niveles.
        }
    }
}
