using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

    public float projectileDamage = 100f;

    public float GetDamage()
    {
        return projectileDamage;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
