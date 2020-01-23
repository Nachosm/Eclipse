using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Talk : Collectable {

    //[SerializeField]
    public Animator anim;

    //Sprites
    [SerializeField]
    private Sprite[] speakersSprites;
    [SerializeField]
    private Sprite[] speakersSprites2;

    [SerializeField]
    private Text[] speakersTexts;

    [SerializeField]
    private Color[] speakersTextsColor;
    protected override void Start()
    {
        base.Start();
        anim = transform.parent.parent.gameObject.GetComponent<Animator>();
    }

    protected override void OnCollect()
    {
        if(!collected)
        {
            transform.parent.gameObject.GetComponent<Talking>().setCurrentTalksCount(speakersTexts.Length);
            transform.parent.gameObject.GetComponent<Talking>().setCurrentTalk(this);
            transform.parent.gameObject.GetComponent<Talking>().setSpritesForGifs(speakersSprites, speakersSprites2);
            anim.SetTrigger("show");
            collected = true;

            //After player arrive to the collider field gamePause
            GameManager.instance.Pause(true);
        }
    }

    public bool getCollected()
    {
        return collected;
    }

    public void setCollected(bool isColleced)
    {
        collected = isColleced;
    }

    public Text[] getSpeakersTexts()
    {
        return speakersTexts;
    }

    public void setSpeakersTexts(Text[] newSpeakersTexts)
    {
        speakersTexts = newSpeakersTexts;
    }

    public Sprite[] getSpeakersSprites()
    {
        return speakersSprites;
    }

    public void setSpeakersSprites2(Sprite[] newSprites)
    {
        speakersSprites2 = newSprites;
    }

    public Sprite[] getSpeakersSprites2()
    {
        return speakersSprites2;
    }

    public Color[] getSpeakersTextColors()
    {
        return speakersTextsColor;
    }
}
