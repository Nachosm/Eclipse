using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : Mover {

    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Sprite brokenSprite;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    protected override void Death()
    {
        spriteRenderer.sprite = brokenSprite;
    }


}