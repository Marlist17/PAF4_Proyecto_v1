using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogos : MonoBehaviour //Di�logos con trigger (la puerta y los mensajes de acci�n).
{
    public TextMeshProUGUI miTexto; // Referencia p�blica a nuestro objeto texto
    [SerializeField] TextMeshProUGUI TextoNombre; // Referencia p�blica a nuestro objeto texto
    public GameObject cajaTexto;
    public GameObject CajaNombre;
    public GameObject raton;
    public string[] lines; // Nuestras frases
    public float textSpeed; // Velocidad del texto
    public int index; // �ndice del di�logo en curso
    public bool DialogoActivo = false;
   

    void Start()
    {
        TextoNombre.text = "";
    }

    public void FueradeRango()
    {

        if (raton != null && raton.gameObject != null)
            raton.SetActive(false);

        if (cajaTexto != null && cajaTexto.gameObject != null)
            cajaTexto.SetActive(false);

        if (miTexto != null && miTexto.gameObject != null)
        {
            miTexto.text = string.Empty;
            miTexto.gameObject.SetActive(false);
        }
        index = 0;  
    }



    void Update()
    {
        
        
    }

    public void PasarDialogo()
    {
       
        if (Input.GetMouseButton(0)) // Si pulsamos el bot�n derecho
        {
            if (index >= 0 && index < lines.Length) // Validamos que el �ndice est� en rango
            {
                if (miTexto.text == lines[index]) // Si se ha colocado todo el texto que tiene guardado el array.
                {
                    Debug.Log("Siguietne dialogo");
                    SiguienteDialogo();
                }
            }
        }

    }

    public bool ComenzarDialogo(string[] lineasNuevas, bool finalizado)
    {
        Debug.Log("Comenzar dialogo");
        DialogoActivo =true;
        if (finalizado)
            UltimoDialogo(lineasNuevas);
        else
        {
           
            lines = lineasNuevas;
            index = 0;
            raton.SetActive(true); //Dejamos que sea visible el rat�n
            miTexto.gameObject.SetActive(true); //Dejamos que sea visible el texto
            cajaTexto.SetActive(true); //Dejamos que no sea visible la caja de texto
            Debug.Log($"index:{index}");
            StartCoroutine(TypeLine());

        }
        return true;
    
    }

    IEnumerator TypeLine()
    {
      
        foreach (char c in lines[index].ToCharArray())
        {
            miTexto.text += c; // Aparece letra a letra
            yield return new WaitForSeconds(textSpeed);
        } 
    }

    public void SiguienteDialogo()
    {
        Debug.Log("Siguietne dialogo");
        if (index < lines.Length -1) //�ndice empieza de 0 (la longitud del texto del 1)
        {
            index++;
            miTexto.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            raton.SetActive(false); //Dejamos que no sea visible el rat�n
            miTexto.gameObject.SetActive(false); //Dejamos que deje de ser visible el texto
            cajaTexto.SetActive(false); //Dejamos que no sea visible la caja de texto
            DialogoActivo = false;
        }
    }
    public void UltimoDialogo(string[] lineasNuevas )
    {
        miTexto.text = string.Empty;
        lines = new string[lineasNuevas.Length]; // Creamos un nuevo array con el tama�o adecuado
        lineasNuevas.CopyTo(lines, 0); // Copiamos las l�neas en el array local
        raton.SetActive(true); //Dejamos que sea visible el rat�n
        miTexto.gameObject.SetActive(true); //Dejamos que deje de ser visible el texto
        cajaTexto.SetActive(true); //Dejamos que no sea visible la caja de texto
        index = lines.Length - 1;
        Debug.Log("Mostrando ultimo dialogo");
        StartCoroutine(TypeLine());
        
    }

    public void LimpiarDialogos()
    {
        miTexto.text = string.Empty;
    }
    public void MostrarNombre(string nombre)
    {

        TextoNombre.text = nombre;
        TextoNombre.enabled = true;
        CajaNombre.SetActive(true);
    }

    public void OcultarNombre()
    {
        TextoNombre.enabled = false;
        CajaNombre.SetActive(false);
       
    }
}

