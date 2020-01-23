using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SarahDriving : Collidable, RandomID
{
    bool driving = false;

    protected override void Start()
    {
        if (usesBoxCollider)
        {
            usesCircleCollider = false;
            boxCollider = transform.GetChild(0).GetComponent<BoxCollider2D>();
        }
        else if (usesCircleCollider)
        {
            circleCollider = transform.GetChild(0).GetComponent<CircleCollider2D>();
            usesBoxCollider = false;
        }
        else if (!usesBoxCollider && !usesCircleCollider)
        {
            usesBoxCollider = true;
        }
    }

    // Update is called once per frame
    protected override void Update () {
        base.Update();
        try
        {
            if (driving)
            {
                GameObject.Find("Player").transform.position = transform.GetChild(0).transform.position;
            }
        }
        catch (NullReferenceException) { }
        
	}

    protected override void OnCollide(Collider2D coll)
    {
        
        if(coll.name == "PlayerHitBox" && !driving)
        {
            transform.GetComponent<Animator>().SetTrigger("GetIn");
            driving = true;
            Debug.Log("Colliding with Player");
            GameObject.Find("PlayerHitBox").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("PlayerHitBox").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("PlayerHitBox").transform.GetChild(1).gameObject.SetActive(false);


        }
        if (coll.name == "HitBox")
        {
            
            Damage dmg = new Damage
            {
                damageAmount = 10,//GameManager.instance.pistol.damagePoint[GameManager.instance.pistol.weaponLevel],
                origin = transform.position,
                pushForce = 0,//GameManager.instance.pistol.pushForce[GameManager.instance.pistol.weaponLevel]
                id = GenerateRandomID(5000)
            };

            coll.SendMessageUpwards("ReceiveDamage", dmg);
        }


    }
    public int GenerateRandomID(int maxNumber)
    {
        return int.Parse(Mathf.Round(UnityEngine.Random.Range(1, maxNumber)).ToString());
    }
}
