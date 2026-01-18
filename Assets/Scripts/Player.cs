using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static Player instance; // Singleton pentru acces ușor din scriptul Card

    [Header("UI & Components")]
    [SerializeField] TextMeshProUGUI healthText;
    Animator anim;
    Rigidbody2D rb;

    [Header("Stats")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] int maxHealth = 100;
    public int damage = 20; // Variabilă nouă pentru atac
    int currentHealth;
    bool dead = false;

    Vector2 movement;
    int facingDirection = 1;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    // Funcția care aplică bonusul ales de pe card
    public void ApplyPowerUp(CardSO card)
    {
        switch (card.effectType)
        {
            case CardEffect.DamageIncrease:
                damage += (int)card.effectValue;
                UnityEngine.Debug.Log("Atac mărit! Nou damage: " + damage);
                break;
            case CardEffect.HealthIncrease:
                currentHealth += (int)card.effectValue;
                if (currentHealth > maxHealth) maxHealth = currentHealth;
                UpdateHealthUI();
                UnityEngine.Debug.Log("Viață mărită! Nouă viață: " + currentHealth);
                break;
        }
    }

    void OnMove(InputValue value)
    {
        if (dead) return;
        movement = value.Get<Vector2>();
    }

    private void Update()
    {
        if (dead)
        {
            movement = Vector2.zero;
            anim.SetFloat("velocity", 0);
            return;
        }
        anim.SetFloat("velocity", movement.magnitude);

        if (movement.x != 0)
        {
            facingDirection = movement.x > 0 ? 1 : -1;
            if (transform.childCount > 0)
                transform.GetChild(0).localScale = new Vector2(facingDirection, 1);
        }
    }

    private void FixedUpdate()
    {
        if (dead)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }
        rb.linearVelocity = movement * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) Hit(20);
    }

    public void Hit(int damageAmount)
    {
        if (dead) return;
        currentHealth -= damageAmount;
        anim.SetTrigger("Hit");
        UpdateHealthUI();
        if (currentHealth <= 0) Die();
    }

    void Die()
    {
        dead = true;
        UnityEngine.Debug.Log("Game Over!");
        GameManager.Instance.GameOver();
    }

    void UpdateHealthUI()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (healthText != null) healthText.text = currentHealth.ToString();
    }
}