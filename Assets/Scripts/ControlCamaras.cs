using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamaras : MonoBehaviour
{
    public CinemachineVirtualCamera camaraPrincipal;
    public CinemachineVirtualCamera camaraSecundariaNv1;
    void Start()
    {
        camaraPrincipal.enabled = true;
        camaraSecundariaNv1.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Esoty en el trigger");
        if (other.CompareTag("Player")) // Asegúrate de que el jugador tenga el tag "Player"
        {
            camaraPrincipal.enabled = false;
            camaraSecundariaNv1.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            camaraPrincipal.enabled = true;
            camaraSecundariaNv1.enabled = false;
        }
    }
}
