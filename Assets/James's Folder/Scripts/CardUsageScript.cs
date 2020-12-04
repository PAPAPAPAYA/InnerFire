using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardUsageScript : MonoBehaviour
{
	static public CardUsageScript me; // singleton

    public GameObject cardInUsed; // the card being used
    public int cardId; // the id of the card being used
    public Vector3 slotPos; // the position to put the card being used
    public GameObject inquire; // inquire button
    public GameObject trade; // trade button
	public GameObject threaten; // threaten button
	public bool showButtons = true;

	private void Start()
	{
		me = this;
	}

	private void Update()
	{
		if (GameManager.me.state == GameManager.me.interview) // when in interview state
		{
			if (showButtons && cardInUsed!=null) // if there is a card being used, show buttons
			{
				
				inquire.SetActive(true);
				trade.SetActive(true);
				threaten.SetActive(true);
			}
			else // if none, hide buttons
			{
				inquire.SetActive(false);
				trade.SetActive(false);
				threaten.SetActive(false);
			}
		}
	}

	// button functions
	public void Threaten()
	{
		BreakPromise();
		DialogueManagerScript.me.myApproach = DialogueManagerScript.Approaches.threaten;
		cardInUsed.GetComponent<CardScript>().AddChara_n_approach(GameManager.me.interviewee, DialogueManagerScript.Approaches.threaten);
		showButtons = false;
		DialogueManagerScript.me.dDisplayer.gameObject.SetActive(true);
	}
	public void Trade()
	{
		BreakPromise();
		DialogueManagerScript.me.myApproach = DialogueManagerScript.Approaches.trade;
		cardInUsed.GetComponent<CardScript>().charasIWasUsedTo.Add(GameManager.me.interviewee);
		cardInUsed.GetComponent<CardScript>().howIWasUsed.Add(DialogueManagerScript.Approaches.trade);
		showButtons = false;
		DialogueManagerScript.me.dDisplayer.gameObject.SetActive(true);
	}
	public void Inquire()
	{
		BreakPromise();
		DialogueManagerScript.me.myApproach = DialogueManagerScript.Approaches.inquire;
		cardInUsed.GetComponent<CardScript>().charasIWasUsedTo.Add(GameManager.me.interviewee);
		cardInUsed.GetComponent<CardScript>().howIWasUsed.Add(DialogueManagerScript.Approaches.inquire);
		showButtons = false;
		DialogueManagerScript.me.dDisplayer.gameObject.SetActive(true);
	}

	private void BreakPromise()
	{
		if (cardInUsed.GetComponent<CardScript>().limited) // if card is limited
		{
			foreach (var chara in cardInUsed.GetComponent<CardScript>().limitedTo)
			{
				if (chara.name + "(Clone)" == GameManager.me.interviewee.name) // if card is used on a restricted character
				{
					int time = 0;
					foreach (var charaToCheck in cardInUsed.GetComponent<CardScript>().charasIWasUsedTo)
					{
						if (charaToCheck.name == GameManager.me.interviewee.name)
						{
							time++;
						}
					}
					if (time == 0)
					{
						cardInUsed.GetComponent<CardScript>().promisedTo.GetComponent<CharacterScript>().relationship--; // decrease relationship with the character that limited the card
					}
				}
			}
			cardInUsed.GetComponent<CardScript>().promisedTo = null;
			cardInUsed.GetComponent<CardScript>().limited = false;
			cardInUsed.GetComponent<CardScript>().limitedTo.Clear();
		}
	}
}
