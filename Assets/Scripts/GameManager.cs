using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private Carta cartaActual = null;
    private int cartasDestruidas = 0;
    private bool generandoNuevasCartas = false;

    public Carta[] tiposDeCartas; // Asigna los diferentes tipos de cartas desde el inspector
    public int filas = 4;
    public int columnas = 4;
    public float espacioEntreCartas = 1.0f; // Variable editable para el espacio entre cartas
    public Tablero tablero;

    void Start()
    {
        GenerarTablero();
    }

    public void SeleccionarCarta(Carta carta)
    {
        if (cartaActual == null)
        {
            cartaActual = carta;
        }
        else
        {
            ComprobarPareja(carta);
        }
    }

    private void ComprobarPareja(Carta segundaCarta)
    {
        if (cartaActual.name == segundaCarta.name)
        {
            StartCoroutine(AnimacionAcierto(cartaActual, segundaCarta));
        }
        else
        {
            StartCoroutine(AnimacionError(cartaActual, segundaCarta));
        }

        cartaActual = null;
    }

    private System.Collections.IEnumerator AnimacionAcierto(Carta carta1, Carta carta2)
    {
        yield return new WaitForSeconds(0.5f);

        float tiempoDeAnimacion = 1.0f;
        float tiempoPasado = 0.0f;

        while (tiempoPasado < tiempoDeAnimacion)
        {
            carta1.transform.localScale += new Vector3(0.01f, 0.01f, 0);
            carta2.transform.localScale += new Vector3(0.01f, 0.01f, 0);

            tiempoPasado += Time.deltaTime;
            yield return null;
        }

        Destroy(carta1.gameObject);
        Destroy(carta2.gameObject);

        cartasDestruidas += 2;

        if (cartasDestruidas >= (filas * columnas)/2)
        {
            Debug.Log("Se destruyeron todas las cartas. Generando nuevo tablero...");
            GenerarTablero();
        }
    }

    private System.Collections.IEnumerator AnimacionError(Carta carta1, Carta carta2)
    {
        yield return new WaitForSeconds(0.5f);

        carta1.DarVuelta();
        carta2.DarVuelta();

        float tiempoDeAnimacion = 1.0f;
        float tiempoPasado = 0.0f;
        Vector3 posicionInicial1 = carta1.transform.position;
        Vector3 posicionInicial2 = carta2.transform.position;

        while (tiempoPasado < tiempoDeAnimacion)
        {
            float vibracion = Mathf.Sin(Time.time * 20f) * 0.1f;
            carta1.transform.position = new Vector3(posicionInicial1.x + vibracion, posicionInicial1.y, posicionInicial1.z);
            carta2.transform.position = new Vector3(posicionInicial2.x + vibracion, posicionInicial2.y, posicionInicial2.z);

            tiempoPasado += Time.deltaTime;
            yield return null;
        }

        carta1.transform.position = posicionInicial1;
        carta2.transform.position = posicionInicial2;
    }

    private void GenerarTablero()
    {
        // Lógica para generar el nuevo tablero
        cartasDestruidas = 0;
        generandoNuevasCartas = false;

        // Implementa la lógica para generar el tablero inicial aquí
        tablero.GenerarTablero();
    }
}
