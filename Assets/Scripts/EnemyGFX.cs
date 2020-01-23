using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour {

    public AIPath aiPath;
    public GameObject target;

    // Update is called once per frame
    void Update () {
        RotateToTarget(target.transform.position);
	}

    protected void RotateToTarget(Vector3 target)
    {
        Vector2 diff = (target - transform.position).normalized;
        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

        Quaternion rotation = new Quaternion();
        rotation.eulerAngles = new Vector3(0, 0, angle - 270); //270
        transform.rotation = rotation;
    }
}
