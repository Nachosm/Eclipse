using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class Boss_Test : Enemy {
public class Boss_Test : Zombie {

    public float[] swordSpeed = { 2.5f, -2.5f };
    public float distance = 0.25f;
    public Transform[] swords;

    protected override void Update()
    {
        base.Update();
        for (int i = 0; i < swords.Length; i++)
        {
            swords[i].position = transform.position + new Vector3(-Mathf.Cos(Time.time * swordSpeed[i]) * distance, Mathf.Sin(Time.time * swordSpeed[i]) * distance, 0);
        }
            

        
    }
}
