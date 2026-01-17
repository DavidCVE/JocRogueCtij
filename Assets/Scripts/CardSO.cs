using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardSO : ScriptableObject
{
    public Sprite cardImage;
    public string cardText;
    public CardEffect effectType;
    public float effectValue; // Cât damage/viață dă cardul
    public bool isUnique;    // Dacă apare o singură dată
    public int unlockLevel;  // La ce nivel se deblochează
}

public enum CardEffect
{
    DamageIncrease
}