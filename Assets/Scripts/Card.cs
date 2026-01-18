using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer cardImageRenderer;
    [SerializeField] TextMeshPro cardTextRenderer;

    public CardSO cardInfo;

    public void Setup(CardSO card)
    {
        cardInfo = card;
        if (cardImageRenderer != null) cardImageRenderer.sprite = card.cardImage;
        if (cardTextRenderer != null) cardTextRenderer.text = card.cardText;
    }


}