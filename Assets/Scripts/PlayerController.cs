using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float shipSpeed = 10f;
    public float projectileSpeed = 10f;
    public GameObject projectilePrefab;
    public float firingRate = 0.2f;

    public float health = 500f;
    public GameObject enemyProjectile;
    public float enemyProjectileSpeed = 10f;
    public float enemyFiringrate = 100f;
    public float enemyFiringtime = 1f;

    private float xmin = -5;
    private float xmax = 5;
    private Vector3 shipSize;

    void Start()
    {
        float distance = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));
        shipSize = GetComponent<Transform>().localScale;
        xmin = leftmost.x + (shipSize.x / 2);
        xmax = rightmost.x - (shipSize.x / 2);
    }
    void Fire()
    {
        GameObject beam = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.00000000001f, firingRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
        if (Input.GetKey("left"))
        {
            transform.position += Vector3.left * shipSpeed * Time.deltaTime;
        }
        else if (Input.GetKey("right"))
        {
            transform.position += Vector3.right * shipSpeed * Time.deltaTime;
        }
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        EnemyProjectile enemyMissile = collider.gameObject.GetComponent<EnemyProjectile>();
        if (enemyMissile)
        {
            Debug.Log("Player gets hit");
            health -= enemyMissile.EnemyGetDamage();
            enemyMissile.EnemyHit();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
