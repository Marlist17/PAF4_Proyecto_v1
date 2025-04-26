using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //Poner esta configuracion para hacer transición entre escenas (Todas las escenas)


public class ReyesDialogos : MonoBehaviour
{
    [SerializeField] Dialogos dialog;
    private string[] lines =
{
    "Mm hm hm...",
    "Pero bueno... ¡Freud, Keaton! Mirad qué cosita tan mona ha venido a visitarnos...",
    "¿No os dan ganas de espachurrarla y sentir cómo sus órganos palpitan bajo vuestras yemas?",
    "¡Desde luego, jefe! ¡Y mira esos bigotillos! Me apetece arrancarlos y hacerme una pulserita con ellos.",
    "...",
    "Bienvenida, cariño, ponte cómoda.",
    "Sí, sí... sé que vives aquí. Disculpa nuestra impertinencia, somos unos maleducados...",
    "Pero, a pesar de ello... estoy seguro de que te sonamos de algo, ¿no es así?",
    "Pst... jefe, creo que se le ha comido la lengua el gato...",
    "Uy... qué traviesa... ¿y quién ha sido el afortunado?",
    "Jefe, no desvaríe.",
    "Tienes razón, tienes razón... por dónde iba... ¡ah, sí!",
    "Tres reyes.",
    "Tres seres inmarcesibles, inexorables, in...evitables.",
    "Tres semidioses portentosos que dan vueltas sin un sino bajo los decadentes mandatos del espacio y del tiempo.",
    "Una sola forma cambiante que se alimenta del ciclo perpetuo de la vida.",
    "Tú, pequeña, cuya forma efímera nos aleja de la debacle de los mundos, tú eres la salvadora. Tú eres la elegida.",
    "Y es aquí donde tu última aventura da comienzo.",
    "Me ha quedado bonito, ¿eh? Se me da bien la verborrea.",
    "¡Ya te digo tío! Lo único que he entendido ha sido 'pequeña'.",
    "Y 'tres', creo.",
    "Bueno, lo que quiero decir, sin tanto circunloquio, es que tus tres reyes te han elegido para cumplir una serie de misiones MUY importantes...",
    "Nos vas a hacer casito, ¿verdad, cariñín?",
    "Bueno, tampoco es como si tuvieras otra opción... deberías sentirte agradecida, ¡honrada!, ¡llena de júbilo por tener la oportunidad de complacernos tan gratamente!",
    "Nunca jamás tendrás una oportunidad tan grande y valiosa como esta... ¿a qué no, Freud?",
    "...",
    "Ciertamente, no la tendrás.",
    "Pues eso, arreando que es gerundio.",
    "Una cosa más.",
    "No te molestes en contarle esto a nadie. Eres la única que puede vernos.",
    "Y oírnos.",
    "¡Y olernos!",
    "Puede olernos, ¿no?",
    "...",
    "Habla con Keaton, él te dará la primera misión.",
    "Buena suerte, gata."
}; // Nuestras frases
    private string Cabecilla = "Schrödinger";
    private string Tonti = "Keaton";
    private string Listo = "Freud";
    bool conversacionFinalizada = false;
    bool jugadorEnRango = false; // Variable para detectar si el jugador está en el trigger
    public int escenaActual;

    void Start()
    {
        dialog.FueradeRango();
        escenaActual = SceneManager.GetActiveScene().buildIndex;

    }

    void Update()
    {
       
        dialog.PasarDialogo();
        
        // Verifica si el jugador está en el área y presiona "E"
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
        if (indiceDialogo == 3) return Tonti;    // Primera línea
        else if (indiceDialogo == 4) return Listo; // Tercera línea (índice 2)
        else if (indiceDialogo > 4 && indiceDialogo < 8) return Cabecilla; // Quinta línea (índice 4)
        else if (indiceDialogo == 8) return Tonti; // Sexta línea (índice 5)
        else if (indiceDialogo == 9) return Cabecilla; // Séptima línea (índice 6)
        else if (indiceDialogo == 10) return Listo; // Octava línea (índice 7)
        else if (indiceDialogo > 10 && indiceDialogo < 18) return Cabecilla; // Novena línea (índice 8)
        else if (indiceDialogo > 18 && indiceDialogo < 21) return Tonti; // Décima línea (índice 9)
        else if (indiceDialogo > 20 && indiceDialogo < 25) return Cabecilla; // Corregido aquí
        else if (indiceDialogo > 24 && indiceDialogo < 27) return Listo; // Undécima línea (índice 10)
        else if (indiceDialogo == 27) return Cabecilla; // Duodécima línea (índice 11)
        else if (indiceDialogo > 27 && indiceDialogo < 30) return Listo; // Decimotercera línea (índice 12)
        else if (indiceDialogo == 30) return Cabecilla; // Decimocuarta línea (índice 13)
        else if (indiceDialogo > 30 && indiceDialogo < 33) return Tonti; // Decimoquinta línea (índice 14)
        else if (indiceDialogo > 32) return Cabecilla; // Decimosexta línea (índice 15)
        return Cabecilla; // Añadido valor por defecto por si algún caso no está cubierto


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


