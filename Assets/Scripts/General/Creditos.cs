using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Creditos : MonoBehaviour
{
    Animator anim;
    bool animacionTerminada = false;
    public TextMeshProUGUI mensaje;
    public GameObject botonSalir;

    void Start()
    {
        anim = GetComponent<Animator>();
        mensaje.gameObject.SetActive(false);
        botonSalir.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       anim.SetBool("empieza", true);
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);

        // Si la animación ha terminado (normalizedTime >= 1) y no se ha cambiado de escena
        if (stateInfo.normalizedTime >= 1.0f && !animacionTerminada)
        {
            animacionTerminada = true;
            mensaje.gameObject.SetActive(true);
            botonSalir.SetActive(true); 
        }
    }
}
