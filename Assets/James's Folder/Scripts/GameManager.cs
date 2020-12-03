using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	static public GameManager me;

    public int state;
	public int pre = 0;
    public int choose = 0;
    public int interview = 1;
	// for choosing who to interview
	public List<GameObject> characterPrefabs;
	public List<GameObject> unlockedCharaPrefabs;
	public Vector3 rosterSect_startPos;
	public float rosterSect_length;
	public List<GameObject> roster;

	// interview
	public GameObject interviewee;
	public Vector3 interviewee_pos;
	public GameObject playerPrefab;
	public GameObject player;
	public GameObject backButton;

	// cardless dialogue
	public GameObject cardlessDialogueManager;

	// record who the player has already interviewed
	public List<GameObject> interviewed;

	private void Start()
	{
		me = this;
		for (int i = 0; i < unlockedCharaPrefabs.Count; i++)
		{
			// instantiate
			GameObject character = Instantiate(unlockedCharaPrefabs[i]);
			// set its pos
			Vector3 pos = new Vector3(rosterSect_startPos.x + (i + 1) * rosterSect_length / (unlockedCharaPrefabs.Count + 1), rosterSect_startPos.y, 0);
			character.transform.position = pos;
			// add it to roster
			roster.Add(character);
		}
		if (player == null)
		{
			player = Instantiate(playerPrefab);
			player.GetComponent<PlayerScript>().disableMeNHand = true;
		}
	}

	private void Update()
	{
		if (state == choose)
		{
			// hide [back] button when choosing who to interview
			backButton.SetActive(false);
			// show characters
			foreach (GameObject chara in roster)
			{
				chara.GetComponent<SpriteRenderer>().enabled = true;
			}
			// initialize roster
			if (roster.Count < unlockedCharaPrefabs.Count && interviewee == null)
			{
				// instantiate unlocked charas
				for (int i = 0; i < unlockedCharaPrefabs.Count; i++)
				{
					// get roster
					List<GameObject> tempRoster = new List<GameObject>();
					foreach (var chara in roster)
					{
						tempRoster.Add(chara);
					}
					// if not instantiated
					int showUpTime = 0;
					foreach (var chara in tempRoster)
					{
						if (unlockedCharaPrefabs[i].name + "(Clone)" == chara.name)
						{
							showUpTime++;
						}
					}
					if (showUpTime == 0)
					{
						// instantiate
						GameObject character = Instantiate(unlockedCharaPrefabs[i]);
						// set its pos
						Vector3 pos = new Vector3(rosterSect_startPos.x + (i + 1) * rosterSect_length / (unlockedCharaPrefabs.Count + 1), rosterSect_startPos.y, 0);
						character.transform.position = pos;
						// add it to roster
						roster.Add(character);
					}
				}
				// set roster pos
				for (int i = 0; i < roster.Count; i++)
				{
					Vector3 pos = new Vector3(rosterSect_startPos.x + (i + 1) * rosterSect_length / (roster.Count + 1), rosterSect_startPos.y, 0);
					roster[i].transform.position = pos;
				}
			}
			// if an interviewee is selected, go to interview state
			else if (interviewee != null)
			{
				// activate cardless dialogue manager if the cardless dialogue for the selected chara hasn't been shown
				if (!interviewee.GetComponent<CharacterScript>().cardlessDialogueFinished) 
				{
					if (interviewee.GetComponent<CharacterScript>().cardlessDialogues.Count > 0) // if the interviewee has at least one cardless dialogue
					{
						CheckQuestionOptionsPrecondition(); // check each option in each dialogue and mark each option if its precondition is met
						int preconditionMetTimes = 0;
						// check each dialogue and mark each dialgoue if none of its option is unlocked
						for (int i = 0; i < interviewee.GetComponent<CharacterScript>().cardlessDialogues.Count; i++)
						{
							for (int j = 0; j < interviewee.GetComponent<CharacterScript>().cardlessDialogues[i].options.Count; j++)
							{
								if (interviewee.GetComponent<CharacterScript>().cardlessDialogues[i].options[j].precondition_met)
								{
									preconditionMetTimes++;
								}
							}
							// if none of the option is unlocked, set noneOptionUnlocked to true
							if (preconditionMetTimes == 0)
							{
								CardlessDialogueStruct thisCDstruct = interviewee.GetComponent<CharacterScript>().cardlessDialogues[i];
								thisCDstruct.noneOptionUnlocked = true;
								interviewee.GetComponent<CharacterScript>().cardlessDialogues[i] = thisCDstruct;
							}
							else // if there is at least one option unlocked, set noneOptionUnlocked to false
							{
								CardlessDialogueStruct thisCDstruct = interviewee.GetComponent<CharacterScript>().cardlessDialogues[i];
								thisCDstruct.noneOptionUnlocked = false;
								interviewee.GetComponent<CharacterScript>().cardlessDialogues[i] = thisCDstruct;
							}
						}
						// if none of the dialogue has unlocked option, don't activate the cardless Dialogue Manager
						int noneOptionUnlockedTimes = 0;
						foreach (var dialogue in interviewee.GetComponent<CharacterScript>().cardlessDialogues)
						{
							if (dialogue.noneOptionUnlocked)
							{
								noneOptionUnlockedTimes++;
							}
						}
						if (noneOptionUnlockedTimes < interviewee.GetComponent<CharacterScript>().cardlessDialogues.Count)
						{
							cardlessDialogueManager.SetActive(true);
						}
					}
				}
				else
				{
					// instantiate player
					if (player == null)
					{
						player = Instantiate(playerPrefab);
					}
					else
					{
						if (player.GetComponent<PlayerScript>().disableMeNHand)
						{
							ActivatePlayer();
						}
					}
				}

				// if not selected, hide
				foreach (GameObject chara in roster)
				{
					if (chara != interviewee)
					{
						chara.GetComponent<SpriteRenderer>().enabled = false;
					}
				}
				// put interviewee chara to interview pos and scale up
				interviewee.transform.position = interviewee_pos;
				interviewee.transform.localScale = new Vector3(2, 2, 1);
				
				// set state to interview state
				state = interview;
			}
		}
		// when in interview (and cardless dialogues are finished), show [back] button
		else if (state == interview && interviewee.GetComponent<CharacterScript>().cardlessDialogueFinished)
		{
			backButton.SetActive(true);
		}
	}

	public void ExitInterview()
	{
		// set interviewee back to before selected
		interviewee.transform.position = interviewee.GetComponent<CharacterScript>().ogPos;
		interviewee.transform.localScale = interviewee.GetComponent<CharacterScript>().ogScale;
		if (interviewee.GetComponent<CharacterScript>().re_interview_ability == false)
		{
			interviewee.GetComponent<CharacterScript>().canBeChosen = false;
			interviewee.GetComponent<SpriteRenderer>().color = Color.grey;
		}
		// reset interviewee
		interviewee = null;
		// change state
		state = choose;
		// reset dialogue
		DialogueManagerScript.me.dDisplayer.text = "";
		// tell player to hide itself and hand (if player exists)
		if (player != null)
		{
			player.GetComponent<PlayerScript>().disableMeNHand = true;
		}
		// set roster pos
		for (int i = 0; i < roster.Count; i++)
		{
			Vector3 pos = new Vector3(rosterSect_startPos.x + (i + 1) * rosterSect_length / (roster.Count + 1), rosterSect_startPos.y, 0);
			roster[i].transform.position = pos;
		}
	}

	public void ActivatePlayer()
	{
		player.GetComponent<PlayerScript>().disableMeNHand = false;
		foreach (var card in player.GetComponent<PlayerScript>().hand)
		{
			card.GetComponent<SpriteRenderer>().enabled = true;
			card.GetComponent<CardScript>().canBeDragged = true;
		}
		player.GetComponent<PlayerScript>().ArrangeCards();
	}

	private void CheckQuestionOptionsPrecondition()
	{
		for (int i = 0; i < interviewee.GetComponent<CharacterScript>().cardlessDialogues.Count; i++) // look at all the cardless dialogues of the interviewee
		{
			for (int j = 0; j < interviewee.GetComponent<CharacterScript>().cardlessDialogues[i].options.Count; j++) // look at all the options of each cardless dialogues
			{
				if (interviewee.GetComponent<CharacterScript>().cardlessDialogues[i].options[j].preconditionCharas.Count > 0)
				{
					int matchTimes = 0;
					for (int k = 0; k < interviewee.GetComponent<CharacterScript>().cardlessDialogues[i].options[j].preconditionCharas.Count; k++) // look at all the precondition of each option
					{
						for (int q = 0; q < interviewed.Count; q++) // compare the precondition with interviewed
						{
							if (interviewed[q].name == interviewee.GetComponent<CharacterScript>().cardlessDialogues[i].options[j].preconditionCharas[k].name + "(Clone)")
							{
								matchTimes++; // if there is a match, matchTimes++;
							}
						}
					}
					if (matchTimes == interviewee.GetComponent<CharacterScript>().cardlessDialogues[i].options[j].preconditionCharas.Count) // if matchTimes equals the number of preconditions
					{
						// set precondition_met to true;
						QuestionOptionsStruct thisOption = interviewee.GetComponent<CharacterScript>().cardlessDialogues[i].options[j];
						thisOption.precondition_met = true;
						interviewee.GetComponent<CharacterScript>().cardlessDialogues[i].options[j] = thisOption;
					}
				}
				else
				{
					QuestionOptionsStruct thisOption = interviewee.GetComponent<CharacterScript>().cardlessDialogues[i].options[j];
					thisOption.precondition_met = true;
					interviewee.GetComponent<CharacterScript>().cardlessDialogues[i].options[j] = thisOption;
				}
			}
		}
		
	}
}
