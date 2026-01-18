using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardSO : ScriptableObject
{
    public Sprite cardImage;
    public string cardText;
    public CardEffect effectType;

    public float effectValue;
    public bool isUnique;
    public int unlockLevel;  // Verifică să fie scris exact așa
}

public enum CardEffect
{
    DamageIncrease,
    HealthIncrease,
    AttackSpeedIncrease
}