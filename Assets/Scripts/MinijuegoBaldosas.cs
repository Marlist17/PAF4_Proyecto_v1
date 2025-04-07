using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinijuegoBaldosas : MonoBehaviour
{
    public GameObject señal;
    public GameObject baldosa;
    public int contador = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
       
        if(contador == 1 && col.isTrigger)
        {
            baldosa.SetActive(false);
            contador = 0;
            Invoke("reiniciar", 0.3f);
        }
      
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.isTrigger)
        {

            señal.SetActive(false);
            contador++;
            GameManager.Instance.baldosaRota++;
        }

    }
    void reiniciar()
    {
        GameManager.Instance.baldosaRota = 0;
        GameManager.Instance.ReiniciarNivel();

    }
}
