using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

    //Components
    public Animator anim;
    public Rigidbody2D rBody;
    public Collider2D col2D;
    //Input (Which will follow PS Standards)
    private string VInput = "Vertical";
    private float VAsix;
    private string HInput = "Horizontal";
    private float HAsix;
    private string Cross = "Cross";
    private string Circle = "Circle";
    private string Triangle = "Triangle";
    private string Square = "Square";
    private string R1 = "R1";
    private string L1 = "L1";
    //Variables
    public float walking_speed;
    public GameObject collidedObject;
    public ICard cardActionSlot;
    public List<ICard> Inventory;
    public int inventoryInSlot = 0;
    
	// Use this for initialization
	void Start () 
    {
	    
	}
	
	// Update is called once per frame
	void Update () 
    {
        //Misc Stuff
        if(Inventory.Count == 0)
        {
            cardActionSlot = null;
        }
        else
        {
            cardActionSlot = Inventory[inventoryInSlot];
        }
            
        
	    //Movement Input and Animation
        VAsix = Input.GetAxisRaw(VInput);
        HAsix = Input.GetAxisRaw(HInput);
        rBody.velocity = new Vector2(HAsix*walking_speed, VAsix*walking_speed);
        if(rBody.velocity.y <= -1)
        {
            anim.SetInteger("Direction", 0);
        }
        else if(rBody.velocity.x <= -1)
        {
            anim.SetInteger("Direction", 1);
        }
        else if(rBody.velocity.y >= 1)
        {
            anim.SetInteger("Direction", 2);
        }
        else if(rBody.velocity.x >= 1)
        {
            anim.SetInteger("Direction", 3);
        }

        //Button Controls
        //Square is for using the Action Solt card
        if(Input.GetButtonDown(Square))
        {
            Action(cardActionSlot);
        }
        //Circle is basically the button for interactiving with objects
        if(Input.GetButtonDown(Circle))
        {
            if(collidedObject == null)
            {
                LogMessage("Nothing to pick up!");
            }
            else
            {
                PickUp(collidedObject.GetComponent<Card>());
            }
        }
        //L1 and R2 are used to cycle your Action Card from inventory
        if(Input.GetButtonDown(R1))
        {
            if(inventoryInSlot < Inventory.Count-1)
            {
                inventoryInSlot++;
            }
            else
            {
                inventoryInSlot = 0;
            }
        }
        if(Input.GetButtonDown(L1))
        {
            if(inventoryInSlot > 0)
            {
                inventoryInSlot--;
            }
            else
            {
                inventoryInSlot = Inventory.Count - 1;
            }
        }
	}

    //Use card in Action Slot
    private void Action(ICard actionSlot)
    {
        if(actionSlot == null)
        {
            LogMessage("No card in Action Slot.");
        }
        else if(actionSlot.cardName == "Sword")
        {
            LogMessage("You have a sword");
        }
        else
        {
            LogMessage("Something went wrong with Card ID " + actionSlot.cardID);
        }
    }

    //Interact with collided object (Only for pick up atm)
    private void PickUp(Card card)
    {
        int slot = 0;
        if(Inventory.Count == 0)
        {
            Inventory.Add(ScriptableObject.CreateInstance<ICard>());
            Inventory[0].init(card);
			LogMessage("Picked up " + card.cardName + " in Inventory Slot 1");
        }
        else
        {
            for (int i = 0; i < Inventory.Count - 1; i++)
            {
                if (Inventory[i] == null)
                {
                    slot = i;
                    break;
                }
            }
            Inventory[slot] = ScriptableObject.CreateInstance<ICard>();
            Inventory[slot].init(card);
			LogMessage("Picked up " + card.cardName + " created in Inventory Slot " + slot);
        }        
        Destroy(card.gameObject);
    }

    //Send message to Unity Console
    private void LogMessage(string msg)
    {
        Debug.Log(msg);
    }

    //Trigger Methods
    void OnTriggerEnter2D(Collider2D col)
    {
        collidedObject = col.gameObject;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        collidedObject = null;
    }
}
