using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatallaClics : MonoBehaviour
{
    private int contador = 0;
    private int faseActual = 1;
    private Animator animator;

    // Configuración de clics por fase
    private int[] clicsPorFase = { 30, 60, 90, 125 };

    // Nombres de los parámetros booleanos en el Animator
    private string[] parametrosFase = { "Fase1", "Fase2", "Fase3", "Fase4" };

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("No se encontró un componente Animator en el objeto.");
        }
        else
        {
            // Activar solo la animación inicial (Fase 1)
            ActivarFaseAnimator(1);
        }
    }

    void Update()
    {
        ComprobarVictoria();
        if (Input.GetMouseButtonDown(0))
        {
            contador++;
            Debug.Log($"Clics: {contador} - Fase: {faseActual}");

            // Verificar si avanzamos a la siguiente fase
            VerificarCambioDeFase();
        }
    }

    void VerificarCambioDeFase()
    {
        for (int i = faseActual - 1; i < clicsPorFase.Length; i++)
        {
            if (contador >= clicsPorFase[i] && faseActual <= i + 1)
            {
                faseActual = i + 2; // Avanzar a la siguiente fase
                ActivarFaseAnimator(faseActual);
                break;
            }
        }
    }

    void ActivarFaseAnimator(int nuevaFase)
    {
        if (animator != null)
        {
            // Desactivar todos los booleanos primero
            foreach (string param in parametrosFase)
            {
                animator.SetBool(param, false);
            }

            // Activar solo el booleano de la fase actual
            if (nuevaFase <= parametrosFase.Length)
            {
                string parametroActivar = parametrosFase[nuevaFase - 1];
                animator.SetBool(parametroActivar, true);
                Debug.Log($"Activando animación: {parametroActivar}");
            }
        }
    }

    void ComprobarVictoria()
    {
        if (contador >= clicsPorFase[clicsPorFase.Length - 1]) // Última fase
        {
            GameManager.Instance.MinijuegoBatalla = true;
        }
    }
}


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatallaClics : MonoBehaviour
{
    private int contador = 0;
 
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ComprobarVictoria();
        if (Input.GetMouseButtonDown(0))
        {
            contador++;
            Debug.Log(contador);
        
        
        
        }

    }

    void ComprobarVictoria()
    {
        if (contador == 125)
        {
            GameManager.Instance.MinijuegoBatalla = true;
        }
        
    }
}*/
