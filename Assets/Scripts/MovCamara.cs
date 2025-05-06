using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovCamara : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    void Start()
        {
            GameObject jugador = GameObject.FindWithTag("Player");
            if (jugador != null && virtualCamera != null)
            {
                virtualCamera.Follow = jugador.transform;
               
            }
        }
    }
    

   