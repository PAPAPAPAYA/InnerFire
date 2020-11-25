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
					GiveCard(); // give player the card this approach gives
					if (GameManager.me.player == null) // if there is no player, make one
					{
						GameManager.me.player = Instantiate(GameManager.me.playerPrefab);
					}
					myApproach = Approaches.na; // set my approach
				}
				else
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
					else // if all the cardless dialogues of the interviewee are cycle through
					{
						// disable cardless dialogue manager and spawn player, meaning the player can use cards now
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
			if (CardlessDialogueManager.me.questionChosen == 0) // no question chosen
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
		// hide everything other than the dialogue
		Destroy(CardUsageScript.me.cardInUsed);
		CardUsageScript.me.cardInUsed = null;
		if (GameManager.me.player != null)
		{
			GameManager.me.player.GetComponent<PlayerScript>().destroyMe = true;
		}
		// destroy or limit: implement here
	}

	public void GiveCard()
	{
		if (myApproach == Approaches.inquire &&
			!chunk.inquiringCard_given)
		{
			// give inquire card
			if (chunk.cards_inquiring.Count > 0)
			{
				GameManager.me.playerPrefab.GetComponent<PlayerScript>().handPrefabs.Add(chunk.cards_inquiring[0]);
				SetCardGiven();
			}
		}
		else if (myApproach == Approaches.threaten &&
				!chunk.threatenedCard_given)
		{
			// give threatened card
			if (chunk.cards_inquiring.Count > 0)
			{
				GameManager.me.playerPrefab.GetComponent<PlayerScript>().handPrefabs.Add(chunk.cards_threatened[0]);
				SetCardGiven();
			}
		}
		else if (myApproach == Approaches.trade &&
				!chunk.tradingCard_given)
		{
			// give trading card
			if (chunk.cards_inquiring.Count > 0)
			{
				GameManager.me.playerPrefab.GetComponent<PlayerScript>().handPrefabs.Add(chunk.cards_trading[0]);
				SetCardGiven();
			}
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

	private void DoQuestionsOptions()
	{

	}
}