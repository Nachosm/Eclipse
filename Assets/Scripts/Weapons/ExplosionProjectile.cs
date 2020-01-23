using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionProjectile : Projectile {

    [SerializeField]
    protected float radius = 1f;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter" || coll.tag == "Blocking")
        {
            if (coll.name == "PlayerHitBox")
                return;

            Damage dmg = new Damage
            {
                damageAmount = damagePoint,
                origin = transform.position,
                pushForce = this.pushForce
            };

            Collider2D[] colliders;
            colliders = Physics2D.OverlapCircleAll(transform.position, radius);
            
            foreach(Collider2D c in colliders)
            {
                if (c.tag == "Fighter")
                    SendMessageUpwards("ReceiveDamage", dmg);
                Debug.Log(c.name);
            }
            Destroy(gameObject);
        }
    }
}
