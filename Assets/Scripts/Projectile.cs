using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    [SerializeField] int damage = 25;

    void Start()
    {
        // Debug
        UnityEngine.Debug.Log("Glont lansat!");

        // Distrugere automata dupa 3 secunde (timp maxim de viata)
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        // Miscare continua
        transform.position += transform.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Vedem in consola exact ce atingem
        UnityEngine.Debug.Log("Am atins: " + collision.name);

        // --- REGULA 1: Daca e INAMIC ---
        // Verificam daca obiectul are scriptul "Enemy"
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Hit(damage);  // Ii dam damage
            Destroy(gameObject); // Distrugem glontul
            return; // Iesim din functie
        }

        // --- REGULA 2: Daca e PERETE (Wall/Border) ---
        // Doar daca numele contine "Wall" sau "Border" distrugem glontul
        if (collision.name.Contains("Wall") || collision.name.Contains("Border"))
        {
            Destroy(gameObject);
            return;
        }

        // --- REGULA 3: ORICE ALTCEVA (Podea, Player, Flori, etc.) ---
        // NU scriem nimic aici. 
        // Daca nu e Inamic si nu e Perete, glontul va trece prin obiect si NU se va distruge.
    }
}