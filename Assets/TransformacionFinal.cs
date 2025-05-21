using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TransformacionFinal : MonoBehaviour
{
    Animator animator;
    bool animacionTerminada = false;
    [SerializeField] GameObject cuadro;
    AudioClip sonidoMuerte;
    AudioClip sonidoTransformacion;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(EsperarYDesactivarCuadro());

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

    IEnumerator EsperarYDesactivarCuadro()
    {
        AudioManager.Instance.PlaySound(sonidoMuerte);
        // Espera 2 segundos
        yield return new WaitForSeconds(5f);
        // Desactiva el objeto
        cuadro.SetActive(false);
 
        AudioManager.Instance.PlayTransformSound();
        yield return new WaitForSeconds(2f);
    }
   
        
        
       

}
