using System.Collections;
using UnityEngine;

[System.Serializable]
public class CombinacionAcierto
{
    public Sprite imagenCarta1;
    public Sprite imagenCarta2;
}

public class Tablero : MonoBehaviour
{
    public GameObject cartaPrefab; // Prefab de la carta
    public CombinacionAcierto[] combinacionesAcierto; // Lista de combinaciones de aciertos

    private Carta[] cartasReveladas = new Carta[2];
    private int cantidadCartasReveladas = 0;

    // Método para comprobar las cartas reveladas
    public void ComprobarCarta(Carta carta)
    {
        if (cantidadCartasReveladas < 2)
        {
            cartasReveladas[cantidadCartasReveladas] = carta;
            cantidadCartasReveladas++;

            if (cantidadCartasReveladas == 2)
            {
                StartCoroutine(CompararCartas());
            }
        }
    }

    // Método para comparar las cartas reveladas
    private IEnumerator CompararCartas()
    {
        yield return new WaitForSeconds(1f); // Esperar un momento para que el jugador vea las cartas

        Sprite imagenCarta1 = cartasReveladas[0].GetComponent<SpriteRenderer>().sprite;
        Sprite imagenCarta2 = cartasReveladas[1].GetComponent<SpriteRenderer>().sprite;

        if (SonAciertos(imagenCarta1, imagenCarta2))
        {
            // Las cartas son aciertos, destruir las cartas
            Debug.Log("¡Acierto!");

            // Obtener las posiciones de las últimas cartas destruidas
            Vector3 posicionNuevaCarta1 = cartasReveladas[0].transform.position;
            Vector3 posicionNuevaCarta2 = cartasReveladas[1].transform.position;

            // Generar nuevas cartas en las mismas posiciones
            GameObject nuevaCarta1 = Instantiate(cartaPrefab, posicionNuevaCarta1, Quaternion.identity);
            GameObject nuevaCarta2 = Instantiate(cartaPrefab, posicionNuevaCarta2, Quaternion.identity);

            // Configurar las nuevas cartas como hijas del Tablero
            nuevaCarta1.transform.parent = transform;
            nuevaCarta2.transform.parent = transform;

            // Ajusta la cantidad según tu diseño específico
          
            // Destruir las cartas originales
            Destroy(cartasReveladas[0].gameObject);
            Destroy(cartasReveladas[1].gameObject);
        }
        else
        {
            // Las cartas no son aciertos, volver al dorso
            cartasReveladas[0].VoltearCarta();
            cartasReveladas[1].VoltearCarta();
        }

        // Limpiar el arreglo y reiniciar el contador
        cartasReveladas = new Carta[2];
        cantidadCartasReveladas = 0;
    }

    // Método para verificar si las cartas son aciertos
    private bool SonAciertos(Sprite imagenCarta1, Sprite imagenCarta2)
    {
        // Verificar las combinaciones específicas de aciertos definidas en el Inspector
        foreach (var combinacion in combinacionesAcierto)
        {
            if ((combinacion.imagenCarta1 == imagenCarta1 && combinacion.imagenCarta2 == imagenCarta2) || 
                (combinacion.imagenCarta1 == imagenCarta2 && combinacion.imagenCarta2 == imagenCarta1))
            {
                return true;
            }
        }

        return false;
    }
}
