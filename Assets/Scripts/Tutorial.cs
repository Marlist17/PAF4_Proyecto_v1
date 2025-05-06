using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] Dialogos dialog;
    public string[] lines; // Nuestras frases
   [SerializeField] bool conversacionFinalizada = false;
    bool FueradeEscena = false;
    HashSet<GameObject> gameobjects;
    void Start()
    {
        dialog.FueradeRango();
        gameobjects = new();
    }

    void Update()
    {
        dialog.PasarDialogo();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameobjects.Contains(collision.gameObject))
            return;
       gameobjects.Add(collision.gameObject);
        dialog.FueradeRango();

        if (GameManager.Instance.ObjetoObtenido) //Si esta variable es verdadera (solo cuando se ha cogido el objeto)
        {
            Invoke("TransicionLobby", 1f);
            FueradeEscena = true;
          
        }
        else
        {
          
            if (GameManager.Instance.NochePasada)
            {
                GameManager.Instance.TransicionLobby();
                FueradeEscena = true;
                return; //detiene la ejecución siguiente para poder transicionar al lobby
            }

            conversacionFinalizada = dialog.ComenzarDialogo(lines, conversacionFinalizada);
           
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {  
        gameobjects.Remove(collision.gameObject);
        if(!FueradeEscena)
            dialog.FueradeRango();

    }
    void TransicionLobby()
    {
        GameManager.Instance.TransicionLobby();
    }
}
