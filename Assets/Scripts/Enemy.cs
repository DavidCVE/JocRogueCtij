using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform player;
    [SerializeField] float speed = 10f;
    [SerializeField] int health = 100;

    Animator anim;

    [Header("Charger Stats")] // Sectiune noua in Inspector
    public bool isCharger;       // Bifezi asta DOAR la inamicul Charger
    [SerializeField] float distanceToCharge = 5f; // Distanta de la care te simte
    [SerializeField] float chargeSpeed = 20f;     // Viteza cand ataca (mai mare decat speed normal)
    [SerializeField] float prepareTime = 2f;      // Timpul cat sta pe loc

    private bool isCharging;
    private bool isPreparingCharge;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null)
        {
            // 1. Daca se pregateste de atac, NU se misca (iese din functie)
            if (isPreparingCharge) return;

            // 2. Logica pentru Charger: E aproape? Nu ataca deja? E Charger?
            if (isCharger && !isCharging && Vector3.Distance(transform.position, player.position) < distanceToCharge)
            {
                isPreparingCharge = true;
                Invoke("StartCharging", prepareTime); // Cheama functia de atac dupa 2 secunde
            }

            // 3. Miscare (Codul tau vechi)
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    // Aceasta functie se activeaza automat dupa "prepareTime" secunde
    void StartCharging()
    {
        isPreparingCharge = false; // Nu se mai pregateste
        isCharging = true;         // Acum ataca efectiv
        speed = chargeSpeed;       // Ii dam viteza mare!
    }

    public void Hit(int damage)
    {
        health -= damage;

        if (anim != null)
        {
            anim.SetTrigger("Hit");
        }

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Aici va veni logica de damage player mai tarziu
        }
    }
}