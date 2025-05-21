using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cortinas : MonoBehaviour
{
    Animator animator;
    [SerializeField] Collider2D collider;
    [SerializeField] Collider2D colliderD;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AbrirCortinas();
      
    }

    public void AbrirCortinas()
    {
       
        if (Inventario.instancia.ObtenerObjetoActivo() == Inventario.instancia.cuchillo)
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
