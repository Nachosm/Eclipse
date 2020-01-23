using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRenderer : MonoBehaviour {
    private SpriteRenderer mySprite;
    private Animator anim;

    void Awake () {
        mySprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
	

	void Update () {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            anim.SetBool("run", true);
        }
        else
            anim.SetBool("run", false);
        if (!GameManager.instance.getPause())
        {
            //A játékos forogjon a kurzor irányába
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

            transform.up = -direction;
        }
        
    }
    void FixedUpdate()
    {
        //anim.SetFloat("speed", Mathf.Abs(Input.GetAxis("Vertical")));
    }
}
