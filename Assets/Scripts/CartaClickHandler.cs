using UnityEngine;

public class CartaClickHandler : MonoBehaviour
{
    public GameManager gameManager;

    void OnMouseDown()
    {
        Carta carta = GetComponent<Carta>();

        if (carta != null && !carta.EstaVisible && gameManager != null)
        {
            carta.MostrarCara();
            gameManager.SeleccionarCarta(carta);
        }
    }
}
