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

    // Această metodă lipsea și de aceea nu vedeai cardurile la început!
    private void Start()
    {
        RandomizeNewCards(); // Apelează generarea la pornirea jocului
    }

    public void RandomizeNewCards()
    {
        if (cardOne != null) Destroy(cardOne);
        if (cardTwo != null) Destroy(cardTwo);
        if (cardThree != null) Destroy(cardThree);

        List<CardSO> randomizedCards = new List<CardSO>();
        // Ne asigurăm că lista de cărți disponibile este creată din pachetul (deck) din Inspector
        List<CardSO> availableCards = new List<CardSO>(deck);

        // Filtrare cărți unice
        availableCards.RemoveAll(card => card.isUnique && alreadySelectedCards.Contains(card)
        || card.unlocklevel >GameManager.Instance.GetCurrentLevel());

        if (availableCards.Count < 3)
        {
            UnityEngine.Debug.Log("Not enough available cards");
            return;
        }

        while (randomizedCards.Count < 3)
        {
            // Folosim Random.Range pentru a alege o carte aleatorie
            CardSO randomCard = availableCards[UnityEngine.Random.Range(0, availableCards.Count)];
            if (!randomizedCards.Contains(randomCard))
            {
                randomizedCards.Add(randomCard);
            }
        }

        // Instanțiem cele 3 cărți pe pozițiile lor
        cardOne = InstantiateCard(randomizedCards[0], cardPositionOne);
        cardTwo = InstantiateCard(randomizedCards[1], cardPositionTwo);
        cardThree = InstantiateCard(randomizedCards[2], cardPositionThree);
    }

    GameObject InstantiateCard(CardSO cardSO, Transform position)
    {
        // Crearea vizuală a cardului în scenă
        GameObject cardGo = Instantiate(cardPrefab, position.position, Quaternion.identity, position);
        Card cardScript = cardGo.GetComponent<Card>();

        if (cardScript != null)
        {
            cardScript.Setup(cardSO); // Trimite datele (imagine, text) către scriptul Card
        }

        return cardGo;
    }
}