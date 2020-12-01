using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Timers;

public class DialogueManagerScript : MonoBehaviour
{
	static public DialogueManagerScript me; // singleton
    public TextMeshProUGUI dDisplayer; // the TMPro for displaying dialogue
	public int index; // index to control dialogue advancement
	public DialogueStruct chunk; // the current dialogue chunk
	public List<string> currentDialogue; // the whole dialauge list, with dialogues of different approaches
	private float timer; // timer to limit the speed of the player's clicking
	private float interval = .2f; // speed of the player's clicking

	public enum Approaches // approaches enum
	{
		threaten, trade, inquire, na
	}

	public Approaches myApproach = Approaches.na; // initialize an approach

	private void Start()
	{
		me = this; // singleton
		timer = interval; // set timer
		chunk = default;
	}

	private void Update()
	{
		// if it's interview state, show dialogue
		if (GameManager.me.state == GameManager.me.interview)
		{
			ShowDialogue(GameManager.me.interviewee);
		} 
		// cd
		if (timer > 0)
		{
			timer -= Time.deltaTime;
		}
	}

	public void AdvanceDialogue() // call in character script
	{
		if (timer <= 0) // if clicking cd is finished
		{
			if (index < currentDialogue.Count - 1)
			{
				index++; // add one to index
			}
			else // if dialogue is finished
			{
				index = 0; // reset index to the start of the dialogue
				if (myApproach != Approaches.na) // if the dialogue being displayed is not default dialogue
				{
					CardDialogueEnd_Actions(); // give player the card this approach gives; unlock characters; change relationship
					// if player is disabled, activate it
					if (GameManager.me.player.GetComponent<PlayerScript>().destroyMe)
					{
						GameManager.me.ActivatePlayer();
					}
					myApproach = Approaches.na; // set my approach
					chunk = default;
				}
				else
				{
					if (GameManager.me.cardlessDialogueManager.activeSelf)
					{
						// give CDM the list of cardless dialogues
						CardlessDialogueManager.me.SetCurrentListOf_CD(GameManager.me.interviewee.GetComponent<CharacterScript>().cardlessDialogues);
						// show question buttons
						CardlessDialogueManager.me.ShowQuestionButtons();
						// advance cardless dialogues
						if (CardlessDialogueManager.me.index_cardlessDialogues < CardlessDialogueManager.me.currentListOf_cardlessDialogue.Count - 1) // cycle through all the cardless dialgoues
						{
							CardlessDialogueManager.me.index_cardlessDialogues++;
						}
					}
				}
			}
			timer = interval; // reset timer
		}
	}

	public void ShowDialogue(GameObject interviewee) // call in update
	{
		// show default dialogue
		if (myApproach == Approaches.na)
		{
			if (CardlessDialogueManager.me.questionChosen == 0 || CardlessDialogueManager.me.gameObject.activeSelf == false) // no question chosen or no cardless dialogue manager
			{
				currentDialogue = interviewee.GetComponent<CharacterScript>().defaultDialogues;
				dDisplayer.text = currentDialogue[index];
			}
			else
			{
				currentDialogue = CardlessDialogueManager.me.currentListOf_correspondingDialogues;
				dDisplayer.text = currentDialogue[index];
			}
			
		}
		// show inquire/trade/threaten dialogue
		else
		{
			// first get all the dialogues stored in the character
			foreach (DialogueStruct availableChunk in interviewee.GetComponent<CharacterScript>().dialogues)
			{
				// get all the trigger ids in all the dialogues
				foreach (int triggerID in availableChunk.triggerIDs)
				{
					// if the trigger id matches the card in use
					if (triggerID == CardUsageScript.me.cardId)
					{
						// show different text based on different approaches
						if (myApproach == Approaches.inquire)
						{
							chunk = availableChunk;
							dDisplayer.text = chunk.dialogue_inquiring[index];
							currentDialogue = chunk.dialogue_inquiring;
							ProcessCard();
						}
						else if (myApproach == Approaches.trade)
						{
							chunk = availableChunk;
							dDisplayer.text = chunk.dialogue_trading[index];
							currentDialogue = chunk.dialogue_trading;
							ProcessCard();
						}
						else if (myApproach == Approaches.threaten)
						{
							chunk = availableChunk;
							dDisplayer.text = chunk.dialogue_threatened[index];
							currentDialogue = chunk.dialogue_threatened;
							ProcessCard();
						}
					}
				}
			}
		}
	}

	public void ProcessCard()
	{
		// need to run this only once
		// hide everything other than the dialogue
		if (CardUsageScript.me.cardInUsed != null)
		{
			CardUsageScript.me.cardInUsed.GetComponent<SpriteRenderer>().enabled = false;
			CardUsageScript.me.cardInUsed = null;
		}
		if (GameManager.me.player != null)
		{
			GameManager.me.player.GetComponent<PlayerScript>().destroyMe = true;
		}
	}

