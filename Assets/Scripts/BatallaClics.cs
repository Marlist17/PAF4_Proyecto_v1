using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatallaClics : MonoBehaviour
{
    private int contador = 0;
    public GameObject Victoria;
    void Start()
    {
        Victoria.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ComprobarVictoria();
        if (Input.GetMouseButtonDown(0))
        {
            contador++;
            Debug.Log(contador);
        
        
        
        }

    }

    void ComprobarVictoria()
    {
        if (contador == 125)
        {
            Victoria.SetActive(true);
            GameManager.Instance.VictoriaMinijuegoCallejon = true;
        }
        
    }
}
