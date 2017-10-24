using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public float enemyHealth = 150f;
    public GameObject enemyProjectile;
    public float enemyProjectileSpeed = 10f;
    public float enemyFirerate = 0.5f;
    public float enemyFiringtime = 1f;
    public int scoreValue = 150;

    private ScoreKeeper highscoreUi;
    private AudioSource enemyFire;
    private AudioSource enemyDie;

    void Start()
    {
        highscoreUi = GameObject.Find("Score").GetComponent<ScoreKeeper>();
        enemyFire = GameObject.Find("EnemyFireSound").GetComponent<AudioSource>();
        enemyDie = GameObject.Find("EnemyDieSound").GetComponent<AudioSource>();
    }

    void EnemyFire()
    {
        GameObject enemyBeam = Instantiate(enemyProjectile, transform.position, Quaternion.identity) as GameObject;
        enemyBeam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -enemyProjectileSpeed, 0);
        enemyFire.Play();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Projectile missile = collider.gameObject.GetComponent<Projectile>();
        GameObject.Find("Score").GetComponent<ScoreKeeper>();
        if (missile)
        {
            enemyHealth -= missile.GetDamage();
            missile.Hit();
            if (enemyHealth <= 0)
            {
                Destroy(gameObject);
                enemyDie.Play();
                highscoreUi.Score(scoreValue);
            }
        }
    }
    void Update()
    {
        float firerateProbability = Time.deltaTime * enemyFirerate;
        if (Random.value < firerateProbability)
        {
            EnemyFire();
        }
    }
}
