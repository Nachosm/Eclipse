using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleHitbox : EnemyHitbox
{
    protected override void OnCollide(Collider2D coll)
    {
        base.OnCollide(coll);
    }
}
