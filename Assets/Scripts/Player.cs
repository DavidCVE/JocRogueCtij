using TMPro;
using UnityEngine;
using UnityEngine.InputSystem; // Necesar pentru New Input System

public class Player : MonoBehaviour
{
    [Header("UI & Components")]
    [SerializeField] TextMeshProUGUI healthText;
    Animator anim;
    Rigidbody2D rb;

    [Header("Stats")]
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] int maxHealth = 100;
    int currentHealth;
    bool dead = false;

    // Variabile pentru miscare
    Vector2 movement;
    int facingDirection = 1;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Initializam viata la pornire
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    // --- NEW INPUT SYSTEM ---
    // Aceasta functie primeste automat input-ul de la tastatura/gamepad
    void OnMove(InputValue value)
    {
        if (dead) return;
        movement = value.Get<Vector2>();
    }

    private void Update()
    {
        // 1. Daca e mort, oprim tot
        if (dead)
        {
            movement = Vector2.zero;
            anim.SetFloat("velocity", 0);
            return;
        }

        // 2. Trimitem viteza catre Animator (pentru tranzitia Idle <-> Run)
        anim.SetFloat("velocity", movement.magnitude);

        // 3. Rotirea (Flip)
        // Aici este modificarea importanta: Rotim DOAR copilul (Grafica), nu tot Player-ul
        if (movement.x != 0)
        {
            facingDirection = movement.x > 0 ? 1 : -1;

            // Verificam daca exista un copil (Grafica/Sprites) si il rotim pe el
            if (transform.childCount > 0)
            {
                transform.GetChild(0).localScale = new Vector2(facingDirection, 1);
            }
        }
        
    }

    private void FixedUpdate()
    {
        // Mutarea fizica a jucatorului
        if (dead)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = movement * moveSpeed;
    }

    // --- LOGICA DE COMBAT (Din video) ---

    // Detectam coliziunile fizice (Min 11:44)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificam daca ne-am lovit de un INAMIC
        // Asigura-te ca obiectul Enemy are Tag-ul "Enemy" setat in Inspector!
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Primim 20 damage (sau cat vrei tu)
            Hit(20);
        }
    }

    // Functia de primire damage
    public void Hit(int damage)
    {
        if (dead) return;

        currentHealth -= damage;

        // Declansam animatia de lovitura (Trebuie sa ai Trigger-ul "Hit" in Animator)
        anim.SetTrigger("Hit");

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Functia de moarte
    void Die()
    {
        dead = true;
        // Folosim UnityEngine explicit pentru a evita confuzia cu System.Diagnostics
        UnityEngine.Debug.Log("Game Over! Player is dead.");
        GameManager.instance.GameOver();
        // Aici poti adauga logica de final de joc (ecran Game Over, Restart etc.)
    }

    // Actualizarea textului de pe ecran
    void UpdateHealthUI()
    {
        // Ne asiguram ca viata ramane intre 0 si 100
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthText != null)
        {
            healthText.text = currentHealth.ToString();
        }
    }
}