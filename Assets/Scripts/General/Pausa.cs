using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausa : MonoBehaviour
{
    [SerializeField] GameObject menuPausa;
    [SerializeField] GameObject botonPausa;
    [SerializeField] GameObject controles;
    [SerializeField] GameObject botonSinsonido;
    [SerializeField] GameObject botonSonido;
   
    [SerializeField] AudioSource audioSource; // Cambia GameObject por AudioSource
    private bool pausa = false;
    private bool controlesAbierto = false;

    // Start is called before the first frame update
    void Start()
    {
        botonSonido.SetActive(false);
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
        controles.SetActive(false);
        controlesAbierto = false;
        pausa = false;
    }
    public void Pausar()
    {
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
        Time.timeScale = 0;
        pausa = true;
    }

    public void Controles()
    {
        if (!controlesAbierto)
        {
            controles.SetActive(true);
            controlesAbierto = true;
        }
        else
        {
            controles.SetActive(false);
            controlesAbierto = false;
        }
    }
    public void Sonido()
    {
        Debug.Log("Suena");
        botonSinsonido.SetActive(true);
        botonSonido.SetActive(false);
        AudioListener.volume = 1f;
        //AudioManager.Instance.enableAudio(true);
        //SubirVolumen();
    }
    public void SinSonido()
    {
        Debug.Log("Silencio");
        botonSinsonido.SetActive(false);  
        botonSonido.SetActive(true);
        AudioListener.volume = 0f;
        //BajarVolumen();
        //AudioManager.Instance.enableAudio(false);
    }

}
