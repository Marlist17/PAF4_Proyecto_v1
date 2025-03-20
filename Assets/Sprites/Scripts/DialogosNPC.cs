using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogosNPC : MonoBehaviour
{
    public TextMeshProUGUI Texto; // Referencia pública a nuestro objeto texto
    public GameObject cajaTexto;
    public string[] lines; // Nuestras frases
    public float textSpeed; // Velocidad del texto
    private int index; // Índice del diálogo en curso
    private bool TextoPrimeraVez = true;
    private bool PlayerIsClose;
    void Start()
    {
        Texto.text = string.Empty;
        cajaTexto.SetActive(false); //Dejamos que no sea visible la caja de texto
    }


    private void OnTriggerEnter2D(Collider2D collision) //Se acceda a un sitio se activa)
    {

        if (collision.CompareTag("Player"))
        {
            PlayerIsClose = true;
        
        
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerIsClose = false;


        }
    
    }


    void Update()
    {
        if (PlayerIsClose) // Solo permitir la interacción si el jugador está cerca
        {
            if (Input.GetKeyDown(KeyCode.E)) // Si se presiona la tecla E
            {
                if (TextoPrimeraVez)
                    Invoke("ComenzarDialogo", 1f); // Llama a la función después de 1 segundo (o ajusta el tiempo según necesites)
                else
                    Invoke("UltimoDialogo", 1f);
            }
        }
        if (TextoPrimeraVez)
        {
            if (Input.GetMouseButton(0)) //Si pulsamos el botón derecho
            {
                if (Texto.text == lines[index]) //Si se ha colocado todo el texto que tiene guardado el array.
                {
                    SiguienteDialogo();
                    
                }

            }
        }
        else
        {

            if (Input.GetMouseButton(0)) //Si pulsamos el botón derecho
            {
                Texto.gameObject.SetActive(false); //Dejamos que deje de ser visible el texto
                cajaTexto.SetActive(false); //Dejamos que no sea visible la caja de texto
            }
        }

    }
    void ComenzarDialogo()
    {
        index = 0;
        Texto.gameObject.SetActive(true); //Dejamos que deje de ser visible el texto
        cajaTexto.SetActive(true); //Dejamos que no sea visible la caja de texto
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            Texto.text += c; // Aparece letra a letra
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void SiguienteDialogo()
    {
        if (index < lines.Length - 1) //índice empieza de 0 (la longitud del texto del 1)
        {
            index++;
            Texto.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {

            Texto.gameObject.SetActive(false); //Dejamos que deje de ser visible el texto
            cajaTexto.SetActive(false); //Dejamos que no sea visible la caja de texto
            TextoPrimeraVez = false;

        }
    }
    void UltimoDialogo()
    {
        Texto.text = string.Empty;
        Texto.gameObject.SetActive(true); //Dejamos que deje de ser visible el texto
        cajaTexto.SetActive(true); //Dejamos que no sea visible la caja de texto
        index = lines.Length - 1;
        StartCoroutine(TypeLine());
    }

}
