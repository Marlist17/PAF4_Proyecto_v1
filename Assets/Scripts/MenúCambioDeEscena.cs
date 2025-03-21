using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenúCambioDeEscena : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BotonComenzar()
    {
        SceneManager.LoadScene(1);
    }

    public void BotonSalir()
    {
        Debug.Log("Salir");
        Application.Quit();

    }
}
