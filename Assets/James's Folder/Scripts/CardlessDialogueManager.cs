using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardlessDialogueManager : MonoBehaviour
{
    static public CardlessDialogueManager me;
	public int index = 0;
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
		currentListOf_questionOptions = currentListOf_cardlessDialogue[index].options;
	}

	public void ShowQuestionButtons()
	{
		for (int i = 0; i < QuestionOptionsManager.me.questionButtons.Count; i++)
		{
			QuestionOptionsManager.me.questionButtons[i].SetActive(true);
			QuestionOptionsManager.me.buttonTexts[i].text = currentListOf_questionOptions[i].question;
		}
	}

	public void SetCurrent_correspondingDialogues()
	{
		currentListOf_correspondingDialogues = currentListOf_questionOptions[questionChosen - 1].dialogues;
	}
	
}
