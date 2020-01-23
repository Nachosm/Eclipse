using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class KillObjective : Objective {

    [SerializeField]
    private string[] killToComplete = new string[] { };
    [SerializeField]
    private int[] killToCompleteCount = new int[] { };

    private string[] killTexts = new string[] { "", "", ""};

    void Update()
    {
        for(int i = 0; i < killToComplete.Length; i++)
        {
            if (killToComplete[i] == "zombie" && GameManager.instance.KilledZombiesCount >= killToCompleteCount[i] && completed == false)
            {
                completed = true;
                ActivateObjects();
                Debug.Log("You killed " + killToCompleteCount[i] + " zombies, that's enough.");
                
                
            }
            killTexts[i] = GameManager.instance.KilledZombiesCount + "/" + killToCompleteCount[i].ToString() + "\t";
        }
        ModifyObjectiveTextsEnd(killTexts);
    }


    private void ModifyObjectiveTextsEnd(string[] modifiers)
    {
        try
        {
            string addToText = "";
            foreach (string i in modifiers)
            {
                addToText += i;
            }
            if (objectiveCount == 1)
            {
                GameObject.Find("Objective1Text").GetComponent<Text>().text = objectiveText.text + "\r\n\r\n" + addToText;
            }
        } catch (NullReferenceException)
        {

        }
        
    } 

    protected override void UpdateText()
    {
        if (objectiveCount == 1)
        {
            GameObject.Find("Objective1Text").GetComponent<Text>().text = objectiveText.text + "\r\n" + 
                GameManager.instance.KilledZombiesCount + "/" + killToComplete;
        }
            
        else
            GameObject.Find("Objective2Text").GetComponent<Text>().text = objectiveText.text;
    }

}
