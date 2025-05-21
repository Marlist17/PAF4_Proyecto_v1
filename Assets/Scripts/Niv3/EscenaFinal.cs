using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaFinal : MonoBehaviour
{
    Animator animator;
    bool animacionTerminada = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        // Si la animación ha terminado (normalizedTime >= 1) y no se ha cambiado de escena
        if (stateInfo.normalizedTime >= 1.0f && !animacionTerminada)
        {
            animacionTerminada = true;
            Debug.Log("Animación terminada, cambiando escena...");
            SceneManager.LoadScene(11);
        }
    }
}