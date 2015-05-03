using UnityEngine;
using System.Collections;

public class ICard : ScriptableObject {

	//Card's Information
    public string cardName;
    public int cardID;
    public enum CardType { Action = 1, Equipment = 2}
    public CardType cardType;

    public void init(Card card)
    {
        cardName = card.cardName;
        cardID = card.cardID;
        cardType = (ICard.CardType)card.cardType;
    }

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    // Takes the card's ID and preforms apporiate action
    void preformAction(int ID)
    {
        if(ID == 0)
        {
            //Sword Card

        }
    }
}
