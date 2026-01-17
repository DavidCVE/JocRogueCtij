using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer cardImageRenderer; //
    [SerializeField] TextMeshPro cardTextRenderer; //

    private CardSO cardInfo; //

    public void Setup(CardSO card)
    {
        cardInfo = card; //
        cardImageRenderer.sprite = card.cardImage; //
        cardTextRenderer.text = card.cardText; //
    }

    private void OnMouseDown()
    {
        // Această metodă va fi apelată când dai click pe card în joc
        UnityEngine.Debug.Log("Ai dat click pe cardul: " + cardInfo.cardText); //
    }
}