using UnityEngine;
using System.Collections;

public class chest : MonoBehaviour {

	public Animator anim;
	public bool beenOpened = false;
	public ICard contents;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (beenOpened)
		{
			anim.SetBool("opened", true);
		}
	}
}
