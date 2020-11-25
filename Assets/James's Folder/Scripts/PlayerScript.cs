using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// player is instantiated only when in interview, is destroyed after exiting an interview
public class PlayerScript : MonoBehaviour
{
    public List<GameObject> handPrefabs;
	public List<GameObject> hand;
	public Vector3 handSect_startPos;
	public float handSect_length;
	public bool destroyMe = false;
	
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
		if (destroyMe)
		{
			foreach (GameObject card in hand)
			{
				// tell cards to destroy self
				card.GetComponent<SpriteRenderer>().enabled = false;
			}
			// destroy self
			Destroy(gameObject);
		}
	}
}
