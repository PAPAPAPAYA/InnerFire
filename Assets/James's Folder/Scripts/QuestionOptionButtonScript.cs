﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionOptionButtonScript : MonoBehaviour
{
    public int number; // starts from 1

	public void SetQuestionChosen() // called in buttons
	{
		QuestionOptionsManager.me.HideQuestionButtons();
		CardlessDialogueManager.me.questionChosen = number;
		DialogueManagerScript.me.index = 0;
		CardlessDialogueManager.me.SetCurrent_correspondingDialogues();
	}
}