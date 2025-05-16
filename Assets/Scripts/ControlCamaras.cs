using Cinemachine;
using UnityEngine;

public class ControlCamaras : MonoBehaviour
{
    public CinemachineVirtualCamera camaraPrincipal;
    public CinemachineVirtualCamera camaraSecundariaNv1;

    
    void Start()
    {
        ActivarCamaraPrincipal(); //Siempre ponemos la cámara principal
       
    }

    private void ActivarCamaraPrincipal()
    {
        if (camaraPrincipal != null) //Si es diferente de null:
            camaraPrincipal.enabled = true; //Activamos que se vea esta cámara

        if (camaraSecundariaNv1 != null) //Desactivamos la cámara secundaria
            camaraSecundariaNv1.enabled = false;
    }

    private void ActivarCamaraSecundaria()
    {
        if (camaraPrincipal != null)
            camaraPrincipal.enabled = false; //desactivamos la cámara principal

        if (camaraSecundariaNv1 != null)
            camaraSecundariaNv1.enabled = true; //Activamos la camara secundaria
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
    

        ActivarCamaraSecundaria(); //Si entramos en la zona específica: ponemos la nueva cámara
    }

    private void OnTriggerExit2D(Collider2D other) //Si no volvemos a la principal
    {


        ActivarCamaraPrincipal();
    }
}
