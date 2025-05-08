using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Poner esta configuracion para hacer transición entre escenas (Todas las escenas)



public class CajasCOM : MonoBehaviour
{   
    public GameObject caja;
    public GameObject bordes;
    public enum TipoCaja { Nada, CajaNormal, CajaOro, CajaSucia } //Creamos una enumeración para guardar los tipos de caja posibles
    public TipoCaja Caja = TipoCaja.Nada; //Lo inicializamos.
    public int escenaActual;
    [SerializeField] Dialogos dialog;
    private string[] Aviso1 = {"¡No deberías coger cosas ajenas!, deberías pedir permiso"}; // Nuestras frases
    private string[] Aviso2 = {"Ya tienes una caja, ¡No puedes llevar más!"}; // Nuestras frases
    bool conversacionFinalizada = false;

    // Start is called before the first frame update
    void Start()
    {
        bordes.SetActive(false);
        escenaActual = SceneManager.GetActiveScene().buildIndex;
        dialog.OcultarNombre(); //Ocultamos en este caso el nombre ya que es un aviso.
        

    }
  
    void OnMouseOver()
    {
        bordes.SetActive(true);
    }
    void OnMouseExit()
    {
        bordes.SetActive(false);
    }
    void OnMouseDown() //Cuando clickemos 
    {
        if(escenaActual == 3 || escenaActual == 5)
        {
             
            if (!GameManager.Instance.HablarNPC)
            {
                dialog.LimpiarDialogos();
                conversacionFinalizada = dialog.ComenzarDialogo(Aviso1, conversacionFinalizada);
            }
            else
            {
                if (GameManager.Instance.CajaObtenida)
                {
         
                    dialog.LimpiarDialogos();
                    conversacionFinalizada = dialog.ComenzarDialogo(Aviso2, conversacionFinalizada);
                }
                else
                {
                    GameManager.Instance.CajaObtenida = true;
                    Inventario.instancia.MeterObjetoInventario(caja);
                    ComprobarCaja();

                }

            }

        }
        else
        {
            dialog.OcultarNombre();


            if (GameManager.Instance.CajaObtenida)
            {
               
                dialog.LimpiarDialogos();
                conversacionFinalizada = dialog.ComenzarDialogo(Aviso2, conversacionFinalizada);

            }
            else
            {
                
                ComprobarCaja();
                GameManager.Instance.CajaObtenida = true;
            }
           
        }
       
 
    }
   
    void ComprobarCaja()
    {
        if (CompareTag("CajaNormal")) //Mirara el tag para asignar un su tipo de enum correspondiente (si lo hay)
        {
            Caja = TipoCaja.CajaNormal;

        }
        if (CompareTag("CajaSucia"))
        {
            Caja = TipoCaja.CajaSucia;

        }
        if (CompareTag("CajaOro"))
        {
            Caja = TipoCaja.CajaOro;
            GameManager.Instance.cajaOroCogida = true;

        }
       
        GameManager.Instance.mensajeCoger = true; //Convertimos en true la variable MensajeCoger para usarlo en otro script y mostrar el mensaje por pantalla
        bordes.SetActive(false); //"Cogemos el Objeto"
        caja.SetActive(false);
        if (Caja == TipoCaja.CajaNormal || Caja == TipoCaja.CajaSucia || Caja == TipoCaja.CajaOro) //Mira si tiene alguna de esas etiquetas y si es ásí, sigue con lo de abajo:
        {
           
                GameManager.Instance.RecogerCaja(caja); //Llamamos a la función RecogerCaja del otro script que se puede, porque tiene almacenado el objeto que tiene esas funciones, por lo que es accesible)

            
        }

    }

    
}
