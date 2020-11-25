using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	static public GameManager me;

    public int state;
    public int choose = 0;
    public int interview = 1;
	// for choosing who to interview
	public List<GameObject> characterPrefabs;
	public Vector3 rosterSect_startPos;
	public float rosterSect_length;
	public List<GameObject> roster;

	// interview
	public GameObject interviewee;
	public Vector3 interviewee_pos;
	public GameObject playerPrefab;
	public GameObject player;
	public GameObject backButton;

	private void Start()
	{
		me = this;
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
			// initialize characters
			if (roster.Count < characterPrefabs.Count && interviewee == null)
			{
				for (int i = 0; i < characterPrefabs.Count; i++)
				{
					// instantiate chara and set pos
					GameObject character = Instantiate(characterPrefabs[i]);
					Vector3 pos = new Vector3(rosterSect_startPos.x + (i + 1) * rosterSect_length / (characterPrefabs.Count + 1), rosterSect_startPos.y, 0);
					character.transform.position = pos;
					// add it to a list
					roster.Add(character);
				}
			}
			// if an interviewee is selected, go to interview state
			else if (interviewee != null)
			{
				// if not selected, hide
				foreach(GameObject chara in roster)
				{
					if (chara != interviewee)
					{
						chara.GetComponent<SpriteRenderer>().enabled = false;
					}
				}
				// put interviewee chara to interview pos and scale up
				interviewee.transform.position = interviewee_pos;
				interviewee.transform.localScale = new Vector3(2, 2, 1);
				// instantiate player
				player = Instantiate(playerPrefab);
				// set state to interview state
				state = interview;
			}
		}
		// when in interview, show [back] button
		else if (state == interview)
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
		// tell player to destory itself
		player.GetComponent<PlayerScript>().destroyMe = true;
	}
}
