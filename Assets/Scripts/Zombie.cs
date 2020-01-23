using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : NPC {
    //protected Animator anim;

    // Experience
    

    protected override void Death()
    {
        if (corpses.Length > 0)
        Instantiate(corpses[int.Parse(Mathf.Floor(Random.Range(0, corpses.Length)).ToString())], 
            transform.position, Quaternion.Inverse(gameObject.transform.rotation));
        
        Destroy(gameObject);
        GameManager.instance.GrantScore(xpValue);
        GameManager.instance.ShowText("+" + xpValue + " xp", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
        GameManager.instance.KilledZombiesCount++;
    }

    public void setHitpoint(int newHitpoint)
    {
        hitpoint = newHitpoint;
    }



    public void setTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
