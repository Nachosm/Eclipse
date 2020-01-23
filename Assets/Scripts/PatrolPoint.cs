using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : Collidable {
    private string name;
    private Vector3 coordinates;

    protected override void Start()
    {
        base.Start();
        name = transform.name;
        coordinates = transform.position;
    }
    public Vector3 getCoordinates()
    {
        return coordinates;
    }
    public void setCoodrdinates(Vector3 newCoordinates)
    {
        coordinates = newCoordinates;
        //transform.position = coordinates;
        gameObject.transform.position = newCoordinates;
    }
    public string getName()
    {
        return name;
    }
    public void setName(string newName)
    {
        name = newName;
    }
    protected override void OnCollide(Collider2D coll)
    {
        
    }
}
