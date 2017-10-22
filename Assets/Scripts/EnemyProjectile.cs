using UnityEngine;
using System.Collections;

public class EnemyProjectile : MonoBehaviour {

    public float enemyProjectileDamage = 100f;

public float EnemyGetDamage()
    {
        return enemyProjectileDamage;
    }

public void EnemyHit()
    {
        Destroy(gameObject);
    }
}
