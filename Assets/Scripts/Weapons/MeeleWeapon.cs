using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleWeapon : Weapons, RandomID {

    protected Damage dmg;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter" && coll.name == "HitBox")
        {
            if (coll.name == "PlayerHitBox") //Change "Player"
                return;

            //Create a new damage object, then we'll send it to the fighter we've hit
            /*
            Damage dmg = new Damage
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel],
                id = GenerateRandomID(5000)
            };*/
            coll.SendMessageUpwards("ReceiveDamage", dmg);
        }

    }

    public int GenerateRandomID(int maxNumber)
    {
        return int.Parse(Mathf.Round(Random.Range(1, maxNumber)).ToString());
    }

}
