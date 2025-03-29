using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] Dialogos dialog;
    public string[] lines; // Nuestras frases
    bool conversacionFinalizada = false;
    bool FueradeEscena = false;

    void Start()
    {
        dialog.FueradeRango();
      
    }

    void Update()
    {
        dialog.PasarDialogo();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.ObjetoObtenido) //Si esta variable es verdadera (solo cuando se ha cogido el objeto)
        {
            Invoke("TransicionLobby", 1f);
            FueradeEscena = true;
            GameManager.Instance.TutorialRealizado = true;
        }
        else
        {
          
            conversacionFinalizada = dialog.ComenzarDialogo(lines, conversacionFinalizada);
           
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {   if(!FueradeEscena)
            dialog.FueradeRango();

    }
    void TransicionLobby()
    {
        GameManager.Instance.TransicionLobby();
    }
}
