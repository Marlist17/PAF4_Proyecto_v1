using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinijuegoBaldosas : MonoBehaviour
{
    public GameObject se�al;
    public GameObject baldosa;
    private Animator animator;
    public int contador = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
       
        if(contador == 1 && col.isTrigger)
        {
            baldosa.SetActive(false);
            contador = 0;
            Invoke("reiniciar", 0.3f);
        }
      
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.isTrigger)
        {

            se�al.SetActive(false);
            contador++;
            animator.SetBool("inicial", false);
            animator.SetBool("roto", true);
            GameManager.Instance.baldosaRota++;
        }

    }
    void reiniciar()
    {
        GameManager.Instance.baldosaRota = 0;
        animator.SetBool("roto",true);
        animator.SetBool("inicial", true);
        GameManager.Instance.ReiniciarNivel();

    }
}
