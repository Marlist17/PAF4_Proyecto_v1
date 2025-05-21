using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Creditos : MonoBehaviour
{
    public TextMeshProUGUI mensaje;
    public GameObject botonSalir;
    void Start()
    {
        
        mensaje.gameObject.SetActive(false);
        botonSalir.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
