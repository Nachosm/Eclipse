using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider2D))]
public class Collidable : MonoBehaviour {
    public ContactFilter2D filter;
    [SerializeField]
    protected bool usesBoxCollider = true;
    protected BoxCollider2D boxCollider;
    [SerializeField]
    protected bool usesCircleCollider = false;
    protected CircleCollider2D circleCollider;
    protected Collider2D[] hits = new Collider2D[10];

    protected virtual void Start()
    {
        if (usesBoxCollider)
        {
            usesCircleCollider = false;
            boxCollider = GetComponent<BoxCollider2D>();
        }
        else if (usesCircleCollider)
        {
            circleCollider = GetComponent<CircleCollider2D>();
            usesBoxCollider = false;
        }
        else if (!usesBoxCollider && !usesCircleCollider)
        {
            usesBoxCollider = true;
        }
            

        
    }

    protected virtual void Update()
    {
        //Collision work
        if (usesBoxCollider)
            boxCollider.OverlapCollider(filter, hits);
        else
            circleCollider.OverlapCollider(filter, hits);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;
            OnCollide(hits[i]);
            //The array is not cleaned up, so we do it ourself

           hits[i] = null;
        }

    }
    protected virtual void OnCollide(Collider2D coll)
    {
        Debug.Log("OnCOllide was not implemented in " + this.name);
    }
}
