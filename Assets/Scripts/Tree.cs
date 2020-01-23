using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Collidable {

    [SerializeField]
    private List<SpriteRenderer> sprites = new List<SpriteRenderer>();

    private bool changeOpacity;
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter" && (coll.gameObject.layer == 9 || coll.gameObject.layer == 11))
        {
            changeOpacity = true;
            foreach (var s in sprites)
                s.color = new Color(1f, 1f, 1f, .5f);
        }
        else
            changeOpacity = false;

    }
}
