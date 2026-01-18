using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardSO : ScriptableObject
{
    public string cardName;
    [TextArea]
    public string cardText;
    public Sprite cardImage;

    public CardEffectType effectType; 

    public float effectValue;
    public bool isUnique;
    public int unlockLevel;
}


public enum CardEffectType
{
    DamageIncrease,
    HealthIncrease,
    SpeedIncrease,
    NewGun
}