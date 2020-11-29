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

	private void Start()
	{
		me = this;
	}

	private void Update()
	{
		if (GameManager.me.state == GameManager.me.interview) // when in interview state
		{
			if (cardInUsed != null) // if there is a card being used, show buttons
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
		DialogueManagerScript.me.myApproach = DialogueManagerScript.Approaches.threaten;
	}
	public void Trade()
	{
		DialogueManagerScript.me.myApproach = DialogueManagerScript.Approaches.trade;
	}
	public void Inquire()
	{
		print("yo");
		DialogueManagerScript.me.myApproach = DialogueManagerScript.Approaches.inquire;
	}
}
