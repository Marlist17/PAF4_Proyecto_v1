using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Poner esta configuracion para hacer transici�n entre escenas (Todas las escenas)


public class ReyesDialogos : MonoBehaviour
{
    [SerializeField] Dialogos dialog;
    private string[] lines =
{
    "Mm hm hm...",
    "Pero bueno... �Freud, Keaton! Mirad qu� cosita tan mona ha venido a visitarnos...",
    "�No os dan ganas de espachurrarla y sentir c�mo sus �rganos palpitan bajo vuestras yemas?",
    "�Desde luego, jefe! �Y mira esos bigotillos! Me apetece arrancarlos y hacerme una pulserita con ellos.",
    "...",
    "Bienvenida, cari�o, ponte c�moda.",
    "S�, s�... s� que vives aqu�. Disculpa nuestra impertinencia, somos unos maleducados...",
    "Pero, a pesar de ello... estoy seguro de que te sonamos de algo, �no es as�?",
    "Pst... jefe, creo que se le ha comido la lengua el gato...",
    "Uy... qu� traviesa... �y qui�n ha sido el afortunado?",
    "Jefe, no desvar�e.",
    "Tienes raz�n, tienes raz�n... por d�nde iba... �ah, s�!",
    "Tres reyes.",
    "Tres seres inmarcesibles, inexorables, in...evitables.",
    "Tres semidioses portentosos que dan vueltas sin un sino bajo los decadentes mandatos del espacio y del tiempo.",
    "Una sola forma cambiante que se alimenta del ciclo perpetuo de la vida.",
    "T�, peque�a, cuya forma ef�mera nos aleja de la debacle de los mundos, t� eres la salvadora. T� eres la elegida.",
    "Y es aqu� donde tu �ltima aventura da comienzo.",
    "Me ha quedado bonito, �eh? Se me da bien la verborrea.",
    "�Ya te digo t�o! Lo �nico que he entendido ha sido 'peque�a'.",
    "Y 'tres', creo.",
    "Bueno, lo que quiero decir, sin tanto circunloquio, es que tus tres reyes te han elegido para cumplir una serie de misiones MUY importantes...",
    "Nos vas a hacer casito, �verdad, cari��n?",
    "Bueno, tampoco es como si tuvieras otra opci�n... deber�as sentirte agradecida, �honrada!, �llena de j�bilo por tener la oportunidad de complacernos tan gratamente!",
    "Nunca jam�s tendr�s una oportunidad tan grande y valiosa como esta... �a qu� no, Freud?",
    "...",
    "Ciertamente, no la tendr�s.",
    "Pues eso, arreando que es gerundio.",
    "Una cosa m�s.",
    "No te molestes en contarle esto a nadie. Eres la �nica que puede vernos.",
    "Y o�rnos.",
    "�Y olernos!",
    "Puede olernos, �no?",
    "...",
    "Habla con Keaton, �l te dar� la primera misi�n.",
    "Buena suerte, gata."
}; // Nuestras frases
    private string Cabecilla = "Schr�dinger";
    private string Tonti = "Keaton";
    private string Listo = "Freud";
    bool conversacionFinalizada = false;
    bool jugadorEnRango = false; // Variable para detectar si el jugador est� en el trigger
    public int escenaActual;

    void Start()
    {
        dialog.FueradeRango();
        escenaActual = SceneManager.GetActiveScene().buildIndex;

    }

    void Update()
    {
       
        dialog.PasarDialogo();
        
        // Verifica si el jugador est� en el �rea y presiona "E"
        if (jugadorEnRango && Input.GetKeyDown(KeyCode.E))
        {
            if (escenaActual == 3)
            {
                GameManager.Instance.HablarNPC = true;
                dialog.LimpiarDialogos();

            }
            int indiceActual = dialog.index;
            string nombre = determinarNombreSegunIndice(indiceActual);
            
            dialog.MostrarNombre(nombre);
            conversacionFinalizada = dialog.ComenzarDialogo(lines, conversacionFinalizada);
        }
        if (dialog.DialogoActivo)
        {
            int indiceActual = dialog.index;
            string nombre = determinarNombreSegunIndice(indiceActual);
            dialog.MostrarNombre(nombre);
        }
    }
    public string determinarNombreSegunIndice(int indiceDialogo)
    {
        if (indiceDialogo < 3)
            return Cabecilla;
        if (indiceDialogo == 3) return Tonti;    // Primera l�nea
        else if (indiceDialogo == 4) return Listo; // Tercera l�nea (�ndice 2)
        else if (indiceDialogo > 4 && indiceDialogo < 8) return Cabecilla; // Quinta l�nea (�ndice 4)
        else if (indiceDialogo == 8) return Tonti; // Sexta l�nea (�ndice 5)
        else if (indiceDialogo == 9) return Cabecilla; // S�ptima l�nea (�ndice 6)
        else if (indiceDialogo == 10) return Listo; // Octava l�nea (�ndice 7)
        else if (indiceDialogo > 10 && indiceDialogo < 18) return Cabecilla; // Novena l�nea (�ndice 8)
        else if (indiceDialogo > 18 && indiceDialogo < 21) return Tonti; // D�cima l�nea (�ndice 9)
        else if (indiceDialogo > 20 && indiceDialogo < 25) return Cabecilla; // Corregido aqu�
        else if (indiceDialogo > 24 && indiceDialogo < 27) return Listo; // Und�cima l�nea (�ndice 10)
        else if (indiceDialogo == 27) return Cabecilla; // Duod�cima l�nea (�ndice 11)
        else if (indiceDialogo > 27 && indiceDialogo < 30) return Listo; // Decimotercera l�nea (�ndice 12)
        else if (indiceDialogo == 30) return Cabecilla; // Decimocuarta l�nea (�ndice 13)
        else if (indiceDialogo > 30 && indiceDialogo < 33) return Tonti; // Decimoquinta l�nea (�ndice 14)
        else if (indiceDialogo > 32) return Cabecilla; // Decimosexta l�nea (�ndice 15)
        return Cabecilla; // A�adido valor por defecto por si alg�n caso no est� cubierto


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Verifica si el objeto que entra es el jugador
        if (collision.CompareTag("Player"))
        {
            jugadorEnRango = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            jugadorEnRango = false;
            dialog.FueradeRango();
        }
    }
}


