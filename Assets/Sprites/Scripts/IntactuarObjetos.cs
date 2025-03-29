using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntactuarObjetos : MonoBehaviour
{
    public GameObject bordes; //Metemos el sprite con el borde para indicar que es un objeto interactivo
    [SerializeField] Dialogos dialog; //Metemos el objeto del dialogManager para acceder a todas las funciones
    private string[] Aviso1 = { "Ya llevas un objeto, ¡No puedes llevar más!" }; // Nuestro aviso
    bool conversacionFinalizada = false; //Condición de si ya ha salido el aviso


    void Start()
    {
        bordes.SetActive(false); //Hacemos invisible los bordes
        if(dialog != null) //Si hay dialogo
        {
            dialog.OcultarNombre(); //Ocultamos en este caso el nombre ya que es un aviso.
        }
       

    }
    void OnMouseOver()
    {
        bordes.SetActive(true); //Mostramos el borde al tener el ratón encima del objeto
       
    }
    void OnMouseExit()
    {
        bordes.SetActive(false); //Cuando lo sacamos el borde se vuelve invisible de nuevo.
       
    }
    void OnMouseDown() //Cuando clickemos 
    {


        if ((!GameManager.Instance.ObjetoObtenido)) //Si no tenemos la variable en true de ObjetoObtenido:
        {
            if (CompareTag("Altar")) //Si tiene el tag altar:
            {
                //No hacemos nada ya que ya lo único importante es DEJAR EL OBJETO en el altar
            }
            else
            {
               
                GameManager.Instance.CogerObjeto(); //Hacemos true que hemos cogido el objeto
                gameObject.SetActive(false); //Volvemos invisible el objeto que hemos cogido
                bordes.SetActive(false); //Volvemos invisible los bordes
                GameManager.Instance.mensajeCoger = true; //Convertimos en true la variable MensajeCoger para usarlo en otro script y mostrar el mensaje por pantalla
            }
           

        }
     
        else if  (GameManager.Instance.ObjetoObtenido && CompareTag("Altar"))//Dejamos objeto en el altar cuando tenemos la condicion de ObjetoObtenido
        {
           
                GameManager.Instance.mensajeDejar = true; //Convertimos en true la variable MensajeDejar para usarlo en otro script y mostrar el mensaje por pantalla
                GameManager.Instance.ObjetoObtenido = false; //Volvemos la variable false ya que no tenemos el objeto
       
        }
        else //Si hemos cogido el objeto y clickamos en otros sitios interactuables que no sean para dejar el objeto
        {
            dialog.LimpiarDialogos(); //Limpiamos por si queda algo de texto
            conversacionFinalizada = dialog.ComenzarDialogo(Aviso1, conversacionFinalizada); //Mostramos el aviso para que deje primero el objeto para coger otro

        }

    }


}



