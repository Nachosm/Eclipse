using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talking : MonoBehaviour {
    //Current talk
    private Talk currentTalk = null;

    //Gif
    private float spriteCooldown = 0.74f;
    [SerializeField]
    private List<Sprite> spritesForGifs;
    private int currentSprite;
    //Text Logic
    private int currentTalkCount = 0;
    private int currentTalksCount = 0;

    private string newString = "";
    private float textCooldown = 0.05f;
    private float lastWrittenText = 0f;
    private string currentText = "";
    private string previousText;
    private int charCount = 0;
    private int charPerCooldown = 3;

	void Update () {


        if (currentTalk != null && spritesForGifs.Count != 0)
        {
            if (currentTalkCount == currentTalksCount)
            {
                //Reset values
                currentTalk.anim.SetTrigger("hide");
                currentTalkCount = 0;
                currentSprite = 0;
                spriteCooldown = 0.74f;
                spritesForGifs = new List<Sprite>();
                GameManager.instance.Pause(false);
            }
                
            //Set texts
            else if (currentTalk.getSpeakersTexts()[currentTalkCount].text.Contains("Speaker0: "))
            {
                showText(currentTalk.getSpeakersTextColors()[currentTalkCount]);
                currentTalkerGif(spritesForGifs[currentSprite], spritesForGifs[currentSprite + 1], 1, 2);
            }
                
            else if (currentTalk.getSpeakersTexts()[currentTalkCount].text.Contains("Speaker1: "))
            {
                showText(currentTalk.getSpeakersTextColors()[currentTalkCount]);
                currentTalkerGif(spritesForGifs[currentSprite], spritesForGifs[currentSprite + 1], 2, 1);
            }
                
        }
        
	}

    private void showText(Color textColor)
    {
        
        if (Time.time - lastWrittenText > textCooldown)
        {
            lastWrittenText = Time.time;
            
            //Remove the first 10 character from the text
            newString = currentTalk.getSpeakersTexts()[currentTalkCount].text.Substring(9);
            if (newString.Length - currentText.Length >= charPerCooldown)
            {
                charCount += charPerCooldown;
            }
            else if (charCount != newString.Length)
            {
                charCount++;
            } 
            currentText = newString.Substring(0, (newString.Length - newString.Length + charCount));

            //Set the color and the text
            transform.parent.gameObject.transform.GetChild(0).GetChild(0).transform.gameObject.GetComponent<Text>().color = textColor;
            transform.parent.gameObject.transform.GetChild(0).GetChild(0).transform.gameObject.GetComponent<Text>().text =
                currentText;
        }
    }


    private void currentTalkerGif(Sprite newSprite1, Sprite newSprite2, int show, int hide)
    {
        GameObject.Find("Talking_Menu").transform.GetChild(0).GetChild(show).GetComponent<Image>().gameObject.SetActive(true);
        GameObject.Find("Talking_Menu").transform.GetChild(0).GetChild(hide).GetComponent<Image>().gameObject.SetActive(false);
        spriteCooldown -= Time.deltaTime;
        if (spriteCooldown < 0)
        {
            spriteCooldown = 1.5f;
            GameObject.Find("Talking_Menu").transform.GetChild(0).GetChild(show).GetComponent<Image>().sprite = newSprite1;
        }
        else if (spriteCooldown < 0.75)
        {
            GameObject.Find("Talking_Menu").transform.GetChild(0).GetChild(show).GetComponent<Image>().sprite = newSprite2;
        }

    }

    public void AddToCurrentTalkPlusOne()
    {
        currentTalkCount++;
        charCount = 0;
        currentSprite += 2;
        spriteCooldown = 0.74f;


    }

    public void setCurrentTalksCount(int newCurrentTalks)
    {
        currentTalksCount = newCurrentTalks;
    }
    public int getCurrentTalksCount()
    {
        return currentTalksCount;
    }
    
    public Talk getCurrentTalk()
    {
        return currentTalk;
    }

    public void setCurrentTalk(Talk newCurrentTalk)
    {
        currentTalk = newCurrentTalk;
    }

    public void setSpritesForGifs(Sprite[] speakerSprites1, Sprite[] speakerSprites2)
    {
        if (spritesForGifs.Count != 0)
            spritesForGifs.RemoveRange(0, spritesForGifs.Count);
        for (int i = 0; i < speakerSprites1.Length; i++)
        {
            spritesForGifs.Add(speakerSprites1[i]);
            spritesForGifs.Add(speakerSprites2[i]);
        }
    }

    public List<Sprite> getSpritesForGifs()
    {
        return spritesForGifs;
    }
}
/*
 *     private void showTalker(Sprite newSprite, bool isFirstSpeaker)
    {
        if (isFirstSpeaker)
        {
            GameObject.Find("Talking_Menu").transform.GetChild(0).GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
            GameObject.Find("Talking_Menu").transform.GetChild(0).GetChild(2).GetComponent<Image>().gameObject.SetActive(false);
            GameObject.Find("Talking_Menu").transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = newSprite;
        }

        else
        {
            GameObject.Find("Talking_Menu").transform.GetChild(0).GetChild(2).GetComponent<Image>().gameObject.SetActive(true);
            GameObject.Find("Talking_Menu").transform.GetChild(0).GetChild(1).GetComponent<Image>().gameObject.SetActive(false);
            GameObject.Find("Talking_Menu").transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = newSprite;
        }
            
    }

    private void showTalker(Sprite newSprite1, Sprite newSprite2, bool isFirstSpeaker)
    {
        if (isFirstSpeaker)
        {
            GameObject.Find("Talking_Menu").transform.GetChild(0).GetChild(1).GetComponent<Image>().gameObject.SetActive(true);
            GameObject.Find("Talking_Menu").transform.GetChild(0).GetChild(2).GetComponent<Image>().gameObject.SetActive(false);
            spriteCooldown -= Time.deltaTime;
            if (spriteCooldown < 0)
            {
                spriteCooldown = 1.5f;
                GameObject.Find("Talking_Menu").transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = newSprite1;
            }
            else if (spriteCooldown < 0.75f)
            {
                GameObject.Find("Talking_Menu").transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = newSprite2;
            }
        }
        else
        {
            GameObject.Find("Talking_Menu").transform.GetChild(0).GetChild(2).GetComponent<Image>().gameObject.SetActive(true);
            GameObject.Find("Talking_Menu").transform.GetChild(0).GetChild(1).GetComponent<Image>().gameObject.SetActive(false);
            spriteCooldown -= Time.deltaTime;
            if (spriteCooldown < 0)
            {
                spriteCooldown = 1.5f;
                GameObject.Find("Talking_Menu").transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = newSprite1;
            }
            else if (spriteCooldown < 0.75f)
            {
                GameObject.Find("Talking_Menu").transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = newSprite2;
            }
        }
    }

 */
