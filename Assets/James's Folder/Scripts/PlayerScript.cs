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
	public bool disableMeNHand = false;

	public Vector3 usableCards_startPos;
	public float usableCards_length;


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
		if (disableMeNHand)
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
			hand[i].GetComponent<CardScript>().og_pos = hand[i].transform.position;
		}
	}

	public void ShowUsableCards_Player()
	{
		CharacterScript iS = GameManager.me.interviewee.GetComponent<CharacterScript>();
		List<GameObject> usableCards = new List<GameObject>();
		for (int i = 0; i < hand.Count; i++)
		{
			CardScript cs = hand[i].GetComponent<CardScript>();
			if (iS.usableIDs.Contains(cs.id))
			{
				usableCards.Add(hand[i]);
				hand[i].GetComponent<SpriteRenderer>().enabled = true;
			}
			else
			{
				hand[i].GetComponent<SpriteRenderer>().enabled = false;
			}
		}
		for (int i = 0; i < usableCards.Count; i++)
		{
			Vector3 pos = new Vector3(usableCards_startPos.x + (i + 1) * usableCards_length / (usableCards.Count + 1), usableCards_startPos.y, 0);
			usableCards[i].transform.position = pos;
			usableCards[i].GetComponent<CardScript>().og_pos = usableCards[i].transform.position;
		}
	}

	public void DestroyCard(string cardName)
	{
		List<GameObject> cardsToRemove = new List<GameObject>();
		foreach (var card in hand)
		{
			if (card.name == cardName) // names have "(Clone)" in it
			{
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
