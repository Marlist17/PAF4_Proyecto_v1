using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPuertasNv3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(GameManager.Instance.baldosaRota == 6)
        {
            Debug.Log("Has conseguido superar el primer nivel");
            Debug.Log("Puerta del nv 1 abriendose...");
            GameManager.Instance.baldosaRota = 0; //Reseteamos para los proximos niveles.
        }
        else if (GameManager.Instance.baldosaRota == 30)
        {
            Debug.Log("Has conseguido superar el segundo nivel");
            Debug.Log("Puerta del nv 2 abriendose...");
            GameManager.Instance.baldosaRota = 0; //Reseteamos para los proximos niveles.
        }
        else if (GameManager.Instance.baldosaRota == 55)
        {
            Debug.Log("Has conseguido superar el tercer nivel");
            Debug.Log("Puerta del nv 3 abriendose...");
            GameManager.Instance.baldosaRota = 0; //Reseteamos para los proximos niveles.
        }
    }
}
