using UnityEngine;
using TMPro;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer cardImageRenderer;
    [SerializeField] TextMeshPro cardTextRenderer;

    private CardSO cardInfo;

    public void Setup(CardSO card)
    {
        cardInfo = card;
        if (cardImageRenderer != null) cardImageRenderer.sprite = card.cardImage;
        if (cardTextRenderer != null) cardTextRenderer.text = card.cardText;
    }

    private void OnMouseDown()
    {
        // Trimitem bonusul către jucător
        if (Player.instance != null)
        {
            Player.instance.ApplyPowerUp(cardInfo);
        }

        // Revenim la joc prin GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResumeAfterCardSelection();
        }

        // Folosim UnityEngine.Debug pentru a evita eroarea CS0104
        UnityEngine.Debug.Log("Card selectat cu succes!");
    }
}