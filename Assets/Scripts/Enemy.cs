using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform player;
    [SerializeField] float speed = 10f;
    [SerializeField] int health = 100;

    Animator anim;

    [Header("Charger Stats")] 
    public bool isCharger;       
    [SerializeField] float distanceToCharge = 5f; 
    [SerializeField] float chargeSpeed = 20f;     
    [SerializeField] float prepareTime = 2f;      

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

            if (isPreparingCharge) return;


            if (isCharger && !isCharging && Vector3.Distance(transform.position, player.position) < distanceToCharge)
            {
                isPreparingCharge = true;
                Invoke("StartCharging", prepareTime); 
            }


            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }


    void StartCharging()
    {
        isPreparingCharge = false; 
        isCharging = true;         
        speed = chargeSpeed;       
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
            
        }
    }
}