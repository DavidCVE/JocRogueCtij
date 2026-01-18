using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("UI & Components")]
    [SerializeField] TextMeshProUGUI healthText;
    Animator anim;
    Rigidbody2D rb;

    [Header("Stats")]

    public float moveSpeed = 6f;
    public int maxHealth = 100;
    public int damage = 25;

    int currentHealth;
    bool dead = false;


    Vector2 movement;
    int facingDirection = 1;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        UpdateHealthUI();
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
            {
                transform.GetChild(0).localScale = new Vector2(facingDirection, 1);
            }
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Hit(20);
        }
    }

    public void Hit(int damage)
    {
        if (dead) return;
        currentHealth -= damage;
        anim.SetTrigger("Hit");
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

  
    public void Heal(int amount)
    {
        if (dead) return;
        currentHealth += amount;
        if (currentHealth > maxHealth) currentHealth = maxHealth;
        UpdateHealthUI();
    }

    void Die()
    {
        dead = true;
        UnityEngine.Debug.Log("Game Over!");
        if (GameManager.Instance != null) GameManager.Instance.GameOver();
    }

    void UpdateHealthUI()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        if (healthText != null)
        {
            healthText.text = currentHealth.ToString();
        }
    }
}