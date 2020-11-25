using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{
	public List<QuestionOptionsStruct> options; // for choosing different questions before using any cards
	public List<DialogueStruct> dialogues; // stores all the dialogues for all the approaches
	[TextArea]
	public List<string> defaultDialogues;
	public Vector3 ogPos; // position in character choosing screen
	public Vector3 ogScale; // scale in character choosing screen

	private void Start()
	{
		// store og pos and og scale
		ogPos = transform.position;
		ogScale = transform.localScale;
	}

	private void OnMouseDown()
	{
		// if in choose state, clicking the chara set the chara as the interviewee, change state to interview
		if (GameManager.me.state == GameManager.me.choose)
		{
			GameManager.me.interviewee = gameObject;
		}
		// if in interview state, clicking the chara advance the dialogue
		else if (GameManager.me.state == GameManager.me.interview)
		{
			DialogueManagerScript.me.AdvanceDialogue();
		}
	}
}