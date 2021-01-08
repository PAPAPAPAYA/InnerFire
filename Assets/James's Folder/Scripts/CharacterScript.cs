using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class CharacterScript : MonoBehaviour
{
	public string namae;
	public List<GameObject> allTheCardsIGive; // for debugging only
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

	

	//public StateManagerScript.Charas whoIAm;

	// display info (currently only name)
	public TextMeshProUGUI info;

	// can be clicked
	public bool canAdvanceDialogue = true;

	// IDs of cards can be used
	public List<int> usableIDs;

	private void Start()
	{
		// store og pos and og scale
		ogPos = transform.position;
		ogScale = transform.localScale;
		foreach (var dialogue in dialogues)
		{
			foreach (var id in dialogue.triggerIDs)
			{
				usableIDs.Add(id);
			}
		}
		usableIDs = usableIDs.Distinct().ToList();
		allTheCardsIGive = new List<GameObject>();
		foreach (var dialogue in dialogues)
		{
			foreach (var card in dialogue.cards_inquiring)
			{
				allTheCardsIGive.Add(card);
			}
			foreach (var card in dialogue.cards_inquiring_n)
			{
				allTheCardsIGive.Add(card);
			}
			foreach (var card in dialogue.cards_inquiring_p)
			{
				allTheCardsIGive.Add(card);
			}
			foreach (var card in dialogue.cards_threatened)
			{
				allTheCardsIGive.Add(card);
			}
			foreach (var card in dialogue.cards_threatened_n)
			{
				allTheCardsIGive.Add(card);
			}
			foreach (var card in dialogue.cards_threatened_p)
			{
				allTheCardsIGive.Add(card);
			}
			foreach (var card in dialogue.cards_trading)
			{
				allTheCardsIGive.Add(card);
			}
			foreach (var card in dialogue.cards_trading_n)
			{
				allTheCardsIGive.Add(card);
			}
			foreach (var card in dialogue.cards_trading_p)
			{
				allTheCardsIGive.Add(card);
			}
		}
		foreach (var cardlessD in cardlessDialogues)
		{
			foreach (var option in cardlessD.options)
			{
				foreach (var card in option.cardsItGives)
				{
					allTheCardsIGive.Add(card);
				}
				foreach (var card in option.cardsItGives_n)
				{
					allTheCardsIGive.Add(card);
				}
				foreach (var card in option.cardsItGives_p)
				{
					allTheCardsIGive.Add(card);
				}
			}
		}
	}

	private void OnMouseDown()
	{
		SoundManager.me.PlayClick();
		// if in choose state, clicking the chara set the chara as the interviewee, change state to interview
		if (GameManager.me.state == GameManager.me.choose && canBeChosen)
		{
			GameManager.me.interviewee = gameObject;
			// if already exists, don't add again
			int time = 0;
			foreach (var chara in GameManager.me.interviewed)
			{
				if (chara != null &&
					chara.name == gameObject.name)
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
		else if (GameManager.me.state == GameManager.me.interview &&
					canAdvanceDialogue)
		{
			DialogueManagerScript.me.AdvanceDialogue();
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
		if (GameManager.me.state == GameManager.me.choose)
		{
			info.text = namae + "\n好感度: " + relationship;
		}
		else
		{
			info.text = "";
		}
	}

	public void ShowUsableCards()
	{
		foreach (var card in PlayerScript.me.hand)
		{
			CardScript cs = card.GetComponent<CardScript>();
			foreach (var id in usableIDs)
			{
				if (id == cs.id)
				{
					card.GetComponent<SpriteRenderer>().enabled = true;
					print(cs.name);
				}
				else
				{
					card.GetComponent<SpriteRenderer>().enabled = false;
					print("hide "+cs.name);
				}
			}
		}
	}
}