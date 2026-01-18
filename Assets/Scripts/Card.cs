using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer cardImageRenderer;
    [SerializeField] TextMeshPro cardTextRenderer;

    private CardSO cardInfo;

    public void Setup(CardSO card)
    {
        cardInfo = card;
        cardImageRenderer.sprite = card.cardImage;
        cardTextRenderer.text = card.cardText;
    }

    // Această funcție se declanșează când dai click pe card
    // ATENȚIE: Cardul trebuie să aibă un BoxCollider2D!
    private void OnMouseDown()
    {
        // 1. Aplicăm bonusul jucătorului folosind Singleton-ul din clasa Player
        if (Player.instance != null)
        {
            Player.instance.ApplyPowerUp(cardInfo);
            UnityEngine.Debug.Log("Ai ales cardul: " + cardInfo.cardText);
        }

        // 2. Închidem meniul de carduri și reluăm timpul jocului
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResumeAfterCardSelection();
        }
    }
}