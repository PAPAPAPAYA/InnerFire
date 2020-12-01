using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CardScript : MonoBehaviour
{
    public int id; // id
    public string namae; // name of the card
    [TextArea]
    public string description; // description of the card

    // show info
    public TextMeshProUGUI infoDisplay; // show description of the card

	// drag and drop
	private Vector3 og_pos; // show where the card was before being dragged around

	// limit
	public bool limited = false;
	public List<GameObject> limitedTo;
	public GameObject promisedTo;

	// track who and how this card was used on
	public List<GameObject> charasIWasUsedTo;
	public List<DialogueManagerScript.Approaches> howIWasUsed; 

	

	private void Start()
	{
		og_pos = transform.position; // set og pos
	}

	private void Update()
	{
		if (!GetComponent<SpriteRenderer>().enabled) // if told by player script to disable sprite renderer
		{
			//Destroy(gameObject); // destroy self
		}
	}

	private void OnMouseDrag()
	{
		infoDisplay.text = description; // show description
		Vector3 mousePos_screen = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10); // get mouse screen pos
		Vector3 mousePos_world = Camera.main.ScreenToWorldPoint(mousePos_screen); // convert mouse screen pos to world pos
		transform.position = mousePos_world; // set the card position to mouse world pos
		GameManager.me.interviewee.GetComponent<BoxCollider2D>().enabled = false; // disable 
	}

	private void OnMouseUpAsButton() // when not dragging the card
	{
		infoDisplay.text = ""; // hide card info
		
		GameManager.me.interviewee.GetComponent<BoxCollider2D>().enabled = true; // enable interviwee's collider
		// get interviewee's position
		Transform interviewee = GameManager.me.interviewee.transform;
		Vector3 interviewee_pos = GameManager.me.interviewee_pos;
		
		// check if card is inside interviewee's picture
		if (transform.position.x > interviewee_pos.x - interviewee.localScale.x / 2 &&
			transform.position.x < interviewee_pos.x + interviewee.localScale.x / 2 &&
			transform.position.y > interviewee_pos.y - interviewee.localScale.y / 2 &&
			transform.position.y < interviewee_pos.y + interviewee.localScale.y / 2)
		{
			// show options
			int time = 0;
			foreach (var chara in charasIWasUsedTo)
			{
				if (chara.name == interviewee.name)
				{
					time++;
				}
			}
			if (time == 0)
			{
				CardUsageScript.me.cardInUsed = gameObject; // set card in used
				CardUsageScript.me.cardId = id; // set card in used id
				CardUsageScript.me.showButtons = true;
			}
			else
			{
				transform.position = og_pos;
			}
		}
		else // if the card is released while not over the interviwee's picture, move the card back to its original position
		{
			transform.position = og_pos;
		}
	}

	public void AddChara_n_approach(GameObject interviewee, DialogueManagerScript.Approaches approach)
	{
		int charaTime = 0;
		// check if this character is already in [charas i was used to]
		if (charasIWasUsedTo.Count > 0)
		{
			foreach (var chara in charasIWasUsedTo)
			{
				if (chara.name == interviewee.name)
				{
					charaTime++;
				}
			}
			if (charaTime == 0)
			{
				charasIWasUsedTo.Add(interviewee);
				howIWasUsed.Add(approach);
			}
		}
		else
		{
			charasIWasUsedTo.Add(interviewee);
			howIWasUsed.Add(approach);
		}
	}
}
