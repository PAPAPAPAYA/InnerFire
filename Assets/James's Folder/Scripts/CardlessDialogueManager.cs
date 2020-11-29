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
		for (int i = 0; i < QuestionOptionsManager.me.questionButtons.Count; i++)
		{
			if (currentListOf_questionOptions.Count > 0 && !currentListOf_questionOptions[i].shown) // questiong option not shown
			{
				QuestionOptionsManager.me.questionButtons[i].SetActive(true);
				QuestionOptionsManager.me.buttonTexts[i].text = currentListOf_questionOptions[i].question;
				var temp = currentListOf_questionOptions[i];
				temp.shown = true;
				currentListOf_questionOptions[i] = temp;
			}
			else // if all the cardless dialogues are shown
			{
				// instantiate player
				if (GameManager.me.player == null)
				{
					GameManager.me.player = Instantiate(GameManager.me.playerPrefab);
				}
				// set dDispalyer to display defualt message and reset and disable self
				DialogueManagerScript.me.currentDialogue = GameManager.me.interviewee.GetComponent<CharacterScript>().defaultDialogues;
				DialogueManagerScript.me.dDisplayer.text = DialogueManagerScript.me.currentDialogue[0];
				DialogueManagerScript.me.index = 0;
				questionChosen = 0;
				index_cardlessDialogues = 0;
				currentListOf_cardlessDialogue.Clear();
				currentListOf_correspondingDialogues.Clear();
				currentListOf_questionOptions.Clear();
				GameManager.me.interviewee.GetComponent<CharacterScript>().cardlessDialogueFinished = true;
				gameObject.SetActive(false);
			}
		}
	}

	public void SetCurrent_correspondingDialogues()
	{
		currentListOf_correspondingDialogues = currentListOf_questionOptions[questionChosen - 1].dialogues;
	}

	public void CardlessDialogueEnd_Actions()
	{
		// based on the question chosen
		// add the card prefabs to player's hand 
		if (currentListOf_questionOptions[questionChosen - 1].cardsItGives.Count > 0)
		{
			foreach (var card in currentListOf_questionOptions[questionChosen - 1].cardsItGives)
			{
				GameManager.me.playerPrefab.GetComponent<PlayerScript>().handPrefabs.Add(card);
			}
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
			foreach (var cards in currentListOf_questionOptions[questionChosen - 1].cardsItLimits)
			{
				cards.GetComponent<CardScript>().limited = true;
				cards.GetComponent<CardScript>().promisedTo = GameManager.me.interviewee;
			}
		}
		// destroy cards

	}

}
