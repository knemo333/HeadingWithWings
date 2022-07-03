using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_SplashArchon : Bullet
{
    const string ENEMY = "ENEMY";
    [HideInInspector]
    public float splashRange = 2.0f;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ENEMY))
        {
            Collider[] hitColliders = Physics.OverlapSphere(other.transform.position, splashRange);
            foreach (var hitCollider in hitColliders)
            {
                hitCollider.GetComponent<EnemyTakeDamage>().TakeDamage(other.transform, damage, knockbackSize);
            }
            Destroy(gameObject);
        }
    }
}
