using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booze : Collectable
{
    public Sprite emtyChest;
    public int boozeAmount = 1;

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emtyChest;
            GameManager.instance.booze += boozeAmount;
            GameManager.instance.ShowText("+" + boozeAmount + " booze level!", 25,Color.yellow,transform.position,Vector3.up * 50, 3.0f);
        }
    }
}
