using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(BoxCollider2D))]
public class Player : Mover {

    private SpriteRenderer spriteRenderer;
    public bool isAlive = true;

    //rotate
    /*private float offset = 0.0f;
    private bool turned = false;*/
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    //Weapons
    [SerializeField]
    private List<Weapons> weapons = new List<Weapons> { };
    private int currentWeaponIndex = 0;
    private int lastWeaponIndex = 0;

    //StartingHitPoint
    public int startingHitPoint = 5;


    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    protected override void ReceiveDamage(Damage dmg)
    {
        if (!isAlive)
            return;

        base.ReceiveDamage(dmg);
        GameManager.instance.OnHitpointChange();

    }
    protected override void Death()
    {
        isAlive = false;
        GameManager.instance.deathMenuAnim.SetTrigger("Show");
    }


    

    private void FixedUpdate()
    {
        WeaponSelection();
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if(isAlive && !GameManager.instance.getPause())
            UpdateMotor(new Vector3(x, y, 0));

        cursorIn();
    }

    private void WeaponSelection()
    {
        if (Input.GetKey(KeyCode.Alpha1))
            currentWeaponIndex = 1;
        else if(Input.GetKey(KeyCode.Alpha2))
            currentWeaponIndex = 2;
        /*
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            currentWeaponIndex++;
            lastWeaponIndex = currentWeaponIndex - 1;
            
        }
            
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            currentWeaponIndex--;
            lastWeaponIndex = currentWeaponIndex + 1;
            
        }
            

        //when go over the list, we set currentIndex to 0 or lastIndexOf list
        if (currentWeaponIndex >= weapons.Count)
        {
            lastWeaponIndex = weapons.Count - 1;
            currentWeaponIndex = 0;
            
        }
        else if (currentWeaponIndex < 0)
        {
            lastWeaponIndex = 0;
            currentWeaponIndex = weapons.Count - 1;
            
        }*/
        //weaponChange();
        
        //Debug.Log("LastWeapon: " + weapons[lastWeaponIndex].name);
        //Debug.Log("CurrentWeapon: " + weapons[currentWeaponIndex].name);
        
    }
    private void weaponChange()
    {
        //weapons[currentWeaponIndex].setActive(true);
        //weapons[lastWeaponIndex].setActive(false);
    }
    //Mark point
    private void cursorIn()
    {   
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
    }

    public void OnLevelUp()
    {
        maxHitpoint++;
        hitpoint = maxHitpoint;
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
            OnLevelUp();
    }
    public void Heal(int healingAmount)
    {
        if (hitpoint == maxHitpoint)
            return;

        hitpoint += healingAmount;
        if (hitpoint > maxHitpoint)
            hitpoint = maxHitpoint;

        GameManager.instance.ShowText("+ " + healingAmount.ToString() + " hp", 25, Color.green, transform.position, Vector3.up, 1.0f);
        GameManager.instance.OnHitpointChange();
    }
    public void Respawn()
    {
        GameManager.instance.score = 0;
        GameManager.instance.booze = 10;
        Heal(maxHitpoint);
        isAlive = true;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
        GameManager.instance.TotalKilledEnemiesCount = 0;
        GameManager.instance.TotalKilledEnemiesCount = 0;
    }

    /*public void AddWeapon(GameObject weapon, int orderInHand)
    {
        weapons.Insert(orderInHand, weapon);
    }*/
}
