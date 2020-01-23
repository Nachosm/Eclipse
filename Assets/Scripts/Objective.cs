using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public abstract class Objective : MonoBehaviour {

    protected bool completed = false;
    [SerializeField]
    protected int objectiveCount = 1;
    [SerializeField]
    protected Text objectiveText;

    [SerializeField]
    private GameObject[] spawnObjectsWhenComplete = new GameObject[] { };

    void Start()
    {
        try
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().AddToObjectives(gameObject.GetComponent<Objective>());
            UpdateText();
        }
        catch (NullReferenceException)
        {
            Debug.Log("Please add the current gameObjects to the Objective.");
        }
        
    }

    protected virtual void UpdateText()
    {
        if (objectiveCount == 1)
            GameObject.Find("Objective1Text").GetComponent<Text>().text = objectiveText.text;
        else
            GameObject.Find("Objective2Text").GetComponent<Text>().text = objectiveText.text;
    }

    public bool Completed
    {
        get { return completed; }
        set { completed = value; }
    }

    protected void SetPortalActive()
    {
        GameObject.Find("Portal").SetActive(true);
    }

    protected void ActivateObjects()
    {
        foreach (GameObject o in spawnObjectsWhenComplete)
        {
            o.transform.GetChild(0).gameObject.SetActive(true);
            Debug.Log("Activate EndOfMap " + o.transform.GetChild(0).name);
        }

    }
}
