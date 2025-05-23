using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class EscenaFinal : MonoBehaviour
{
    Animator animator;
    bool animacionTerminada = false;
    [SerializeField] GameObject cuadro;
     AudioClip sonidoMuerte;
     AudioClip sonidoTransformacion;

    void Start()
    {
       
        AudioManager.Instance.PlaySound(sonidoTransformacion);
        
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

    IEnumerator EsperarYEjecutar()
    {
        Debug.Log("Inicio de la espera");

        yield return new WaitForSeconds(4f); // Espera 2.5 segundos

        Debug.Log("Pasaron 2.5 segundos");

    }
}