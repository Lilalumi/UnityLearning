using UnityEngine;
using System.Collections.Generic;

public class Tablero : MonoBehaviour
{
    public Carta[] tiposDeCartas; // Asigna los diferentes tipos de cartas desde el inspector
    public int filas = 4;
    public int columnas = 4;

    public float espacioEntreCartas = 1.0f; // Variable editable para el espacio entre cartas

    void Start()
    {
        GenerarTablero();
    }

    void GenerarTablero()
        {
            // Creamos una lista de cartas duplicadas
        List<Carta> cartasDuplicadas = new List<Carta>();

        // Duplicamos cada tipo de carta
        foreach (Carta tipoDeCarta in tiposDeCartas)
        {
            cartasDuplicadas.Add(tipoDeCarta);
            cartasDuplicadas.Add(tipoDeCarta);
        }

        // Mezclamos las cartas en la lista utilizando la extensión Shuffle
        cartasDuplicadas.Shuffle();

        // Crea un objeto contenedor para las cartas
        GameObject contenedorCartas = new GameObject("Cartas");
        contenedorCartas.transform.parent = transform; // Hace que el objeto contenedor sea hijo del objeto Tablero

        // Calcula la posición central del tablero
        float centroX = columnas * espacioEntreCartas / 2f - espacioEntreCartas / 2f;
        float centroY = -filas * espacioEntreCartas / 2f + espacioEntreCartas / 2f;

        // Coloca las cartas en el contenedor
        for (int fila = 0; fila < filas; fila++)
        {
            for (int columna = 0; columna < columnas; columna++)
            {
                int index = fila * columnas + columna;
                if (index < cartasDuplicadas.Count)
                {
                    Carta nuevaCarta = Instantiate(cartasDuplicadas[index], new Vector3(columna * espacioEntreCartas - centroX, -fila * espacioEntreCartas + centroY, 0), Quaternion.identity);
                    nuevaCarta.transform.parent = contenedorCartas.transform; // Hace que la carta sea hija del objeto contenedor

                    // Añade un script CartaClickHandler a cada carta
                    CartaClickHandler clickHandler = nuevaCarta.gameObject.AddComponent<CartaClickHandler>();
                    clickHandler.gameManager = FindObjectOfType<GameManager>();
                }
            }
        }
    }
}
