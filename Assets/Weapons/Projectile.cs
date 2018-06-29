using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    const float DESTROY_DELAY = 0.01f;

    [SerializeField] float projectileSpeed;
    [SerializeField] GameObject shooter;    //inspectable when paused

    float damageCaused;

    public void SetShooter(GameObject shooter)
    {
        this.shooter = shooter;
    }

    public void SetDamage(float damage)
    {
        damageCaused = damage;
    }

    void OnCollisionEnter(Collision collision)
    {
        var collisionObject = collision.gameObject;
        if (!GameObject.Equals(collisionObject.layer, shooter.layer))    //projectiles discriminate between layers
        {
            DamageIfDamageable(collisionObject);
        }

    }

    private void DamageIfDamageable(GameObject collisionObject)
    {
        Component damagableComponent = collisionObject.GetComponent(typeof(IDamageable));
        if (damagableComponent)
        {
            (damagableComponent as IDamageable).TakeDamage(damageCaused);
        }
        Destroy(gameObject, DESTROY_DELAY);
    }

    internal float GetDefaultLaunchSpeed()
    {
        return projectileSpeed;
    }
}
