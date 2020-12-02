using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// player is instantiated only when in interview, is destroyed after exiting an interview
public class PlayerScript : MonoBehaviour
{
	public static PlayerScript me;
    public List<GameObject> handPrefabs; // initial cards
	public List<GameObject> hand;
	public Vector3 handSect_startPos;
	public float handSect_length;
	public bool hideMeNHand = false;

	private void Awake()
	{
		me = this;
	}

	private void Start()
	{
		// show hand
		for (int i = 0; i < handPrefabs.Count; i++)
		{
			// instantiate cards and set position
			GameObject card = Instantiate(handPrefabs[i]);
			Vector3 pos = new Vector3(handSect_startPos.x + (i+1)*handSect_length/(handPrefabs.Count+1), handSect_startPos.y, 0);
			card.transform.position = pos;
			// add it to a list
			hand.Add(card);
		}
	}

	private void Update()
	{
		// if told to destroy self
		if (hideMeNHand)
		{
			foreach (GameObject card in hand)
			{
				// disable card dragging
				//card.GetComponent<SpriteRenderer>().enabled = false;
				card.GetComponent<CardScript>().canBeDragged = false;
			}
			// destroy self
			//Destroy(gameObject);
		}
	}

	public void ArrangeCards()
	{
		for (int i = 0; i < hand.Count; i++)
		{
			Vector3 pos = new Vector3(handSect_startPos.x + (i + 1) * handSect_length / (hand.Count + 1), handSect_startPos.y, 0);
			hand[i].transform.position = pos;
		}
	}

	public void DestroyCard(string cardName)
	{
		List<GameObject> cardsToRemove = new List<GameObject>();
		foreach (var card in hand)
		{
			if (card.name == cardName + "(Clone)")
			{
				print("destroy " + cardName + "(Clone)");
				cardsToRemove.Add(card);
				card.GetComponent<CardScript>().destroyMe = true;
			}
		}
		foreach (var card in cardsToRemove)
		{
			hand.Remove(card);
		}
	}
}
