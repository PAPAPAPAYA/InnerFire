using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardlessDialogueManager : MonoBehaviour
{
    static public CardlessDialogueManager me;
	public int index_cardlessDialogues = 0;
	public List<CardlessDialogueStruct> currentListOf_cardlessDialogue;
	public List<QuestionOptionsStruct> currentListOf_questionOptions;
	public int questionChosen; // 0 = no question chosen, set in question option button script
	public List<string> currentListOf_correspondingDialogues;

	private void Awake()
	{
		me = this;
	}

	public void SetCurrentListOf_CD(List<CardlessDialogueStruct> cdList)
	{
		currentListOf_cardlessDialogue = cdList;
		currentListOf_questionOptions = currentListOf_cardlessDialogue[index_cardlessDialogues].options;
	}

	public void ShowQuestionButtons()
	{
		PlayerScript ps = GameManager.me.player.GetComponent<PlayerScript>();
		for (int i = 0; i < currentListOf_questionOptions.Count; i++)
		{
			// show buttons
			if (currentListOf_questionOptions.Count > 0 && !currentListOf_questionOptions[i].shown) 
			{
				// hide dialogue displayer's collider so that the buttons can be pressed
				DialogueManagerScript.me.dDisplayer.gameObject.SetActive(false);
				// check precondition
				if (currentListOf_questionOptions[i].preconditionCharas.Count > 0)
				{
					// check if precondition charas are in interviewed list
					for (int j = 0; j < GameManager.me.interviewed.Count; j++)
					{
						for (int k = 0; k < currentListOf_questionOptions[i].preconditionCharas.Count; k++)
						{
							if (GameManager.me.interviewed[j] != null &&
								GameManager.me.interviewed[j].name == currentListOf_questionOptions[i].preconditionCharas[k].name + "(Clone)")
							{
								QuestionOptionsManager.me.questionButtons[i].SetActive(true);
								GameManager.me.interviewee.GetComponent<CharacterScript>().canAdvanceDialogue = false;
								QuestionOptionsManager.me.buttonTexts[i].text = currentListOf_questionOptions[i].question;
								var temp = currentListOf_questionOptions[i];
								temp.shown = true;
								currentListOf_questionOptions[i] = temp;
								GameManager.me.interviewee.GetComponent<CharacterScript>().cardlessDialogueFinished = false;
							}
						}
					}
				}
				else
				{
					QuestionOptionsManager.me.questionButtons[i].SetActive(true);
					GameManager.me.interviewee.GetComponent<CharacterScript>().canAdvanceDialogue = false;
					QuestionOptionsManager.me.buttonTexts[i].text = currentListOf_questionOptions[i].question;
					var temp = currentListOf_questionOptions[i];
					temp.shown = true;
					currentListOf_questionOptions[i] = temp;
					GameManager.me.interviewee.GetComponent<CharacterScript>().cardlessDialogueFinished = false;
				}
			}
			else if (questionChosen != 0) // if dialogue is finished
			{
				GameManager.me.interviewee.GetComponent<CharacterScript>().canAdvanceDialogue = true;
				DialogueManagerScript.me.currentDialogue = GameManager.me.interviewee.GetComponent<CharacterScript>().defaultDialogues;
				DialogueManagerScript.me.dDisplayer.text = DialogueManagerScript.me.currentDialogue[DialogueManagerScript.me.currentDialogue.Count-1];
				DialogueManagerScript.me.index = 0;
				questionChosen = 0;
				index_cardlessDialogues = 0;
				currentListOf_cardlessDialogue.Clear();
				currentListOf_correspondingDialogues.Clear();
				currentListOf_questionOptions.Clear();
				GameManager.me.interviewee.GetComponent<CharacterScript>().cardlessDialogueFinished = true;
				gameObject.SetActive(false);
				
				// show player
				if (GameManager.me.player != null && ps.disableMeNHand)
				{
					GameManager.me.ActivatePlayer();
					ps.ShowUsableCards_Player();
					//DialogueManagerScript.me.ShowUsableCardsOnly();
				}
			}
		}
	}

	public void SetCurrent_correspondingDialogues()
	{
		if (GameManager.me.interviewee.GetComponent<CharacterScript>().relationship == 0)
		{
			currentListOf_correspondingDialogues = currentListOf_questionOptions[questionChosen - 1].dialogues;
		}
		else if (GameManager.me.interviewee.GetComponent<CharacterScript>().relationship >0)
		{
			if (currentListOf_questionOptions[questionChosen - 1].dialogues_p.Count == 0)
			{
				currentListOf_correspondingDialogues = currentListOf_questionOptions[questionChosen - 1].dialogues;
			}
			else
			{
				currentListOf_correspondingDialogues = currentListOf_questionOptions[questionChosen - 1].dialogues_p;
			}
		}
		else if (GameManager.me.interviewee.GetComponent<CharacterScript>().relationship <0)
		{
			if (currentListOf_questionOptions[questionChosen - 1].dialogues_n.Count == 0)
			{
				currentListOf_correspondingDialogues = currentListOf_questionOptions[questionChosen - 1].dialogues;
			}
			else
			{
				currentListOf_correspondingDialogues = currentListOf_questionOptions[questionChosen - 1].dialogues_n;
			}
		}
	}

	public void Check_relationship_then_endActions()
	{
		int relationship = GameManager.me.interviewee.GetComponent<CharacterScript>().relationship;
		if (relationship == 0)
		{
			CardlessDialogue_processCards(currentListOf_questionOptions[questionChosen - 1].cardsItGives,
										  currentListOf_questionOptions[questionChosen - 1].relationshipChangeAmount,
										  currentListOf_questionOptions[questionChosen - 1].charasToUnlock,
										  currentListOf_questionOptions[questionChosen - 1].cardsItLimits,
										  currentListOf_questionOptions[questionChosen - 1].cardsLimitedTo,
										  currentListOf_questionOptions[questionChosen - 1].cardsItDestroys);
		}
		else if (relationship > 0)
		{
			if (currentListOf_questionOptions[questionChosen - 1].dialogues_p.Count == 0)
			{
				CardlessDialogue_processCards(currentListOf_questionOptions[questionChosen - 1].cardsItGives,
										  currentListOf_questionOptions[questionChosen - 1].relationshipChangeAmount,
										  currentListOf_questionOptions[questionChosen - 1].charasToUnlock,
										  currentListOf_questionOptions[questionChosen - 1].cardsItLimits,
										  currentListOf_questionOptions[questionChosen - 1].cardsLimitedTo,
										  currentListOf_questionOptions[questionChosen - 1].cardsItDestroys);
			}
			else
			{
				CardlessDialogue_processCards(currentListOf_questionOptions[questionChosen - 1].cardsItGives_p,
										  currentListOf_questionOptions[questionChosen - 1].relationshipChangeAmount_p,
										  currentListOf_questionOptions[questionChosen - 1].charasToUnlock_p,
										  currentListOf_questionOptions[questionChosen - 1].cardsItLimits_p,
										  currentListOf_questionOptions[questionChosen - 1].cardsLimitedTo_p,
										  currentListOf_questionOptions[questionChosen - 1].cardsItDestroys_p);
			}
		}
		else if (relationship < 0)
		{
			if (currentListOf_questionOptions[questionChosen - 1].dialogues_n.Count == 0)
			{
				CardlessDialogue_processCards(currentListOf_questionOptions[questionChosen - 1].cardsItGives,
										  currentListOf_questionOptions[questionChosen - 1].relationshipChangeAmount,
										  currentListOf_questionOptions[questionChosen - 1].charasToUnlock,
										  currentListOf_questionOptions[questionChosen - 1].cardsItLimits,
										  currentListOf_questionOptions[questionChosen - 1].cardsLimitedTo,
										  currentListOf_questionOptions[questionChosen - 1].cardsItDestroys);
			}
			else
			{
				CardlessDialogue_processCards(currentListOf_questionOptions[questionChosen - 1].cardsItGives_n,
										  currentListOf_questionOptions[questionChosen - 1].relationshipChangeAmount_n,
										  currentListOf_questionOptions[questionChosen - 1].charasToUnlock_n,
										  currentListOf_questionOptions[questionChosen - 1].cardsItLimits_n,
										  currentListOf_questionOptions[questionChosen - 1].cardsLimitedTo_n,
										  currentListOf_questionOptions[questionChosen - 1].cardsItDestroys_n);
			}
		}
	}

	public void CardlessDialogue_processCards(List<GameObject> cardsToGive, 
											  int relationship_changeAmount, 
											  List<StateManagerScript.Charas> charasToUnlock,
											  List<GameObject> cardsToLimit,
											  List<GameObject> cardsLimitedTo,
											  List<GameObject> cardsToDestroy)
	{
		// based on the question chosen
		// add the card prefabs to player's hand 
		if (cardsToGive.Count > 0)
		{
			foreach (var card in cardsToGive)
			{
				// instantiate card
				GameObject instantiatedCard = Instantiate(card);
				HandToClueboard htc = instantiatedCard.GetComponent<HandToClueboard>();
				CardScript cs = instantiatedCard.GetComponent<CardScript>();
				// check if this card already exists in player's hand
				if (GameManager.me.player.GetComponent<PlayerScript>().hand.Contains(instantiatedCard))
				{
					// if yes, destroy it
					Destroy(instantiatedCard);
				}
				else
				{
					// if no, add it to hand and check if add to clue board
					GameManager.me.player.GetComponent<PlayerScript>().hand.Add(instantiatedCard);
					if (htc.addMeToClueBoard) // check if this card should be added to clue board, if yes
					{
						GameManager.me.newCardNotification.SetActive(true);
						ClueBoard.me.UnlockClueBoardCard(cs.namae, cs.description, htc.motiveText, htc.fireText, htc.whoToConnect, htc.imAMotiveCard, htc.imAFireCard, instantiatedCard);
					}
				}
			}
			GameManager.me.player.GetComponent<PlayerScript>().ArrangeCards();
		}
		// change interviewee's relationship
		GameManager.me.interviewee.GetComponent<CharacterScript>().relationship += relationship_changeAmount;
		//GameManager.me.interviewee.GetComponent<CharacterScript>().relationship = Mathf.Clamp(GameManager.me.interviewee.GetComponent<CharacterScript>().relationship, -1, 1);
		// unlock characters
		if (charasToUnlock.Count > 0)
		{
			foreach (var chara in charasToUnlock)
			{
				GameManager.me.newCharaNotification.SetActive(true);
				// unlock clue board characters
				switch (chara)
				{
					case StateManagerScript.Charas.mayor2:
						ClueBoard.me.UnlockMayor();
						break;
					case StateManagerScript.Charas.farmer2:
						ClueBoard.me.UnlockFarmer();
						break;
					case StateManagerScript.Charas.worker1:
						ClueBoard.me.UnlockWorker();
						break;
					case StateManagerScript.Charas.young2:
						ClueBoard.me.UnlockYoung();
						break;
				}
				StateManagerScript.me.UnlockChara(chara);
			}
		}
		// limit cards
		if (cardsToLimit.Count > 0)
		{
			foreach (var card in cardsToLimit)
			{
				card.GetComponent<CardScript>().limited = true;
				foreach (var chara in cardsLimitedTo)
				{
					card.GetComponent<CardScript>().limitedTo.Add(chara);
				}
				card.GetComponent<CardScript>().promisedTo = GameManager.me.interviewee;
			}
		}
		// destroy cards
		if (cardsToDestroy.Count > 0)
		{
			//foreach (var card in cardsToDestroy)
			//{
			//	GameManager.me.player.GetComponent<PlayerScript>().DestroyCard(card.name);
			//}
		}
	}

}
