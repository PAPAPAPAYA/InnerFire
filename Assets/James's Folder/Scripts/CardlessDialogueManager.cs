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
							if (GameManager.me.interviewed[j].name == currentListOf_questionOptions[i].preconditionCharas[k].name + "(Clone)")
							{
								QuestionOptionsManager.me.questionButtons[i].SetActive(true);
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
				if (GameManager.me.player != null && GameManager.me.player.GetComponent<PlayerScript>().hideMeNHand)
				{
					GameManager.me.ActivatePlayer();
				}
			}
		}
	}

	public void SetCurrent_correspondingDialogues()
	{
		currentListOf_correspondingDialogues = currentListOf_questionOptions[questionChosen - 1].dialogues;
	}

	public void CardlessDialogue_processCards()
	{
		// based on the question chosen
		// add the card prefabs to player's hand 
		if (currentListOf_questionOptions[questionChosen - 1].cardsItGives.Count > 0)
		{
			foreach (var card in currentListOf_questionOptions[questionChosen - 1].cardsItGives)
			{
				GameObject instantiatedCard = Instantiate(card);
				GameManager.me.player.GetComponent<PlayerScript>().hand.Add(instantiatedCard);
			}
			GameManager.me.player.GetComponent<PlayerScript>().ArrangeCards();
		}
		// change interviewee's relationship
		GameManager.me.interviewee.GetComponent<CharacterScript>().relationship += currentListOf_questionOptions[questionChosen - 1].relationshipChangeAmount;
		// unlock characters
		if (currentListOf_questionOptions[questionChosen - 1].charasToUnlock.Count > 0)
		{
			foreach (var chara in currentListOf_questionOptions[questionChosen - 1].charasToUnlock)
			{
				StateManagerScript.me.UnlockChara(chara);
			}
		}
		// limit cards
		if (currentListOf_questionOptions[questionChosen - 1].cardsItLimits.Count > 0)
		{
			foreach (var card in currentListOf_questionOptions[questionChosen - 1].cardsItLimits)
			{
				card.GetComponent<CardScript>().limited = true;
				foreach (var chara in currentListOf_questionOptions[questionChosen - 1].cardsLimitedTo)
				{
					card.GetComponent<CardScript>().limitedTo.Add(chara);
				}
				card.GetComponent<CardScript>().promisedTo = GameManager.me.interviewee;
			}
		}
		// destroy cards
		if (currentListOf_questionOptions[questionChosen - 1].cardsItDestroys.Count > 0)
		{
			foreach (var card in currentListOf_questionOptions[questionChosen - 1].cardsItDestroys)
			{
				GameManager.me.player.GetComponent<PlayerScript>().DestroyCard(card.name);
			}
		}
	}

}
