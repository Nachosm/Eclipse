using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MeeleWeapon {

    //Swing
    protected Animator anim;

    protected int tmp = 0;
    protected Transform shoot;

    
    //public int mouseButtonToActivate;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();


        shoot = transform.parent.parent.transform;
        /*
        transform.GetChild(0).position = GameObject.Find("pistol").transform.position;
        transform.GetChild(0).rotation = GameObject.Find("pistol").transform.rotation;*/
        //transform.parent.parent.parent.GetComponent<Player>().AddWeapon(gameObject);

        //Debug.Log(transform.parent.parent.parent);
    }


    protected override void Update()
    {
        base.Update();
        shoot = transform.parent.parent.transform;

    }
    protected override void Attack_0()
    {
        if (!GameManager.instance.getPause())
            anim.enabled = true;
        else if (GameManager.instance.getPause())
            anim.enabled = false;

        dmg = new Damage
        {
            damageAmount = damagePoint[weaponLevel],
            origin = transform.position,
            pushForce = pushForce[weaponLevel],
            id = GenerateRandomID(5000)
        };
        if (tmp == 0)
        {
            anim.SetTrigger("Swing");
            tmp = 1;
        }
        else if (tmp == 1)
        {
            anim.SetTrigger("Swing_LTR");
            tmp = 0;
        }

        
    }

    protected override void Attack_1()
    {
        //CreateProjectile(new Vector3(transform.position.x, transform.position.y, transform.position.z - 90));
        //CreateProjectile(ProjectileStartingPosition.position);
    }


}