using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_M4 : RangedWeapon {
    


    protected override void Attack_0()
    {
        if (repertoryCurrent1 > 0)
        {
            CreateProjectile(ProjectileStartingTransform.position, projectile1, velocity);
            
        }
        else if (ammoAmount1 > 0)
        {
            repertoryChange1();
        }
            
    }
    protected override void Attack_1()
    {
        //CreateProjectile(transform.position, projectile2, velocity);
    }
    
}
