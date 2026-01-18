using UnityEngine;
using UnityEngine.InputSystem; // Adăugat pentru a rezolva eroarea de Input

public class Player : MonoBehaviour
{
    public static Player instance;

    [Header("Player Stats")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] int maxHealth = 100;
    int currentHealth;

    private void Awake()
    {
        if (instance == null) instance = this;
        currentHealth = maxHealth;
    }

    void Update()
    {
        // REPARARE EROARE INPUT: Folosim Keyboard.current în loc de Input.GetAxisRaw
        Vector2 moveInput = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) moveInput.y = 1;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) moveInput.y = -1;
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) moveInput.x = -1;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) moveInput.x = 1;
        }

        // Mișcarea normalizată pentru a nu merge mai repede pe diagonală
        transform.position += (Vector3)moveInput.normalized * moveSpeed * Time.deltaTime;
    }

    public void ApplyPowerUp(CardSO card)
    {
        // REPARARE EROARE DEBUG: Folosim UnityEngine.Debug pentru a evita conflictul
        if (card.cardText.Contains("health"))
        {
            currentHealth += 20;
            if (currentHealth > maxHealth) currentHealth = maxHealth;
            UnityEngine.Debug.Log("Viata marita! HP actual: " + currentHealth);
        }
        else if (card.cardText.Contains("Speed"))
        {
            moveSpeed += 1.5f;
            UnityEngine.Debug.Log("Viteza marita! Viteza actuala: " + moveSpeed);
        }
    }
}