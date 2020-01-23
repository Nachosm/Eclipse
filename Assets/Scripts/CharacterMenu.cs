using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour {

    // Text fields
    public Text levelText, hitpointText, /*respectText,*/ upgradeCostText, xpText;

    //Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    void Start()
    {
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("menu_hidden"))
        {
            GetComponent<Animator>().SetTrigger("show");
            GameManager.instance.Pause(true);
            //Set players stats
            GameManager.instance.ResetStats();
            UpdateMenu();
        }
    }
    

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("menu_hidden"))
            {
                GetComponent<Animator>().SetTrigger("show");
                GameManager.instance.Pause(true);
                UpdateMenu();
            }

            else
            {
                GetComponent<Animator>().SetTrigger("hide");
                GameManager.instance.Pause(false);
            }
                
        }
    }

    // CHaracter Selection
    public void OnArrowClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;

            // If we went too far away
            if (currentCharacterSelection == GameManager.instance.playerSprites.Count)
                currentCharacterSelection = 0;

            OnSelectionChanged();
        }
        else
        {
            currentCharacterSelection--;
            // If we went too far away
            if (currentCharacterSelection < 0)
                currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;

            OnSelectionChanged();
        }
    }
    private void OnSelectionChanged()
    {
        characterSelectionSprite.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
        GameManager.instance.player.SwapSprite(currentCharacterSelection);
    }

    // Weapon Upgrade
    public void OnUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
            UpdateMenu();
    }

    // Update the character Information
    public void UpdateMenu()
    {
        // Weapon
        weaponSprite.sprite = GameManager.instance.weaponSprites[GameManager.instance.sword.weaponLevel];
        if (GameManager.instance.sword.weaponLevel == GameManager.instance.weaponPrices.Count)
            upgradeCostText.text = "MAX";
        else
            upgradeCostText.text = GameManager.instance.weaponPrices[GameManager.instance.sword.weaponLevel].ToString();
        //upgradeCostText.text = "NOT IMPLEMENTED";

        // Meta
        levelText.text = GameManager.instance.GetCurrentLevel().ToString();
        hitpointText.text = GameManager.instance.player.hitpoint.ToString();
        //respectText.text = GameManager.instance.booze.ToString();

        // Xp bar
        int currLevel = GameManager.instance.GetCurrentLevel();
        if (currLevel == GameManager.instance.xpTable.Count)
        {
            // Display total xp
            xpText.text = GameManager.instance.score + " total experience points";
            xpBar.localScale = Vector3.one;
        }
        else
        {
            int prevLevelXp = GameManager.instance.GetXpToLevel(currLevel - 1);
            int currLevelXp = GameManager.instance.GetXpToLevel(currLevel);

            int diff = currLevelXp - prevLevelXp;
            int currentXpIntoLevel = GameManager.instance.score - prevLevelXp;

            float completionRatio = (float)currentXpIntoLevel / (float)diff;
            xpBar.localScale = new Vector3(completionRatio,1,1);
            xpText.text = currentXpIntoLevel.ToString() + " / " + diff;
        }

    }

    public void Exit()
    {


        /*UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Destroy(GameObject.Find("GameManager"));
        Destroy(GameObject.Find("RVO"));
        Destroy(GameObject.Find("A*"));
        Destroy(GameObject.Find("FloatingTextManager"));
        Destroy(GameObject.Find("Menu"));
        Destroy(GameObject.Find("HUD"));
        Destroy(GameObject.Find("Main Camera"));
        Destroy(GameObject.Find("Player"));*/
        Debug.Log("Quit!");
        Application.Quit();


    }

    public void setPause(bool isPause)
    {
        GameManager.instance.Pause(isPause);
    }
}
