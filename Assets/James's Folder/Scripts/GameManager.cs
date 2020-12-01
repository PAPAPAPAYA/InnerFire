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

	//
	public GameObject grandmaP;
	public GameObject mayor1P;
	public GameObject mayor2P;
	public GameObject farmer1P;
	public GameObject farmer2P;
	public GameObject worker1P;
	public GameObject worker2P;
	public GameObject factory1P;
	public GameObject factory2P;
	public GameObject young1P;
	public GameObject young2P;
	public GameObject doc1P;
	public GameObject doc2P;

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
		//state = pre;
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
					cardlessDialogueManager.SetActive(true);
				}
				else
				{
					// instantiate player
					player = Instantiate(playerPrefab);
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
		// reset interviewee
		interviewee = null;
		// change state
		state = choose;
		// reset dialogue
		DialogueManagerScript.me.dDisplayer.text = "";
		// tell player to destory itself (if player exists)
		if (player != null)
		{
			player.GetComponent<PlayerScript>().destroyMe = true;
		}
		// set roster pos
		for (int i = 0; i < roster.Count; i++)
		{
			Vector3 pos = new Vector3(rosterSect_startPos.x + (i + 1) * rosterSect_length / (roster.Count + 1), rosterSect_startPos.y, 0);
			roster[i].transform.position = pos;
		}
	}
}
