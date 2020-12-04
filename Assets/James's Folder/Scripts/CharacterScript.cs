using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterScript : MonoBehaviour
{
	public string namae;
	public List<DialogueStruct> dialogues; // stores all the dialogues for all the approaches
	[TextArea]
	public List<string> defaultDialogues;
	public List<CardlessDialogueStruct> cardlessDialogues; // each cardless dialogue stores two question options, each question option has its own dialogue
	public int relationship;
	public Vector3 ogPos; // position in character choosing screen
	public Vector3 ogScale; // scale in character choosing screen
	public bool cardlessDialogueFinished = false;

	public bool unlocked = false;

	public bool destoryMe = false;

	public bool re_interview_ability = false;
	public bool canBeChosen = true;

	// display info (currently only name)
	public TextMeshProUGUI info;

	private void Start()
	{
		// store og pos and og scale
		ogPos = transform.position;
		ogScale = transform.localScale;
	}

	private void OnMouseDown()
	{
		// if in choose state, clicking the chara set the chara as the interviewee, change state to interview
		if (GameManager.me.state == GameManager.me.choose && canBeChosen)
		{
			GameManager.me.interviewee = gameObject;
			// if already exists, don't add again
			int time = 0;
			foreach (var chara in GameManager.me.interviewed)
			{
				if (chara.name == gameObject.name)
				{
					time++;
				}
			}
			if (time == 0)
			{
				GameManager.me.interviewed.Add(gameObject);
			}
		}
		// if in interview state, clicking the chara advance the dialogue
		else if (GameManager.me.state == GameManager.me.interview)
		{
			DialogueManagerScript.me.AdvanceDialogue();
		}
	}

	private void OnMouseOver()
	{
		if (GameManager.me.state == GameManager.me.choose)
		{
			info.text = namae;
		}
	}

	private void OnMouseExit()
	{
		info.text = "";
	}

	private void Update()
	{
		if (destoryMe)
		{
			GameManager.me.roster.Remove(gameObject);
			Destroy(gameObject);
		}
	}
}