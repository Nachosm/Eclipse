using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakingPoint : Collidable {

    public bool collidingWithAlly = false;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.gameObject.layer == 11 || coll.gameObject.layer == 8)
            collidingWithAlly = true;
        else
            collidingWithAlly = false;

        Debug.Log("AwakingPoint: " + collidingWithAlly);
    }
    protected override void Start()
    {
        collidingWithAlly = false;
        base.Start();

    }

    public bool getCollidingWithAlly()
    {
        return collidingWithAlly;
    }

    public void setCollidingWithAlly(bool isCollidingWithAlly)
    {
        collidingWithAlly = isCollidingWithAlly;
    }

}
