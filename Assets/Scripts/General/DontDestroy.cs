using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject instancia;

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this.gameObject; //  Esta línea es esencial
            DontDestroyOnLoad(instancia);
            
        }
        else
        {
          
            Destroy(gameObject);
        }
       
       
    
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}

