using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuertasAnim : MonoBehaviour
{
    Animator anim; // Referencia al componente Animator de la puerta
    [SerializeField] Collider2D colliderAnim; // Collider para activar la animación
    [SerializeField] Collider2D collider; // Collider para la puerta

    public void Start()
    {
        // Obtener el componente Animator al inicio
        anim = GetComponent<Animator>();
    }
    
    public void AbrirPuerta(Collider2D colliderAnim)
    {
        if (colliderAnim.isTrigger)
        {
            anim.SetBool("Abrir", true); // Si el collider es un trigger, activar la animación de abrir
            collider.enabled = false; // Desactivar el collider de la puerta
        }
    }
}
