using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardSO : ScriptableObject
{
    public Sprite cardImage; //
    public string cardText; //
    public CardEffect effectType; //

    // Variabile necesare pentru filtrarea din CardManager
    public float effectValue; //
    public bool isUnique; //
    public int unlockLevel; //
}

public enum CardEffect
{
    DamageIncrease,
    HealthIncrease,
    AttackSpeedIncrease
}