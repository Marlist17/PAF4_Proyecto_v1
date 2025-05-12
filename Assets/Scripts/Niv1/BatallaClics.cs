using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatallaClics : MonoBehaviour
{
    private int contador = 0;
 
    void Start()
    {

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
            GameManager.Instance.Mision_2 = true;
        }
        
    }
}
