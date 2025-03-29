using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Poner esta configuracion para hacer transición entre escenas (Todas las escenas)


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool ObjetoObtenido = false;
    public bool TutorialRealizado = false;
    public bool CajaObtenida = false;
    public bool mensajeCoger = false;
    public bool mensajeDejar = false;
    public bool HablarNPC = false;
    public CajasCOM.TipoCaja Caja = CajasCOM.TipoCaja.Nada;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Debug.Log("Más de un Game Manager en escena!");
            Destroy(gameObject);
        }
       
       
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public  void CogerObjeto()
    {
        ObjetoObtenido=true;
    }

    public void CajaEntregada()
    {

        CajaObtenida = false;
    }
    public void RecogerCaja(GameObject cajaInteractiva) //Función para almacenar que objeto hemos clickado
    {

        CajasCOM caja = cajaInteractiva.GetComponent<CajasCOM>();
        Caja = caja.Caja;
    }
    public void TransicionLobby()
    {
        int escenaActual = SceneManager.GetActiveScene().buildIndex; 
        int lobby = 0;
        SceneManager.LoadScene(lobby); //Cargame la siguiente escena.
    }
    public void ReiniciarNivel()
    {
        int escenaActual = SceneManager.GetActiveScene().buildIndex; 
        SceneManager.LoadScene(escenaActual); //Cargame la siguiente escena.
    }
    

  
}
