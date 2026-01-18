using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer cardImageRenderer;
    [SerializeField] TextMeshPro cardTextRenderer;

    // Trebuie să fie 'public' pentru ca CardManager să îl poată citi când dăm click
    public CardSO cardInfo;

    public void Setup(CardSO card)
    {
        cardInfo = card;
        if (cardImageRenderer != null) cardImageRenderer.sprite = card.cardImage;
        if (cardTextRenderer != null) cardTextRenderer.text = card.cardText;
    }

    // Am șters OnMouseDown pentru că nu mai este necesar.
    // CardManager se ocupă acum de detectarea click-ului prin Raycast.
}