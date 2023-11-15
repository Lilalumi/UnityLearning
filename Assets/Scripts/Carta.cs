using UnityEngine;

public class Carta : MonoBehaviour
{
    public Sprite caraVisible; // Imagen de la cara visible de la carta
    public Sprite dorso; // Imagen del dorso de la carta

    private bool estaVisible = false; // Indica si la cara de la carta está visible

    void Start()
    {
        MostrarDorso(); // Al inicio, mostramos el dorso de la carta

        // Obtén el tamaño del sprite
        Vector2 spriteSize = GetComponent<SpriteRenderer>().bounds.size;

        // Ajusta el tamaño del Box Collider 2D
        GetComponent<BoxCollider2D>().size = spriteSize;
    }

    void OnMouseDown()
    {
        if (!estaVisible)
        {
            MostrarCara(); // Si la carta no está visible, la mostramos al hacer clic
        }
    }

    void MostrarCara()
    {
        GetComponent<SpriteRenderer>().sprite = caraVisible;
        estaVisible = true;
    }

    void MostrarDorso()
    {
        GetComponent<SpriteRenderer>().sprite = dorso;
        estaVisible = false;
    }
}
