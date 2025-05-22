using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TransicionCreditos : MonoBehaviour
{
    Animator animator;
    bool animacionTerminada = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Si la animación ha terminado (normalizedTime >= 1) y no se ha cambiado de escena
        if (stateInfo.normalizedTime >= 1.0f && !animacionTerminada)
        {
            animacionTerminada = true;
            Debug.Log("Animación terminada, cambiando escena...");
            SceneManager.LoadScene(12);
        }
    }
}
