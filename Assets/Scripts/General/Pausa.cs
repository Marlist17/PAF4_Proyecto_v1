using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour
{
    [SerializeField] GameObject menuPausa;
    [SerializeField] GameObject botonPausa;
    private bool pausa = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausa)
            {
                Reanudar();
            }
            else
            {
                Pausar();
            }
        }
    }

    public void Reanudar()
    {
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
        Time.timeScale = 1;
        pausa = false;
    }
    public void Pausar()
    {
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
        Time.timeScale = 0;
        pausa = true;
    }

    public void MenuP()
    {
        SceneManager.LoadScene(4);
    }

}
