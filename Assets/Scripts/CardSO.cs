using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class CardSO : ScriptableObject
{
    public string cardName;
    [TextArea]
    public string cardText;
    public Sprite cardImage;

    public CardEffectType effectType; // Variabila care folosește lista

    public float effectValue;
    public bool isUnique;
    public int unlockLevel;
}

// Aceasta este lista pe care trebuie să o modifici:
public enum CardEffectType
{
    DamageIncrease,
    HealthIncrease,
    SpeedIncrease,
    NewGun // <--- Asigură-te că e aici și are virgulă dacă mai urmează ceva, sau fără dacă e ultimul
}