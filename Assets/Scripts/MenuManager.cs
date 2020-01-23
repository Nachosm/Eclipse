using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    public static MenuManager instance;

    private List<SelectedChapter> chapters = new List<SelectedChapter>();
    //private Dictionary<HighScoreEntity, SelectedLevel> levels = new Dictionary<HighScoreEntity, SelectedLevel>();
    private List<SelectedLevel> levels = new List<SelectedLevel>();

    [SerializeField]
    private GameObject sweetHoneTexas;
    [SerializeField]
    private GameObject simphatyForTheGenie;
    [SerializeField]
    private GameObject welcomeToSlavhalla;

    [SerializeField]
    private GameObject chapter1;
    [SerializeField]
    private GameObject chapter2;
    [SerializeField]
    private GameObject chapter3;
    private int selectedChapterIndex;
    private int selectedLevelIndex;


    private string scene = "Level_00";

    //Canvas Items
    [SerializeField]
    private GameObject mainMenu, optionsMenu, campaignMenu, victoryMenu;

    public GameObject MainMenu
    {
        get { return mainMenu; }
        set { mainMenu = value; }
    }

    public GameObject VictoryMenu
    {
        get { return victoryMenu; }
        set { victoryMenu = value; }
    }

    public string Scene
    {
        set { scene = value; }
    }
    /*
    public Dictionary<HighScoreEntity, SelectedLevel> Levels
    {
        get { return levels; }
        set { levels = value; }
    }*/
    public List<SelectedLevel> Levels
    {
        get { return levels; }
        set { levels = value; }
    }

    public List<SelectedChapter> Chapters
    {
        get { return chapters; }
        set { chapters = value; }
    }

    public int SelectedChapterIndex
    {
        get { return selectedChapterIndex; }
        set { selectedChapterIndex = value; }
    }

    public int SelectedLevelIndex
    {
        get { return selectedLevelIndex; }
        set { selectedLevelIndex = value; }
    }

    void Start()
    {

        //
        //Chapter1
        /*
        HighScore mHighScore = new HighScore();
        System.Data.IDataReader reader = mHighScore.getAllData();
        List<HighScoreEntity> highScoreList = new List<HighScoreEntity>();
        int fieldCount = 0;
        while (fieldCount != reader.FieldCount)
        {
            HighScoreEntity highscore = new HighScoreEntity(reader[0].ToString(),
                                        int.Parse(reader[1].ToString()),
                                        int.Parse(reader[2].ToString()),
                                        bool.Parse(reader[3].ToString()),
                                        reader[4].ToString()
                                        );
            //Add the first level data's to the GUI
            if (fieldCount == 0)
                SetLevelDataGUI(highscore._highScore, highscore._respectScore, highscore._lastPlayed);

            if (fieldCount < 5)
                levels.Add(highscore, GameObject.Find("Chapter1").transform.GetChild(fieldCount).transform.GetChild(0).GetComponent<SelectedLevel>());
            else if(fieldCount >= 5 && fieldCount < 10)
                levels.Add(highscore, GameObject.Find("Chapter2").transform.GetChild(fieldCount).transform.GetChild(0).GetComponent<SelectedLevel>());
            else
                levels.Add(highscore, GameObject.Find("Chapter3").transform.GetChild(fieldCount).transform.GetChild(0).GetComponent<SelectedLevel>());
            fieldCount++;
        }*/
        chapters.Add(sweetHoneTexas.transform.GetChild(0).GetComponent<SelectedChapter>());
        chapters.Add(simphatyForTheGenie.transform.GetChild(0).GetComponent<SelectedChapter>());
        chapters.Add(welcomeToSlavhalla.transform.GetChild(0).GetComponent<SelectedChapter>());

        
        if (GameObject.Find("Main Camera"))
            Destroy(GameObject.Find("Main Camera"));

        if (GameObject.Find("GameManager"))
        {
            /*
            GameObject.Find("ParamountMainMenu").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("ParamountOptionsMenu").transform.GetChild(0).gameObject.SetActive(false);
            GameObject.Find("ParamountCampaignMenu").transform.GetChild(0).gameObject.SetActive(false); ;
            GameObject.Find("ParamountVictoryMenu").transform.GetChild(0).gameObject.SetActive(true);
            */
            ToVictoryMenu();
        }

        instance = this;
    }
    void Update()
    {
        //Debug.Log(GameObject.Find("Chapters").transform.GetChild(0).GetComponent<SelectedText>());

        if (GameObject.Find("GameManager") && !GameManager.instance.statsHasBeenSet)
        {
            GameManager.instance.statsHasBeenSet = true;
            //Find Canvas items and setActive to false except Completed
            try
            {
                /*
                mainMenu.SetActive(false);
                optionsMenu.SetActive(false);
                campaignMenu.SetActive(false);
                victoryMenu.SetActive(true);
                */
                ToVictoryMenu();
            }
            catch (NullReferenceException)
            {
                Debug.Log("Please set active the canvas items.");
            }
        }
    }

    public void SetDataValues()
    {
        
        //Chapter1
        /*HighScore mHighScore = new HighScore();
        System.Data.IDataReader reader = mHighScore.getAllData();*/
        //List<HighScoreEntity> highScoreList = new List<HighScoreEntity>();
        int fieldCount = 0;
        //while (fieldCount != reader.FieldCount)
        while (fieldCount != 14)
        {
            /*
            HighScoreEntity highscore = new HighScoreEntity(reader[0].ToString(),
                                        int.Parse(reader[1].ToString()),
                                        int.Parse(reader[2].ToString()),
                                        bool.Parse(reader[3].ToString()),
                                        reader[4].ToString()
                                       );*/
            //Add the first level data's to the GUI
            //if (fieldCount == 0)
            {
                //SetLevelDataGUI(highscore._highScore, highscore._respectScore, highscore._lastPlayed);
                //SetLevelDataGUI(0, 0, "2019.11.21");
            }
               

            if (fieldCount < 5)
                levels.Add(chapter1.transform.GetChild(fieldCount).transform.GetChild(0).GetComponent<SelectedLevel>());
            else if (fieldCount >= 5 && fieldCount < 10)
                levels.Add(chapter2.transform.GetChild(fieldCount).transform.GetChild(0).GetComponent<SelectedLevel>());
            else
                levels.Add(chapter3.transform.GetChild(fieldCount).transform.GetChild(0).GetComponent<SelectedLevel>());
            fieldCount++;
        }
        chapters.Add(GameObject.Find("Chapters").transform.GetChild(0).transform.GetChild(0).GetComponent<SelectedChapter>());
        chapters.Add(GameObject.Find("Chapters").transform.GetChild(1).transform.GetChild(0).GetComponent<SelectedChapter>());
        chapters.Add(GameObject.Find("Chapters").transform.GetChild(2).transform.GetChild(0).GetComponent<SelectedChapter>());
        Debug.Log(chapters.Count);
    }

    public void SetLevelDataGUI(int highscore, int respectscore, string lastplayed)
    {
            if (lastplayed != "")
            {
                GameObject.Find("Highscore").transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = highscore.ToString();
                GameObject.Find("Last Played").transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = lastplayed;
            }

            else
            {
                GameObject.Find("Highscore").transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "-----";
                GameObject.Find("Last Played").transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = "-----";
            }

            GameObject.Find("Respect").transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = respectscore.ToString() + " point";
    }

    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        if (scene != "")
            SceneManager.LoadScene(scene);
        else
            Debug.Log("Give the level name correctly!");
        //Debug.Log(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    private void ToVictoryMenu()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        campaignMenu.SetActive(false);
        victoryMenu.SetActive(true);
    }
}
