using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; 

public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    [SerializeField] GameObject cardSelectionUI;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform cardPositionOne;
    [SerializeField] Transform cardPositionTwo;
    [SerializeField] Transform cardPositionThree;
    [SerializeField] List<CardSO> deck;

    private GameObject cardOne, cardTwo, cardThree;
    private List<CardSO> alreadySelectedCards = new List<CardSO>();

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (cardSelectionUI.activeSelf)
        {
            if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
            {
                DetectCardClick();
            }
        }
    }

    void DetectCardClick()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();

        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null)
        {
            Card clickedCard = hit.collider.GetComponent<Card>();

            if (clickedCard != null)
            {
                SelectCard(clickedCard.cardInfo);
            }
        }
    }

    public void RandomizeNewCards()
    {
        if (cardOne != null) Destroy(cardOne);
        if (cardTwo != null) Destroy(cardTwo);
        if (cardThree != null) Destroy(cardThree);

        List<CardSO> randomizedCards = new List<CardSO>();
        List<CardSO> availableCards = new List<CardSO>(deck);

        availableCards.RemoveAll(card =>
            (card.isUnique && alreadySelectedCards.Contains(card)) ||
            (GameManager.Instance != null && card.unlockLevel > GameManager.Instance.GetCurrentLevel())
        );

        if (availableCards.Count < 3)
        {
            UnityEngine.Debug.Log("Not enough available cards");
            GameManager.Instance.ResumeAfterCardSelection();
            return;
        }

        while (randomizedCards.Count < 3)
        {
            CardSO randomCard = availableCards[UnityEngine.Random.Range(0, availableCards.Count)];
            if (!randomizedCards.Contains(randomCard))
            {
                randomizedCards.Add(randomCard);
            }
        }

        cardOne = InstantiateCard(randomizedCards[0], cardPositionOne);
        cardTwo = InstantiateCard(randomizedCards[1], cardPositionTwo);
        cardThree = InstantiateCard(randomizedCards[2], cardPositionThree);
    }

    GameObject InstantiateCard(CardSO cardSO, Transform position)
    {
        GameObject cardGo = Instantiate(cardPrefab, position.position, Quaternion.identity, position);

        Card cardScript = cardGo.GetComponent<Card>();

        if (cardScript != null)
        {
            cardScript.Setup(cardSO);
        }

        return cardGo;
    }

    public void SelectCard(CardSO selectedCard)
    {
        if (selectedCard.isUnique)
        {
            alreadySelectedCards.Add(selectedCard);
        }

        Player player = FindObjectOfType<Player>();


        GunManager gunManager = FindObjectOfType<GunManager>();

        if (player != null)
        {
            switch (selectedCard.effectType)
            {
                case CardEffectType.DamageIncrease:
                    UnityEngine.Debug.Log("Damage crescut!");

                    // Asigură-te că această linie NU are // în față:
                    player.damage += (int)selectedCard.effectValue;
                    break;

                case CardEffectType.HealthIncrease:
                    player.maxHealth += (int)selectedCard.effectValue;
                    player.Heal((int)selectedCard.effectValue);
                    break;

                case CardEffectType.SpeedIncrease:
                    player.moveSpeed += selectedCard.effectValue;
                    break;


                case CardEffectType.NewGun:
                    if (gunManager != null)
                    {
                        gunManager.AddGun();
                        UnityEngine.Debug.Log("Arma noua adaugata!");
                    }
                    break;
            }
        }

        GameManager.Instance.ResumeAfterCardSelection();
    }
}