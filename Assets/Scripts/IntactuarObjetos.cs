using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntactuarObjetos : MonoBehaviour
{
    public GameObject bordes;
   
    void Start()
    {
        bordes.SetActive(false);
  
    }
    void OnMouseOver()
    {
        bordes.SetActive(true);
       
    }
    void OnMouseExit()
    {
        bordes.SetActive(false);
       
    }
    void OnMouseDown() //Cuando clickemos 
    {


        if (!GameManager.Instance.ObjetoObtenido)
        {
            if (CompareTag("Altar")) //Mirara el tag para asignar un su tipo de enum correspondiente (si lo hay)
            { 
                return;

            }


            GameManager.Instance.CondicionTutorial();  
            gameObject.SetActive(false);
            bordes.SetActive(false);
            GameManager.Instance.mensajeCoger = true;

        }


        else //Dejamos objeto
        {
            GameManager.Instance.mensajeDejar = true;
            GameManager.Instance.ObjetoObtenido = false;
        }

    }


}



