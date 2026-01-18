using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    [SerializeField] int damage = 25;

    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    void Start()
    {

        UnityEngine.Debug.Log("Glont lansat!");


        Destroy(gameObject, 3f);
    }

    void Update()
    {

        transform.position += transform.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        UnityEngine.Debug.Log("Am atins: " + collision.name);


        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Hit(damage);  
            Destroy(gameObject); 
            return; 
        }


        if (collision.name.Contains("Wall") || collision.name.Contains("Border"))
        {
            Destroy(gameObject);
            return;
        }

    }
}