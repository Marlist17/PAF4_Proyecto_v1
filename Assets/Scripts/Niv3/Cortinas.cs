using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cortinas : MonoBehaviour
{
    Animator animator;
    [SerializeField] Collider2D collider;
    [SerializeField] Collider2D colliderD;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        AbrirCortinas();
        
    }

    public void AbrirCortinas()
    {
        GameObject objetoActivo = Inventario.instancia.ObtenerObjetoActivo();

        if (objetoActivo != null && objetoActivo == Inventario.instancia.cuchillo)
        {
            collider.enabled = false;
            animator.SetBool("cerrada", false);
            animator.SetBool("abrir", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetBool("cerrada", true);
            animator.SetBool("abrir", false);
            collider.enabled = true;
        }
    }
}
