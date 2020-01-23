using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Collidable, RandomID {

    protected int damagePoint = 1;
    protected float pushForce = 0f;
    //protected int ID = 0;

    protected override void OnCollide(Collider2D coll)
    {

        if (coll.tag == "Fighter" && coll.name == "HitBox")
        {
            if (coll.name == "PlayerHitBox")
                return;

            //Create a new damage object, then we'll send it to the fighter we've hit
            Damage dmg = new Damage
            {
                damageAmount = this.damagePoint,//GameManager.instance.pistol.damagePoint[GameManager.instance.pistol.weaponLevel],
                origin = transform.position,
                pushForce = this.pushForce,//GameManager.instance.pistol.pushForce[GameManager.instance.pistol.weaponLevel]
                id = GenerateRandomID(5000)
            };
            //Debug.Log(coll.name);
            coll.SendMessageUpwards("ReceiveDamage", dmg);
            Destroy(gameObject);

        }
        if (coll.tag == "Blocking")
        {
            Destroy(gameObject);
        }
    }

    public int GenerateRandomID(int maxNumber)
    {
        return int.Parse(Mathf.Round(Random.Range(1, maxNumber)).ToString());
    }

    /*
    protected virtual void ActivateProjectile(bool isCollideWithEnemy)
    {
        if (isCollideWithEnemy)
        {

        }
        Damage dmg = new Damage
        {
            damageAmount = this.damagePoint,//GameManager.instance.pistol.damagePoint[GameManager.instance.pistol.weaponLevel],
            origin = transform.position,
            pushForce = this.pushForce//GameManager.instance.pistol.pushForce[GameManager.instance.pistol.weaponLevel]
        };
        
        Destroy(gameObject);
    }*/

    public void setDamage(int newDamagePoint)
    {
        damagePoint = newDamagePoint;
    }
    public int getDamagePoint()
    {
        return damagePoint;
    }

    public void setPushForce(float newPushForce)
    {
        pushForce = newPushForce;
    }
    public float getPushForce()
    {
        return pushForce;
    }

}
