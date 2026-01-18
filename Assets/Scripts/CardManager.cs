using System.Collections.Generic;
using UnityEngine;

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

    // Am comentat Start pentru ca cardurile să apară DOAR la final de Wave, nu la început
    private void Start()
    {
        // RandomizeNewCards(); 
    }

    public void RandomizeNewCards()
    {
        // Ștergem cardurile vechi dacă există
        if (cardOne != null) Destroy(cardOne);
        if (cardTwo != null) Destroy(cardTwo);
        if (cardThree != null) Destroy(cardThree);

        List<CardSO> randomizedCards = new List<CardSO>();
        List<CardSO> availableCards = new List<CardSO>(deck);

        // --- COD CORECTAT AICI ---
        // S-a schimbat 'unlocklevel' în 'unlockLevel' pentru a elimina eroarea CS1061
        availableCards.RemoveAll(card =>
            (card.isUnique && alreadySelectedCards.Contains(card)) ||
            (GameManager.Instance != null && card.unlockLevel > GameManager.Instance.GetCurrentLevel())
        );

        if (availableCards.Count < 3)
        {
            UnityEngine.Debug.Log("Not enough available cards");
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

        // Creăm obiectele pe pozițiile setate în Inspector
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
}