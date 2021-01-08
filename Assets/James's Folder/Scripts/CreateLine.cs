using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateLine : MonoBehaviour
{
	public string whoIsIt;
	public GameObject linePrefab;
	private LineScript ls;
	private GameObject line;
	private GameObject mousePos;
	public TextMeshProUGUI motiveText_displayer;
	public StateManagerScript.Charas whoIAm;
	public List<GameObject> lines;
	public bool motiveMemo;
	public bool fireMemo;

	public int motive_matchTimes;
	public int fire_matchTimes;

	private void Start()
	{
		mousePos = new GameObject();
		mousePos.transform.position = gameObject.transform.position;
		mousePos.transform.parent = gameObject.transform;
		lines = new List<GameObject>();
	}

	private void OnMouseDown()
	{
		if (!ClueBoard.me.cutMode)
		{
			line = Instantiate(linePrefab);
			ClueBoard.me.lines.Add(line);
			lines.Add(line);
			line.GetComponent<LineScript>().memoAttached = gameObject;
		}
	}

	private void OnMouseDrag()
	{
		if (!ClueBoard.me.cutMode)
		{
			ls = line.GetComponent<LineScript>();
			ls.transform.parent = gameObject.transform;
			ls.aT = gameObject.transform;
			mousePos.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10+gameObject.transform.localPosition.z));
			ls.bT = mousePos.transform;
			// check if mouse is inside card
			foreach (var card in ClueBoard.me.clueboard_Cards)
			{
				if (mousePos.transform.position.x > card.transform.position.x - card.transform.lossyScale.x / 2 &&
					mousePos.transform.position.x < card.transform.position.x + card.transform.lossyScale.x / 2 &&
					mousePos.transform.position.y > card.transform.position.y - card.transform.lossyScale.y / 2 &&
					mousePos.transform.position.y < card.transform.position.y + card.transform.lossyScale.y / 2)
				{
					//print(card.GetComponent<ClueBoard_Card>().namae+"attach");
					ls.bT = card.transform;
					//ls.cardAttached = card;
				}
			}
		}
	}

	private void OnMouseUp()
	{
		if (!ClueBoard.me.cutMode)
		{
			if (ls.cardAttached == null)
			{
				ClueBoard.me.lines.Remove(line);
				lines.Remove(line);
				line.GetComponent<LineScript>().memoAttached = null;
				Destroy(line);
			}
			else
			{
				SoundManager.me.PlayPin();
				UpdateMotiveText();
			}
			ls.released = true;
		}
	}

	public void UpdateMotiveText()
	{
		if (motiveMemo)
		{
			motiveText_displayer.text = "他的动机是？";

			foreach (var line in lines)
			{
				ClueBoard_Card cbc = line.GetComponent<LineScript>().cardAttached.GetComponent<ClueBoard_Card>();
				if (cbc.whoICanBeUsedOn == whoIAm && cbc.motiveCard)
				{
					motiveText_displayer.text += "\n" + cbc.motive;
					motive_matchTimes++;
				}
			}
		}
		else
		{
			motiveText_displayer.text = "他与火灾的关系？";

			foreach (var line in lines)
			{
				ClueBoard_Card cbc = line.GetComponent<LineScript>().cardAttached.GetComponent<ClueBoard_Card>();
				if (cbc.whoICanBeUsedOn == whoIAm && cbc.fireCard)
				{
					motiveText_displayer.text += "\n" + cbc.fire;
					fire_matchTimes++;
				}
			}
		}
	}
}
