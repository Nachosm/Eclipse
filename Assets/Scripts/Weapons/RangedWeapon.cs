using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapons {
    //ProjectileTypes
    [SerializeField]
    protected Rigidbody2D projectile1;
    [SerializeField]
    protected Rigidbody2D projectile2;
    [SerializeField]
    protected List<float> grabbings = new List<float>();
    [SerializeField]
    private float cooldownGrabbing = 0.1f;
    protected float nextGrabbing = 0.0f;
    protected int grabbingIndex = 0;
    private bool gainGrabbingIndex = true;
    
    

    

    //Distance to come
    [SerializeField]
    protected float timeToDestroy;
    [SerializeField]
    protected float velocity = 7f;

    protected override void Start()
    {
        base.Start();
        grabbingIndex = grabbings.IndexOf(0);
            
    }

    protected override void Update()
    {
        base.Update();
        nextGrabbing += Time.deltaTime;
    }

    protected override void OnCollide(Collider2D coll)
    {
       
    }
    /*
    protected void CreateProjectile(Vector3 position, Rigidbody2D projectile, float velocity)
    {

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - position.x, mousePosition.y - position.y);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Rigidbody2D clone;
        clone = Instantiate(projectile, position, rotation) as Rigidbody2D;
        clone.velocity = transform.TransformDirection(Vector2.right * velocity);

        //if (clone.tag != "Fighter")
        Destroy(clone.gameObject, timeToDestroy);

    }*/

    protected void DriveBack()
    {

        transform.Translate(Vector2.down * Time.deltaTime);
        transform.Translate(Vector2.up * Time.deltaTime);

    }

    protected void CreateProjectile(Vector3 position, Rigidbody2D projectile, float velocity)
    {

        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - position.x, mousePosition.y - position.y);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        Rigidbody2D clone;
        clone = Instantiate(projectile, position, rotation) as Rigidbody2D;
        //Debug.Log("Time.time: " + Time.time);
        if (Time.time >= nextGrabbing)
        {
            nextGrabbing = 0.0f;
            //Switch if we achive the end of the list
            if (grabbings.Count != 0 && grabbingIndex == grabbings.Count - 1)
                gainGrabbingIndex = false;
            else if (grabbings.Count != 0 && grabbingIndex == 0)
                gainGrabbingIndex = true;
            //Make Bullet direction
            clone.velocity = transform.TransformDirection((Vector2.right + new Vector2(0, grabbings[grabbingIndex])) * velocity);
            //Gaing Index
            if (gainGrabbingIndex)
                grabbingIndex++;
            else
                grabbingIndex--;
        }
        else
        {
            grabbingIndex = grabbings.IndexOf(0);
            //Make Bullet direction
            clone.velocity = transform.TransformDirection((Vector2.right + new Vector2(0, grabbings[grabbingIndex])) * velocity);
        }
        
        repertoryCurrent1--;
        GameManager.instance.UpdateRepertory(repertoryCurrent1);

        Destroy(clone.gameObject, timeToDestroy);

    }

    protected void repertoryChange1()
    {
        int tmp = ammoAmount1;
        if ((ammoAmount1 + repertoryCurrent1) >= repertoryCapacity1)
        {
            repertoryCurrent1 = repertoryCapacity1;
            ammoAmount1 -= repertoryCapacity1;
        } 
        else
        {
            repertoryCurrent1 += ammoAmount1;
            ammoAmount1 = 0;
        }
        GameManager.instance.UpdateAmmo(ammoAmount1);       
    }
}
