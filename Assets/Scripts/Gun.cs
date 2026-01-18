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


        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        if (player == null) return;


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


        GameObject proj = Instantiate(projectilePrefab, muzzlePosition.position, transform.rotation);
        Projectile projScript = proj.GetComponent<Projectile>();
        Player playerScript = FindObjectOfType<Player>();

        
        if (projScript != null && playerScript != null)
        {
            projScript.SetDamage(playerScript.damage);
        }
        Destroy(proj, 3f);


        if (muzzleFlashPrefab != null)
        {
            GameObject flash = Instantiate(muzzleFlashPrefab, muzzlePosition.position, transform.rotation);
            flash.transform.SetParent(transform);
            Destroy(flash, 0.1f);
        }
    }
}