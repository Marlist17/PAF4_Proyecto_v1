using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TiempoCountdown : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TextoTiempo; // Metemos el texto que mostrará el tiempo
    private float Tiempo = 25f; // Tiempo inicial para la cuenta atrás

    void Start()
    {

        GameManager.Instance.tiempoCompletado = false;
    }

    void Update()
    {
        if (Tiempo > 0 && !GameManager.Instance.MinijuegoBatalla) // Solo restamos tiempo si es mayor a 0
        {
            Tiempo -= Time.deltaTime; // Restamos el tiempo que vaya pasando
            Tiempo = Mathf.Max(Tiempo, 0); // Aseguramos que no baje de 0
            ActualizarTextoTiempo(); // Actualizamos el texto del tiempo
        }
        else if (GameManager.Instance.MinijuegoBatalla) //Si ganamos
        {
            TextoTiempo.text = ""; //Ya no se mostrará el contador del tiempo
        }
        else
        {
         
            TextoTiempo.text = "00:00"; // Nos aseguramos de que el texto no muestre números negativos
            GameManager.Instance.tiempoCompletado = true;
        }
    }

    void ActualizarTextoTiempo()
    {
        int minutos = Mathf.FloorToInt(Tiempo / 60); // Calculamos los minutos
        int segundos = Mathf.FloorToInt(Tiempo % 60); // Calculamos los segundos
        TextoTiempo.text = string.Format("{0:00}:{1:00}", minutos, segundos); // Mostramos el tiempo en pantalla
    }
}

