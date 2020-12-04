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
		for (int i = 0; i < currentListOf_questionOptions.Count; i++)
		{
			// show buttons
			if (currentListOf_questionOptions.Count > 0 && !currentListOf_questionOptions[i].shown) 
			{
				if (currentListOf_questionOptions[i].preconditionCharas.Count > 0) // if there is a precondition to meet
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
								print(GameManager.me.interviewee.name);
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
					print(GameManager.me.interviewee.name);
					QuestionOptionsManager.me.buttonTexts[i].text = currentListOf_questionOptions[i].question;
					var temp = currentListOf_questionOptions[i];
					temp.shown = true;
					currentListOf_questionOptions[i] = temp;
					GameManager.me.interviewee.GetComponent<CharacterScript>().cardlessDialogueFinished = false;
				}
			}
			else if (questionChosen != 0) // if dialogue is finished
			{
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
				if (GameManager.me.player != null && GameManager.me.player.GetComponent<PlayerScript>().disableMeNHand)
				{
					GameManager.me.ActivatePlayer();
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
		else if (GameManager.me.interviewee.GetComponent<CharacterScript>().relationship == 1)
		{
			currentListOf_correspondingDialogues = currentListOf_questionOptions[questionChosen - 1].dialogues_p;
		}
		else if (GameManager.me.interviewee.GetComponent<CharacterScript>().relationship == -1)
		{
			currentListOf_correspondingDialogues = currentListOf_questionOptions[questionChosen - 1].dialogues_n;
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
			CardlessDialogue_processCards(currentListOf_questionOptions[questionChosen - 1].cardsItGives_p,
										  currentListOf_questionOptions[questionChosen - 1].relationshipChangeAmount_p,
										  currentListOf_questionOptions[questionChosen - 1].charasToUnlock_p,
										  currentListOf_questionOptions[questionChosen - 1].cardsItLimits_p,
										  currentListOf_questionOptions[questionChosen - 1].cardsLimitedTo_p,
										  currentListOf_questionOptions[questionChosen - 1].cardsItDestroys_p);
		}
		else if (relationship < 0)
		{
			CardlessDialogue_processCards(currentListOf_questionOptions[questionChosen - 1].cardsItGives_n,
										  currentListOf_questionOptions[questionChosen - 1].relationshipChangeAmount_n,
										  currentListOf_questionOptions[questionChosen - 1].charasToUnlock_n,
										  currentListOf_questionOptions[questionChosen - 1].cardsItLimits_n,
										  currentListOf_questionOptions[questionChosen - 1].cardsLimitedTo_n,
										  currentListOf_questionOptions[questionChosen - 1].cardsItDestroys_n);
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
				GameObject instantiatedCard = Instantiate(card);
				GameManager.me.player.GetComponent<PlayerScript>().hand.Add(instantiatedCard);
			}
			GameManager.me.player.GetComponent<PlayerScript>().ArrangeCards();
		}
		// change interviewee's relationship
		GameManager.me.interviewee.GetComponent<CharacterScript>().relationship += relationship_changeAmount;
		// unlock characters
		if (charasToUnlock.Count > 0)
		{
			foreach (var chara in charasToUnlock)
			{
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
			foreach (var card in cardsToDestroy)
			{
				GameManager.me.player.GetComponent<PlayerScript>().DestroyCard(card.name);
			}
		}
	}

}
