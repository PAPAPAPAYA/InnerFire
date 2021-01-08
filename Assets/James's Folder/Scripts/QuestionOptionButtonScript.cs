using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionOptionButtonScript : MonoBehaviour
{
    public int number; // starts from 1

	public void SetQuestionChosen() // called in buttons
	{
		SoundManager.me.PlayClick();
		QuestionOptionsManager.me.HideQuestionButtons();
		CardlessDialogueManager.me.questionChosen = number;
		DialogueManagerScript.me.index = 0;
		CardlessDialogueManager.me.SetCurrent_correspondingDialogues();
		CardlessDialogueManager.me.Check_relationship_then_endActions();
		GameManager.me.interviewee.GetComponent<CharacterScript>().canAdvanceDialogue = true;
		DialogueManagerScript.me.dDisplayer.gameObject.SetActive(true);
	}
}
