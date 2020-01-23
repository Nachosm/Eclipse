using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    private void Awake()
    {
        
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            Destroy(player.gameObject);
            Destroy(floatingTextManager.gameObject);
            Destroy(hud);
            Destroy(menu);
            return;
        }

        instance = this;
        OnHitpointChange();
        SceneManager.sceneLoaded += LoadState;
        SceneManager.sceneLoaded += OnSceneLoaded;
        


        //Update Ammo
        currentWeaponIndex = 0;
        UpdateAmmo(GameObject.Find("PlayerHitBox").transform.GetChild(currentWeaponIndex).GetComponent<Weapons>().AmmoAmount1);
        UpdateRepertory(GameObject.Find("PlayerHitBox").transform.GetChild(currentWeaponIndex).GetComponent<Weapons>().RepertoryCurrent1);
        //waypointNames.Add(GameObject.Find("Waypoint").name);
        //Debug.Log(waypointNames);

        //Set the Menu's Objectives text's, there's always an Objective 1
        GameObject.Find("Objective 2").GetComponent<UnityEngine.UI.Text>().text = "";
        GameObject.Find("Objective2Text").GetComponent<UnityEngine.UI.Text>().text = "";
    }
    void Start()
    {
        //GameObject.Find("WeaponName").GetComponent<TMPro.TextMeshProUGUI>().text = "Sword";
        //ResetStats();
        
        currentLevel = SceneManager.GetActiveScene().name;

            
    }
    private void Update()
    {
        if (!levelCompleted)
        {
            SaveState();

            UpdateTime();
            //ChangeWeapon
            if (Input.GetKey(KeyCode.Alpha1))
            {
                currentWeaponIndex = 0;
                GameObject.Find("PlayerHitBox").transform.GetChild(lastWeaponIndex).GetComponent<Weapons>().Active = false;
                GameObject.Find("PlayerHitBox").transform.GetChild(lastWeaponIndex).GetChild(0).GetComponent<SpriteRenderer>().gameObject.SetActive(false);
                GameObject.Find("PlayerHitBox").transform.GetChild(currentWeaponIndex).GetComponent<Weapons>().Active = true;
                GameObject.Find("PlayerHitBox").transform.GetChild(currentWeaponIndex).GetChild(0).GetComponent<SpriteRenderer>().gameObject.SetActive(true);
                lastWeaponIndex = currentWeaponIndex;
                UpdateAmmo(GameObject.Find("PlayerHitBox").transform.GetChild(currentWeaponIndex).GetComponent<Weapons>().AmmoAmount1);
                UpdateRepertory(GameObject.Find("PlayerHitBox").transform.GetChild(currentWeaponIndex).GetComponent<Weapons>().RepertoryCurrent1);
                GameObject.Find("WeaponName").GetComponent<TMPro.TextMeshProUGUI>().text = "Sword";
                //Debug.Log(currentWeaponIndex);
                //Debug.Log(GameObject.Find("PlayerHitBox").transform.GetChild(currentWeaponIndex).GetComponent<Weapons>().Active);
            }
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                currentWeaponIndex = 1;

                GameObject.Find("PlayerHitBox").transform.GetChild(lastWeaponIndex).GetComponent<Weapons>().Active = false;
                GameObject.Find("PlayerHitBox").transform.GetChild(lastWeaponIndex).GetChild(0).GetComponent<SpriteRenderer>().gameObject.SetActive(false);
                GameObject.Find("PlayerHitBox").transform.GetChild(currentWeaponIndex).GetComponent<Weapons>().Active = true;
                GameObject.Find("PlayerHitBox").transform.GetChild(currentWeaponIndex).GetChild(0).GetComponent<SpriteRenderer>().gameObject.SetActive(true);
                lastWeaponIndex = currentWeaponIndex;
                UpdateAmmo(GameObject.Find("PlayerHitBox").transform.GetChild(currentWeaponIndex).GetComponent<Weapons>().AmmoAmount1);
                UpdateRepertory(GameObject.Find("PlayerHitBox").transform.GetChild(currentWeaponIndex).GetComponent<Weapons>().RepertoryCurrent1);
                GameObject.Find("WeaponName").GetComponent<TMPro.TextMeshProUGUI>().text = "M4";

                //Debug.Log(currentWeaponIndex);
            }

            //Objectives
            if (allObjectivesCompleted == false)
            {

                foreach (var o in objectives)
                {

                    allObjectivesCompleted = true;
                    //If one of the objectives are not completed, allObjectivesCompleted will be false
                    if (o.Completed == false)
                        allObjectivesCompleted = false;


                }
                if (allObjectivesCompleted)
                {
                    ActivatePortal();
                }

            }
            else if (objectives.Count == 0 && !allObjectivesCompleted)
            {
                allObjectivesCompleted = true;
                ActivatePortal();
            }
        }
        else if (GameObject.Find("MenuManager") && !statsHasBeenSet)
        {
            LoadStats();
           
            //If bigger than 2, then the game starts again.
            if (int.Parse(currentLevel[6].ToString()) < 2)
            {
                string nextLevel;
                nextLevel = "Level_" + (int.Parse(currentLevel[6].ToString()) + 1).ToString() + "0";
                MenuManager.instance.Scene = nextLevel;
            }
            Destroy(GameObject.Find("GameManager"));
        }
        

        
    }

    //PlayerPrefs.DeleteAll();

    //Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    //References
    public Player player;
    public Sword sword;
    public Projectile pistol;
    public FloatingTextManager floatingTextManager;
    public RectTransform hitpointBar;
    public Animator deathMenuAnim;
    public GameObject hud;
    public GameObject menu;

    //Logic
    public int booze;
    public int score;

    //Waypoints
    public List<Waypoint> waypointNames;

    //Talking
    [SerializeField]
    private Talking talking;

    //Pause
    private bool pause = false;

    //Weapon
    public int currentWeaponIndex = 0;
    public int lastWeaponIndex = 0;

    //Objectives
    private bool allObjectivesCompleted = false;
    [SerializeField]
    private List<Objective> objectives = new List<Objective> { };
    
    //Kills
    private int killedZombiesCount = 0;
    private int totalKilledEnemiesCount = 0;

    //Time
    private float totalTime = 0f;


    //Menu
    protected bool characterMenuOn = false;

    //End of Level
    public bool levelCompleted = false;
    public bool statsHasBeenSet = false;
    public string currentLevel = "";
    public int highScore = 0;

    public bool AllObjectivesCompleted
    {
        get { return allObjectivesCompleted; }
        set { allObjectivesCompleted = value; }
    }

    public int KilledZombiesCount
    {
        get { return killedZombiesCount; }
        set { killedZombiesCount = value; }
    }

    public int TotalKilledEnemiesCount
    {
        get { return totalKilledEnemiesCount; }
        set { totalKilledEnemiesCount = value; }
    }

    public void ObjectivesRemove()
    {
        objectives.RemoveAll(obj => obj.Completed == true);
    }

    public void  AddToObjectives(Objective objective)
    {
        objectives.Add(objective);

    }



    private void ActivatePortal()
    {
        GameObject.Find("Portal").GetComponent<BoxCollider2D>().enabled = true;
    }

    private void UpdateTime()
    {
        try
        {
            if (!pause)
            {
                totalTime += Time.deltaTime;
                if (totalTime <= 60f)
                    GameObject.Find("TimeCounter").GetComponent<TMPro.TextMeshProUGUI>().text = totalTime.ToString("0.00");
                else if (totalTime > 60f)
                {
                    float min = int.Parse(Mathf.Floor(totalTime / 60f).ToString());
                    GameObject.Find("TimeCounter").GetComponent<TMPro.TextMeshProUGUI>().text = min.ToString() + " m " + (totalTime - (60 * min)).ToString("0.00");
                }
            }
        }
       
        catch (NullReferenceException)
        {

        }
        
            
    }


    public void LoadStats()
    {
        int killedEnemies = score * 30;
        float finalTimeScore = Mathf.Floor(10000 / totalTime + 0.01f);
        float bonusScore = Mathf.Floor((score * totalTime));
        float totalScore = killedEnemies + finalTimeScore + bonusScore;
        int hs = 0;

        HighScore setHs = new HighScore();
        HighScore readHs = new HighScore();

        string currentMap = "";
        if (currentLevel == "Level_00")
            currentMap = "Burn Out";
        else if (currentLevel == "Level_10")
            currentMap = "Lights Off";
        else if (currentLevel == "Level_20")
            currentMap = "Raise Again";



        setHs.UpdateHighscoreByMap(int.Parse(Math.Floor(totalScore).ToString()), currentMap);
        setHs.db_connection.Close();
        
        System.Data.IDataReader reader = readHs.getDataByString(currentMap);
        statsHasBeenSet = true;
        GameObject.Find("KilledEnemiesScore").transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = killedEnemies.ToString();
        GameObject.Find("FinalTimeScore").transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = finalTimeScore.ToString();
        GameObject.Find("BonusScore").transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = bonusScore.ToString();
        GameObject.Find("TotalScore").transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = totalScore.ToString();
        GameObject.Find("RespactAtScore").transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = reader[2].ToString();
        readHs.db_connection.Close();
        Debug.Log("Highscore has been updated to " + totalScore);
        //setHs.close();
    }

    public void UpdateAmmo(int newAmmoCount)
    {
        GameObject.Find("AmmoCount").GetComponent<TMPro.TextMeshProUGUI>().text = newAmmoCount.ToString();
    }

    public void UpdateRepertory(int newRepertory)
    {
        GameObject.Find("RepertoryCount").GetComponent<TMPro.TextMeshProUGUI>().text = newRepertory.ToString();
    }

    // Floating text
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }

    // Upgrade Weapon
    public bool TryUpgradeWeapon()
    {
        // is the weapon max level?
        if (weaponPrices.Count <= sword.weaponLevel)
            return false;
        if (booze >= weaponPrices[sword.weaponLevel])
        {
            booze -= weaponPrices[sword.weaponLevel];
            sword.UpgradeWeapon();
            return true;
        }
        return false;
    }

    // Hitpoint Bar
    public void OnHitpointChange()
    {
        float ratio = (float)player.hitpoint / (float)player.maxHitpoint;
        hitpointBar.localScale = new Vector3(1, ratio, 1);
    }

    //Experience System
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while (score >= add)
        {
            add += xpTable[r];
            r++;

            //Max level
            if (r == xpTable.Count)
                return r;
        }

        return r;
    }
    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while (r < level)
        {
            xp += xpTable[r];
            r++;
        }

        return xp;
        
    }
    public void GrantScore(int s)
    {
        int currLevel = GetCurrentLevel();
        score += s;
        if (currLevel < GetCurrentLevel())
            OnLevelUp();

    }

    public void OnLevelUp()
    {
        Debug.Log("Level Up!");
        player.OnLevelUp();
        OnHitpointChange();
    }

    // On Scene Loaded
    public void OnSceneLoaded(Scene s, LoadSceneMode mode)
    {
        try
        {
            player.transform.position = GameObject.Find("SpawnPoint").transform.position;
            //GameObject.Find("EndOfMapAnimation").GetComponent<Animator>().SetTrigger("hiding");
            Pause(false);
        }
        catch (Exception) { }
        
    }

    // Death menu and Respawn
    public void Respawn()
    {
        deathMenuAnim.SetTrigger("Hide");
        UnityEngine.SceneManagement.SceneManager.LoadScene(currentLevel);
        player.Respawn();
    }

    //Save state
    /*
     * Int prefered Skin
     * Int pesos
     * int ecperience
     * int weaponLevel
     */
    public void SaveState()
    {
        string s = "";

        s += "0" + "|";
        s += booze.ToString() + "|";
        s += score + "|";
        s += sword.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);
        //GameManager.instance.waypointNames = new List<Waypoint>();

    }
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;

        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        //Change player skin
        booze = int.Parse(data[1]);

        // Experience
        score = int.Parse(data[2]);
        if(GetCurrentLevel() != 1)
            player.SetLevel(GetCurrentLevel());

        //Change the weapon Level
        sword.SetWeaponLevel(int.Parse(data[3]));


        //GameManager.instance.waypointNames = new List<Waypoint>();
    }

    public void Pause(bool isPaused)
    {
        pause = isPaused;
        Animator[] anims = FindObjectsOfType<Animator>();
        foreach(Animator a in anims)
        {
            if (a.transform.gameObject.name != "Talking_Menu" && a.transform.gameObject.name != "Menu" /*|| 
                a.transform.gameObject.name != "DeathMenu"*/ && a.name != "EndOfMapAnimation")
                a.enabled = !isPaused;
        }
            
    }

    public Talking getTalking()
    {
        return talking;
    }

    public void setPause(bool isPause)
    {
        pause = isPause;
    }

    public bool getPause()
    {
        return pause;
    }

    public void ResetStats()
    {
        score = 0;
        player.hitpoint = player.startingHitPoint;
    }
}
