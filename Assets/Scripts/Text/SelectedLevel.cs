using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedLevel : TMPro.TMP_TextEventHandler
{

    [SerializeField]
    private string scene;

    private string selectedMap = "";

    protected override void Awake()
    {
        base.Awake();
    }

    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ActivateLevel()
    {
        if (unlocked)
        {
            int index = transform.parent.GetSiblingIndex();
            MenuManager.instance.SelectedLevelIndex = index;
            /*foreach (var l in MenuManager.instance.Levels)
            {
                l.Value.Selected = false;
                if (l.Value == this)
                {
                    //Debug.Log(l.Value);
                    HighScoreEntity highscore = new HighScoreEntity(l.Key._map,
                                                l.Key._highScore,
                                                l.Key._respectScore,
                                                l.Key._isUnlocked,
                                                l.Key._lastPlayed);
                    MenuManager.instance.SetLevelDataGUI(l.Key._highScore, l.Key._respectScore, l.Key._lastPlayed);
                }
            }*/
            foreach (var l in MenuManager.instance.Levels)
            {
                l.Selected = false;
                if (l == this)
                {
                    //Debug.Log(l.Value);
                    selectedMap = transform.parent.name;
                    Debug.Log(selectedMap);
                    /*HighScore readHs = new HighScore();
                    System.Data.IDataReader reader = readHs.getDataByString(selectedMap);

                    GameObject.Find("Score").GetComponent<TMPro.TextMeshProUGUI>().text = reader[1].ToString();
                    GameObject.Find("Respect Score").GetComponent<TMPro.TextMeshProUGUI>().text = reader[2].ToString();
                    GameObject.Find("Last Played Time").GetComponent<TMPro.TextMeshProUGUI>().text = reader[4].ToString();*/
                    //MenuManager.instance.SetLevelDataGUI(int.Parse(reader[1].ToString()), int.Parse(reader[2].ToString()), reader[4].ToString());
                }
            }
            selected = true;
            MenuManager.instance.Scene = scene;
            //Debug.Log(MenuManager.instance.Levels[currentLevel]);
            
        }
    }
}
