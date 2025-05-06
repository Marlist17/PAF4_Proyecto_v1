using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TiempoCountdown : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TextoTiempo; // Metemos el texto que mostrará el tiempo
    private float Tiempo = 26f; // Tiempo inicial para la cuenta atrás
    public GameObject Derrota;

    void Start()
    {
        Derrota.SetActive(false);
        
    }

    void Update()
    {
        if (Tiempo > 0 && !GameManager.Instance.VictoriaMinijuegoCallejon) // Solo restamos tiempo si es mayor a 0
        {
            Tiempo -= Time.deltaTime; // Restamos el tiempo que vaya pasando
            Tiempo = Mathf.Max(Tiempo, 0); // Aseguramos que no baje de 0
            ActualizarTextoTiempo(); // Actualizamos el texto del tiempo
        }
        else if (GameManager.Instance.VictoriaMinijuegoCallejon)
        {
            TextoTiempo.text = "";
        }// Si el tiempo llega a 0, activamos Derrota solo una vez
        else
        {
            Derrota.SetActive(true);
            TextoTiempo.text = "00:00"; // Nos aseguramos de que el texto no muestre números negativos
        }
    }

    void ActualizarTextoTiempo()
    {
        int minutos = Mathf.FloorToInt(Tiempo / 60); // Calculamos los minutos
        int segundos = Mathf.FloorToInt(Tiempo % 60); // Calculamos los segundos
        TextoTiempo.text = string.Format("{0:00}:{1:00}", minutos, segundos); // Mostramos el tiempo en pantalla
    }
}

