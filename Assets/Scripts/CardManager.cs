using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [SerializeField] GameObject cardSelectionUI;
    [SerializeField] GameObject cardPrefab;
    [SerializeField] Transform cardPositionOne;
    [SerializeField] Transform cardPositionTwo;
    [SerializeField] Transform cardPositionThree;
    [SerializeField] List<CardSO> deck; // Lista tuturor cărților disponibile

    // Referințe către obiectele cărților afișate momentan pe ecran
    GameObject cardOne, cardTwo, cardThree;

    // Listă pentru a ține evidența cărților unice deja alese de jucător
    List<CardSO> alreadySelectedCards = new List<CardSO>();

    void RandomizeNewCards()
    {
        // Ștergem cărțile vechi de pe ecran înainte de a genera altele noi
        if (cardOne != null) Destroy(cardOne);
        if (cardTwo != null) Destroy(cardTwo);
        if (cardThree != null) Destroy(cardThree);

        List<CardSO> randomizedCards = new List<CardSO>();

        // Creăm o listă cu cărțile disponibile, filtrându-le pe cele unice deja deținute
        List<CardSO> availableCards = new List<CardSO>(deck);
        availableCards.RemoveAll(card =>
            card.isUnique && alreadySelectedCards.Contains(card)
        );

        // Verificăm dacă avem destule cărți în pachet pentru a alege 3
        if (availableCards.Count < 3)
        {
            Debug.Log("Not enough available cards");
            return;
        }

        // Alegem 3 cărți diferite din lista celor disponibile
        while (randomizedCards.Count < 3)
        {
            CardSO randomCard = availableCards[Random.Range(0, availableCards.Count)];
            // Verificăm să nu adăugăm aceeași carte de două ori în selecția curentă
            if (!randomizedCards.Contains(randomCard))
            {
                randomizedCards.Add(randomCard);
            }
        }

        // Instanțiem cărțile pe pozițiile stabilite în UI
        cardOne = InstantiateCard(randomizedCards[0], cardPositionOne);
        cardTwo = InstantiateCard(randomizedCards[1], cardPositionTwo);
        cardThree = InstantiateCard(randomizedCards[2], cardPositionThree);
    }

    GameObject InstantiateCard(CardSO cardSO, Transform position)
    {
        // Creăm obiectul cărții și îl punem ca sub-obiect al poziției respective
        GameObject cardGo = Instantiate(cardPrefab, position.position, Quaternion.identity, position);

        // Luăm componentul "Card" de pe prefab și îl configurăm cu datele din CardSO
        // Notă: Trebuie să ai un script numit "Card" pe prefab-ul tău care să aibă metoda "Setup"
        Card card = cardGo.GetComponent<Card>();
        card.Setup(cardSO);

        return cardGo;
    }
}