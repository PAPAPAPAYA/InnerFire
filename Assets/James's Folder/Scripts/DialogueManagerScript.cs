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

	public List<GameObject> availableCards;

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
		availableCards = new List<GameObject>();
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
			else // if current dialogue is finished
			{
				if (GameManager.me.interviewee.GetComponent<CharacterScript>().cardlessDialogues.Count <= 0)
				{
					GameManager.me.interviewee.GetComponent<CharacterScript>().cardlessDialogueFinished = true;
				}
				index = currentDialogue.Count - 1; // lock the dialogue at the last index
				if (myApproach != Approaches.na) // if the dialogue being displayed is not default dialogue
				{
					CardDialogueEnd_Actions(); // give player the card this approach gives; unlock characters; change relationship
					CardUsageScript.me.cardInUsed = null;
					//CardUsageScript.me.showButtons = true;
					//dDisplayer.gameObject.SetActive(false);
					myApproach = Approaches.na;
					chunk = default;
				}
				else
				{
					if (GameManager.me.cardlessDialogueManager.activeSelf)
					{
						// give CDM the list of cardless dialogues
						CardlessDialogueManager.me.SetCurrentListOf_CD(GameManager.me.interviewee.GetComponent<CharacterScript>().cardlessDialogues);
						// check if the cardless dialogue has any unlocked options, if none, skip it
						if (CardlessDialogueManager.me.currentListOf_cardlessDialogue[CardlessDialogueManager.me.index_cardlessDialogues].noneOptionUnlocked)
						{
							CardlessDialogueManager.me.index_cardlessDialogues++;
						}
						
						// show question buttons
						CardlessDialogueManager.me.ShowQuestionButtons();
						// advance cardless dialogues
						if (CardlessDialogueManager.me.index_cardlessDialogues < CardlessDialogueManager.me.currentListOf_cardlessDialogue.Count - 1) // cycle through all the cardless dialgoues
						{
							CardlessDialogueManager.me.index_cardlessDialogues++;
						}
					}
					else
					{
						GameManager.me.ActivatePlayer();
						GameManager.me.player.GetComponent<PlayerScript>().ShowUsableCards_Player();
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
			if (GameManager.me.cardlessDialogueManager.activeSelf == false || 
				CardlessDialogueManager.me.questionChosen == 0) // no question chosen or no cardless dialogue manager
			{
				if (!GameManager.me.interviewee.GetComponent<CharacterScript>().cardlessDialogueFinished)
				{
					currentDialogue = interviewee.GetComponent<CharacterScript>().defaultDialogues;
					dDisplayer.text = currentDialogue[index];
				}
			}
			else
			{
				currentDialogue = CardlessDialogueManager.me.currentListOf_correspondingDialogues;
				//if (currentDialogue.Count > 0)
				{
					dDisplayer.text = currentDialogue[index]; ////////////////////////////
				}
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
						chunk = availableChunk;
						SetUpDialogue(myApproach, GameManager.me.interviewee.GetComponent<CharacterScript>().relationship);
						ProcessCard();
					}
				}
			}
			
		}
	}

	private void SetUpDialogue(Approaches appr, int relationship)
	{
		if (appr == Approaches.inquire)
		{
			if (relationship == 0)
			{
				dDisplayer.text = chunk.dialogue_inquiring[index];
				currentDialogue = chunk.dialogue_inquiring;
			}
			else if (relationship > 0)
			{
				if (chunk.dialogue_inquiring_p.Count == 0) // if no positive inquire text, use neutral
				{
					dDisplayer.text = chunk.dialogue_inquiring[index];
					currentDialogue = chunk.dialogue_inquiring;
				}
				else
				{
					dDisplayer.text = chunk.dialogue_inquiring_p[index];
					currentDialogue = chunk.dialogue_inquiring_p;
				}
			}
			else if (relationship < 0)
			{
				if (chunk.dialogue_inquiring_n.Count == 0) // if no negative inquire text, use neutral
				{
					dDisplayer.text = chunk.dialogue_inquiring[index];
					currentDialogue = chunk.dialogue_inquiring;
				}
				else
				{
					dDisplayer.text = chunk.dialogue_inquiring_n[index];
					currentDialogue = chunk.dialogue_inquiring_n;
				}
			}
		}
		else if (appr == Approaches.trade)
		{
			if (relationship == 0)
			{
				dDisplayer.text = chunk.dialogue_trading[index];
				currentDialogue = chunk.dialogue_trading;
			}
			else if (relationship > 0)
			{
				if (chunk.dialogue_trading_p.Count == 0) // if no positive trading text, use neutral
				{
					dDisplayer.text = chunk.dialogue_trading[index];
					currentDialogue = chunk.dialogue_trading;
				}
				else
				{
					dDisplayer.text = chunk.dialogue_trading_p[index];
					currentDialogue = chunk.dialogue_trading_p;
				}
			}
			else if (relationship < 0)
			{
				if (chunk.dialogue_trading_n.Count == 0) // if no negative trading text, use neutral
				{
					dDisplayer.text = chunk.dialogue_trading[index];
					currentDialogue = chunk.dialogue_trading;
				}
				else
				{
					dDisplayer.text = chunk.dialogue_trading_n[index];
					currentDialogue = chunk.dialogue_trading_n;
				}
			}
		}
		else if (appr == Approaches.threaten)
		{
			if (relationship == 0)
			{
				dDisplayer.text = chunk.dialogue_threatened[index];
				currentDialogue = chunk.dialogue_threatened;
			}
			else if (relationship > 0)
			{
				if (chunk.dialogue_threatened_p.Count == 0)
				{
					dDisplayer.text = chunk.dialogue_threatened[index];
					currentDialogue = chunk.dialogue_threatened;
				}
				else
				{
					dDisplayer.text = chunk.dialogue_threatened_p[index];
					currentDialogue = chunk.dialogue_threatened_p;
				}
			}
			else if (relationship < 0)
			{
				if (chunk.dialogue_threatened_n.Count == 0)
				{
					dDisplayer.text = chunk.dialogue_threatened[index];
					currentDialogue = chunk.dialogue_threatened;
				}
				else
				{
					dDisplayer.text = chunk.dialogue_threatened_n[index];
					currentDialogue = chunk.dialogue_threatened_n;
				}
			}
		}
	}

	public void ProcessCard()
	{
		// hide everything other than the dialogue
		if (GameManager.me.player != null)
		{
			GameManager.me.player.GetComponent<PlayerScript>().disableMeNHand = true;
		}
	}

	public void CardDialogueEnd_Actions()
	{
		if (GameManager.me.player.GetComponent<PlayerScript>().disableMeNHand)
		{
			GameManager.me.ActivatePlayer();
		}

		CheckCondition_thenEndAction(myApproach, GameManager.me.interviewee.GetComponent<CharacterScript>().relationship);
		
		GameManager.me.player.GetComponent<PlayerScript>().ArrangeCards();
		GameManager.me.player.GetComponent<PlayerScript>().ShowUsableCards_Player();
	}

	private void CheckCondition_thenEndAction(Approaches appr, int relationship)
	{
		if (appr == Approaches.inquire)
		{
			if (relationship == 0)
			{
				EndAction_with(chunk.cards_inquiring,
							   chunk.inquiringCard_given,
							   chunk.charasToUnlock_inquire,
							   chunk.relationshipChangeAmount_inquire,
							   chunk.cardsToLimit_inquire,
							   chunk.cardsLimitedTo_inquire,
							   chunk.cardsToDestory_inquire);
			}
			else if (relationship > 0)
			{
				if (chunk.dialogue_inquiring_p.Count == 0) // if no postive inquire text
				{
					EndAction_with(chunk.cards_inquiring,
							   chunk.inquiringCard_given_p, // here the card given is still using the bool from positive dialogue
							   chunk.charasToUnlock_inquire,
							   chunk.relationshipChangeAmount_inquire,
							   chunk.cardsToLimit_inquire,
							   chunk.cardsLimitedTo_inquire,
							   chunk.cardsToDestory_inquire);
				}
				else
				{
					EndAction_with(chunk.cards_inquiring_p,
							   chunk.inquiringCard_given_p,
							   chunk.charasToUnlock_inquire_p,
							   chunk.relationshipChangeAmount_inquire_p,
							   chunk.cardsToLimit_inquire_p,
							   chunk.cardsLimitedTo_inquire_p,
							   chunk.cardsToDestory_inquire_p);
				}
			}
			else if (relationship < 0)
			{
				if (chunk.dialogue_inquiring_n.Count == 0) // if no negative inquire text
				{
					EndAction_with(chunk.cards_inquiring,
							   chunk.inquiringCard_given_n,
							   chunk.charasToUnlock_inquire,
							   chunk.relationshipChangeAmount_inquire,
							   chunk.cardsToLimit_inquire,
							   chunk.cardsLimitedTo_inquire,
							   chunk.cardsToDestory_inquire);
				}
				else
				{
					EndAction_with(chunk.cards_inquiring_n,
								  chunk.inquiringCard_given_n,
								  chunk.charasToUnlock_inquire_n,
								  chunk.relationshipChangeAmount_inquire_n,
								  chunk.cardsToLimit_inquire_n,
								  chunk.cardsLimitedTo_inquire_n,
								  chunk.cardsToDestory_inquire_n);
				}
			}
		}
		else if (appr == Approaches.trade)
		{
			if (relationship == 0)
			{
				EndAction_with(chunk.cards_trading,
							   chunk.tradingCard_given,
							   chunk.charasToUnlock_trade,
							   chunk.relationshipChangeAmount_trading,
							   chunk.cardsToLimit_trade,
							   chunk.cardsLimitedTo_trade,
							   chunk.cardsToDestory_trade);
			}
			else if (relationship > 0)
			{
				if (chunk.dialogue_trading_p.Count == 0)
				{
					EndAction_with(chunk.cards_trading,
							   chunk.tradingCard_given_p,
							   chunk.charasToUnlock_trade,
							   chunk.relationshipChangeAmount_trading,
							   chunk.cardsToLimit_trade,
							   chunk.cardsLimitedTo_trade,
							   chunk.cardsToDestory_trade);
				}
				else
				{
					EndAction_with(chunk.cards_trading_p,
							   chunk.tradingCard_given_p,
							   chunk.charasToUnlock_trade_p,
							   chunk.relationshipChangeAmount_trading_p,
							   chunk.cardsToLimit_trade_p,
							   chunk.cardsLimitedTo_trade_p,
							   chunk.cardsToDestory_trade_p);
				}
			}
			else if (relationship < 0)
			{
				if (chunk.dialogue_trading_n.Count == 0)
				{
					EndAction_with(chunk.cards_trading,
							   chunk.tradingCard_given_n,
							   chunk.charasToUnlock_trade,
							   chunk.relationshipChangeAmount_trading,
							   chunk.cardsToLimit_trade,
							   chunk.cardsLimitedTo_trade,
							   chunk.cardsToDestory_trade);
				}
				else
				{
					EndAction_with(chunk.cards_trading_n,
							   chunk.tradingCard_given_n,
							   chunk.charasToUnlock_trade_n,
							   chunk.relationshipChangeAmount_trading_n,
							   chunk.cardsToLimit_trade_n,
							   chunk.cardsLimitedTo_trade_n,
							   chunk.cardsToDestory_trade_n);
				}
			}
		}
		else if (appr == Approaches.threaten)
		{
			if (relationship == 0)
			{
				EndAction_with(chunk.cards_threatened,
							   chunk.threatenedCard_given,
							   chunk.charasToUnlock_threat,
							   chunk.relationshipChangeAmount_threat,
							   chunk.cardsToLimit_threat,
							   chunk.cardsLimitedTo_threat,
							   chunk.cardsToDestory_threat);
			}
			else if (relationship > 0)
			{
				if (chunk.dialogue_threatened_p.Count == 0)
				{
					EndAction_with(chunk.cards_threatened,
							   chunk.threatenedCard_given_p,
							   chunk.charasToUnlock_threat,
							   chunk.relationshipChangeAmount_threat,
							   chunk.cardsToLimit_threat,
							   chunk.cardsLimitedTo_threat,
							   chunk.cardsToDestory_threat);
				}
				else
				{
					EndAction_with(chunk.cards_threatened_p,
								  chunk.threatenedCard_given_p,
								  chunk.charasToUnlock_threat_p,
								  chunk.relationshipChangeAmount_threat_p,
								  chunk.cardsToLimit_threat_p,
								  chunk.cardsLimitedTo_threat_p,
								  chunk.cardsToDestory_threat_p);
				}
			}
			else if (relationship < 0)
			{
				if (chunk.dialogue_threatened_n.Count == 0)
				{
					EndAction_with(chunk.cards_threatened,
							   chunk.threatenedCard_given_n,
							   chunk.charasToUnlock_threat,
							   chunk.relationshipChangeAmount_threat,
							   chunk.cardsToLimit_threat,
							   chunk.cardsLimitedTo_threat,
							   chunk.cardsToDestory_threat);
				}
				else
				{
					EndAction_with(chunk.cards_threatened_n,
								chunk.threatenedCard_given_n,
								chunk.charasToUnlock_threat_n,
								chunk.relationshipChangeAmount_threat_n,
								chunk.cardsToLimit_threat_n,
								chunk.cardsLimitedTo_threat_n,
								chunk.cardsToDestory_threat_n);
				}
			}
		}
	}

	private void EndAction_with(List<GameObject> cardsToGive, 
								bool cardGiven, 
								List<StateManagerScript.Charas> charasToUnlock, 
								int relationship_changeAmount,
								List<GameObject> cardsToLimit,
								List<GameObject> cardsLimitedTo,
								List<GameObject> cardsToDestroy)
	{
		// give card
		if (cardsToGive.Count > 0 &&
			!cardGiven)
		{
			foreach (var card in cardsToGive)
			{
				HandToClueboard htc = card.GetComponent<HandToClueboard>();
				CardScript cs = card.GetComponent<CardScript>();
				// instantiate card
				GameObject instantiatedCard = Instantiate(card);
				instantiatedCard.GetComponent<CardScript>().canBeDragged = true;
				// check to see if player hand already has this card
				if (GameManager.me.player.GetComponent<PlayerScript>().hand.Contains(instantiatedCard))
				{
					//print("already has this card, therefore don't give");
					// if yes, destroy it
					Destroy(instantiatedCard);
				}
				else
				{
					//print("player get new card");
					// if no, add it to hand then check if add to clue board
					GameManager.me.player.GetComponent<PlayerScript>().hand.Add(instantiatedCard);
					if (htc.addMeToClueBoard) // check if this card should be added to clue board, if yes
					{
						GameManager.me.newCardNotification.SetActive(true);
						ClueBoard.me.UnlockClueBoardCard(cs.namae, cs.description, htc.motiveText, htc.fireText, htc.whoToConnect, htc.imAMotiveCard, htc.imAFireCard, instantiatedCard);
					}
				}
			}
			GameManager.me.player.GetComponent<PlayerScript>().ArrangeCards();
			SetCardGiven();
		}
		// unlock charas
		if (charasToUnlock.Count > 0)
		{
			foreach (var chara in charasToUnlock)
			{
				StateManagerScript.me.UnlockChara(chara);
			}
		}
		// change relationship
		GameManager.me.interviewee.GetComponent<CharacterScript>().relationship += relationship_changeAmount;
		//GameManager.me.interviewee.GetComponent<CharacterScript>().relationship = Mathf.Clamp(GameManager.me.interviewee.GetComponent<CharacterScript>().relationship, -1, 1);
		// limit cards
		foreach (var card in cardsToLimit)
		{
			card.GetComponent<CardScript>().limited = true;
			foreach (var chara in cardsLimitedTo)
			{
				card.GetComponent<CardScript>().limitedTo.Add(chara);
			}
			card.GetComponent<CardScript>().promisedTo = GameManager.me.interviewee;
		}
		// destroy cards
		//if (cardsToDestroy.Count > 0)
		{
			//foreach (var card in cardsToDestroy)
			//{
			//	PlayerScript.me.DestroyCard(card.name);
			//}
			PlayerScript.me.DestroyCard(CardUsageScript.me.cardInUsed.name);
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
					DialogueStruct d = GameManager.me.interviewee.GetComponent<CharacterScript>().dialogues[i];
					Check_apprNrelationship_then_setCardGiven(d);
					GameManager.me.interviewee.GetComponent<CharacterScript>().dialogues[i] = d;
				}
			}
		}
	}

	private void Check_apprNrelationship_then_setCardGiven(DialogueStruct tempDS)
	{
		int relationship = GameManager.me.interviewee.GetComponent<CharacterScript>().relationship;
		if (myApproach == Approaches.inquire)
		{
			if (relationship == 0)
			{
				tempDS.inquiringCard_given = true;
			}
			else if (relationship < 0)
			{
				tempDS.inquiringCard_given_p = true;
			}
			else if (relationship > 0)
			{
				tempDS.inquiringCard_given_n = true;
			}
		}
		else if (myApproach == Approaches.trade)
		{
			if (relationship == 0)
			{
				tempDS.tradingCard_given = true;
			}
			else if (relationship < 0)
			{
				tempDS.tradingCard_given_p = true;
			}
			else if (relationship > 0)
			{
				tempDS.tradingCard_given_n = true;
			}
		}
		else if (myApproach == Approaches.threaten)
		{
			if (relationship == 0)
			{
				tempDS.threatenedCard_given = true;
			}
			else if (relationship < 0)
			{
				tempDS.threatenedCard_given_p = true;
			}
			else if (relationship > 0)
			{
				tempDS.threatenedCard_given_n = true;
			}
		}
	}
	
	//public void ShowUsableCardsOnly()
	//{
	//	CharacterScript iS = GameManager.me.interviewee.GetComponent<CharacterScript>();
	//	if (availableCards.Count < iS.usableIDs.Count)
	//	{
	//		foreach (var card in GameManager.me.player.GetComponent<PlayerScript>().hand)
	//		{
	//			foreach (var id in iS.usableIDs)
	//			{
	//				if (card.GetComponent<CardScript>().id == id)
	//				{
	//					availableCards.Add(card);
	//				}
	//			}
	//		}
	//	}

	//}
}