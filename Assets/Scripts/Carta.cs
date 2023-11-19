using UnityEngine;
using System.Collections;

public class Carta : MonoBehaviour
{
    public Sprite[] imagenesFrente; // Lista de imágenes para la cara frontal
    public Sprite imagenDorso; // Imagen fija para el dorso
    private bool mostrandoFrente = false;
    private bool primeraVez = true; // Variable para controlar la primera vez que se hace clic

    // Variable para almacenar la primera imagen de frente seleccionada
    private Sprite primeraImagenFrente;

    // Velocidad de la animación de giro
    public float velocidadGiro = 5f;

    // Referencia al script del Tablero
    private Tablero tablero;

    // Método para inicializar la carta
private void Start()
{
    // Configurar el dorso al inicio
    mostrandoFrente = false;
    primeraImagenFrente = imagenesFrente[Random.Range(0, imagenesFrente.Length)];
    ActualizarImagen();

    // Obtener la referencia al script del Tablero
    tablero = FindObjectOfType<Tablero>();

    if (tablero == null)
    {
        Debug.LogError("No se encontró el objeto Tablero en la escena.");
    }
}

    // Método para cambiar entre el dorso y el frente al hacer clic
    private void OnMouseDown()
    {
        if (primeraVez)
        {
            primeraVez = false;
        }
        else
        {
            mostrandoFrente = !mostrandoFrente;
            // Agregar animación de giro al hacer clic
            StartCoroutine(GirarCarta());

            // Notificar al Tablero sobre la carta revelada solo si la referencia al tablero no es nula
            if (tablero != null)
            {
                tablero.ComprobarCarta(this);
            }
        }
        ActualizarImagen();
    }

    // Método para obtener la imagen frontal actual
    public Sprite GetImagenFrente()
    {
        return primeraImagenFrente;
    }

    // Método para la animación de giro
    private IEnumerator GirarCarta()
    {
        float anguloInicial = transform.rotation.eulerAngles.y;
        float anguloFinal = anguloInicial + 180f;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * velocidadGiro;
            float anguloActual = Mathf.Lerp(anguloInicial, anguloFinal, t);
            transform.rotation = Quaternion.Euler(0f, anguloActual, 0f);
            yield return null;
        }

        // Ajustar la rotación final para evitar problemas de precisión
        transform.rotation = Quaternion.Euler(0f, anguloFinal, 0f);
        
        // Actualizar la imagen después de la animación
        ActualizarImagen();
    }

    // Método para volver la carta al dorso
    public void VoltearCarta()
    {
        mostrandoFrente = false;
        ActualizarImagen();
    }

    // Método para actualizar la imagen de la carta según si está mostrando el dorso o el frente
    private void ActualizarImagen()
    {
        if (mostrandoFrente)
        {
            // Mostrar la primera imagen de frente seleccionada al hacer clic
            GetComponent<SpriteRenderer>().sprite = primeraImagenFrente;

            // Ajustar la rotación para corregir posibles inversiones en el eje Y
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            // Mostrar la imagen fija del dorso al iniciar la escena o al hacer clic
            GetComponent<SpriteRenderer>().sprite = imagenDorso;
        }
    }
}
