using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeeThrough : MonoBehaviour {

    private int skipOverlapCircleAllForSeeThrough = ~((1 << 0) | (1 << 12) | (1 << 10));
    private int skipRaycastLayersForSeeThrough = ~((1 << 0) | (1 << 10) | (1 << 11) | (1 << 12));

    protected Transform FindAllTargets(float length)
    {
        Transform[] targets = new Transform[] { };
        Collider2D[] colliders;
        colliders = Physics2D.OverlapCircleAll(transform.position, length, skipOverlapCircleAllForSeeThrough);
        if (colliders != null)
        {
            RaycastHit2D vision;
            foreach (var c in colliders)
            {
                vision = Physics2D.Raycast(transform.position, c.transform.position - transform.position, Mathf.Infinity, skipRaycastLayersForSeeThrough);
            }
        }
        

        return null;
    }
}