	public void CardDialogueEnd_Actions()
	{
		// inquire approach
		if (myApproach == Approaches.inquire)
		{
			// give cards
			if (chunk.cards_inquiring.Count > 0 &&
				!chunk.inquiringCard_given)
			{
				GameManager.me.playerPrefab.GetComponent<PlayerScript>().handPrefabs.Add(chunk.cards_inquiring[0]);
				SetCardGiven();
			}
			// unlock charas
			if (chunk.charasToUnlock_inquire.Count > 0)
			{
				foreach (var chara in chunk.charasToUnlock_inquire)
				{
					StateManagerScript.me.UnlockChara(chara);
				}
			}
			// change relationship
			GameManager.me.interviewee.GetComponent<CharacterScript>().relationship += chunk.relationshipChangeAmount_inquire;
		}
		// threatened approach
		else if (myApproach == Approaches.threaten)
		{
			if (chunk.cards_threatened.Count > 0 &&
				!chunk.threatenedCard_given)
			{
				GameManager.me.playerPrefab.GetComponent<PlayerScript>().handPrefabs.Add(chunk.cards_threatened[0]);
				SetCardGiven();
			}
			// unlock charas
			if (chunk.charasToUnlock_threat.Count > 0)
			{
				foreach (var chara in chunk.charasToUnlock_threat)
				{
					StateManagerScript.me.UnlockChara(chara);
				}
			}
			// change relationship
			GameManager.me.interviewee.GetComponent<CharacterScript>().relationship += chunk.relationshipChangeAmount_threat;
			// limit cards
			foreach (var card in chunk.cardsToLimit_threat)
			{
				card.GetComponent<CardScript>().limited = true;
				foreach (var chara in chunk.cardsLimitedTo_threat)
				{
					card.GetComponent<CardScript>().limitedTo.Add(chara);
				}
				card.GetComponent<CardScript>().promisedTo = GameManager.me.interviewee;
			}
			// destroy cards here?
		}
		// trading approach
		else if (myApproach == Approaches.trade)
		{
			// give card
			if (chunk.cards_trading.Count > 0 &&
				!chunk.tradingCard_given)
			{
				GameManager.me.playerPrefab.GetComponent<PlayerScript>().handPrefabs.Add(chunk.cards_trading[0]);
				SetCardGiven();
			}
			// unlock charas
			if (chunk.charasToUnlock_trade.Count > 0)
			{
				foreach (var chara in chunk.charasToUnlock_trade)
				{
					StateManagerScript.me.UnlockChara(chara);
				}
			}
			// change relationship
			GameManager.me.interviewee.GetComponent<CharacterScript>().relationship += chunk.relationshipChangeAmount_trading;
			// limit cards
			foreach (var card in chunk.cardsToLimit_trade)
			{
				card.GetComponent<CardScript>().limited = true;
				foreach (var chara in chunk.cardsLimitedTo_trade)
				{
					card.GetComponent<CardScript>().limitedTo.Add(chara);
				}
				card.GetComponent<CardScript>().promisedTo = GameManager.me.interviewee;
			}
			// destroy cards here?
		}
	}

	private void SetCardGiven() // set a bool so that character won't give out cards again
	{
		for (int i = 0; i < GameManager.me.interviewee.GetComponent<CharacterScript>().dialogues.Count; i++)
		{
			foreach (int triggerID in GameManager.me.interviewee.GetComponent<CharacterScript>().dialogues[i].triggerIDs)
			{
				if (triggerID == CardUsageScript.me.cardId)
				{
					if (myApproach == Approaches.inquire)
					{
						DialogueStruct d = GameManager.me.interviewee.GetComponent<CharacterScript>().dialogues[i];
						d.inquiringCard_given = true;
						GameManager.me.interviewee.GetComponent<CharacterScript>().dialogues[i] = d;
					}
					else if (myApproach == Approaches.threaten)
					{
						DialogueStruct d = GameManager.me.interviewee.GetComponent<CharacterScript>().dialogues[i];
						d.threatenedCard_given = true;
						GameManager.me.interviewee.GetComponent<CharacterScript>().dialogues[i] = d;
					}
					else if (myApproach == Approaches.trade)
					{
						DialogueStruct d = GameManager.me.interviewee.GetComponent<CharacterScript>().dialogues[i];
						d.tradingCard_given = true;
						GameManager.me.interviewee.GetComponent<CharacterScript>().dialogues[i] = d;
					}
				}
			}
		}
	}
}