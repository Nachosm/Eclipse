using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRVOObstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Get the simulator for this scene
        Pathfinding.RVO.Simulator sim = (FindObjectOfType(typeof(Pathfinding.RVO.RVOSimulator)) as Pathfinding.RVO.RVOSimulator).GetSimulator();

        //Define the vertices of our obstacle
        Vector3[] verts = new Vector3[] { new Vector3(1, 0, -1), new Vector3(1, 0, 1), new Vector3(-1, 0, 1), new Vector3(-1, 0, -1) };

        //Add our obstacle to the simulation, we set the height to 2 units
        sim.AddObstacle(verts, 2);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
