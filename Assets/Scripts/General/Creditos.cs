using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Creditos : MonoBehaviour
{
    Animator anim;
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
    }
}
