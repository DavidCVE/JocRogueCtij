using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform muzzlePosition;
    [SerializeField] GameObject muzzleFlashPrefab;

    [Header("Config")]
    [SerializeField] float fireRange = 10f;
    [SerializeField] float fireRate = 0.5f;

    float timeSinceLastShot;
    Transform player;
    Transform closestEnemy;
    Vector2 offset;

    Animator anim;

    void Start()
    {


        // Gasim playerul
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        // Setam offset-ul initial (distanta fata de player)
        // In video el face asta la min 4:28
        //SetOffset(new Vector2(1f, 0.5f));
    }

    void Update()
    {
        if (player == null) return;

        // Punem arma langa player + offset
        transform.position = (Vector2)player.position + offset;

        FindClosestEnemy();
        AimAtEnemy();
        Shooting();
    }

    public void SetOffset(Vector2 o)
    {
        offset = o;
    }

    void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance && distanceToEnemy <= fireRange)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null)
            closestEnemy = nearestEnemy.transform;
        else
            closestEnemy = null;
    }

    void AimAtEnemy()
    {
        if (closestEnemy != null)
        {
            Vector3 direction = (closestEnemy.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void Shooting()
    {
        anim.SetTrigger("Shoot");

        if (closestEnemy == null) return;

        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= fireRate)
        {
            Shoot();
            timeSinceLastShot = 0;
        }

    }

    void Shoot()
    {
        anim.SetTrigger("Shoot");

        // 1. Cream Glontul
        // AICI E MODIFICAREA DIN VIDEO (Min 09:12):
        // Salvam glontul intr-o variabila 'proj' si il distrugem dupa 3 secunde
        GameObject proj = Instantiate(projectilePrefab, muzzlePosition.position, transform.rotation);
        Destroy(proj, 3f);

        // 2. Cream Flash-ul
        if (muzzleFlashPrefab != null)
        {
            GameObject flash = Instantiate(muzzleFlashPrefab, muzzlePosition.position, transform.rotation);
            flash.transform.SetParent(transform);
            Destroy(flash, 0.1f);
        }
    }
}