using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : Collidable
{
    private bool isUsed;
    private string name;
    private List<string> whoUsed;
    private Vector3 coordinates;

    public Vector3 getCoordinates()
    {
        return coordinates;
    }

    public void setCoordinates(Vector3 newCoordinates)
    {
        coordinates = newCoordinates;
    }

    public string getName()
    {
        return name;
    }
    public void setName(string newName)
    {
        name = newName;
    }

    public void AddToWhoUsed(string name)
    {
        whoUsed.Add(name);
    }
    public List<string> GetWhoUsed()
    {
        return whoUsed;
    }

    public void setIsUsed(bool yon)
    {
        isUsed = yon;
    }
    public bool getIsUsed()
    {
        return isUsed;
    }

    protected override void Start()
    {
        base.Start();
        isUsed = false;
        name = transform.name;
        GameManager.instance.waypointNames.Add(this);
        //Debug.Log(GameManager.instance.waypointNames.Count);
        coordinates = transform.position;
        /*for (int i = 0; i < GameManager.instance.waypointNames.Count; i++)
        {
            //Debug.Log(GameManager.instance.waypointNames.Contains(this));
            Debug.Log(GameManager.instance.waypointNames[i].name);
        }*/

    }
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name != "PlayerHitBox" && coll.tag == "Fighter" && isUsed == false)
        {
            isUsed = true;
            //Debug.Log(isUsed + " " + name + " " + coll.transform.parent.name);
        }
            
    }
}
