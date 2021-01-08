using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClueBoard_Card : MonoBehaviour
{
    public string namae;
    public string des;
    public string motive;
    public string fire;
    public StateManagerScript.Charas whoICanBeUsedOn;
    public bool motiveCard;
    public bool fireCard;
    public TextMeshProUGUI cardInfo;
	public GameObject whatImAttachedTo;
	public Color32 keyColor;
	public Color32 normalColor;
	public GameObject myOG;

	private void Start()
	{
		if (!motiveCard && !fireCard)
		{
			GetComponent<SpriteRenderer>().color = normalColor;
		}
	}

	private void Update()
	{
		if (myOG == null)
		{
			ClueBoard.me.clueboard_Cards.Remove(gameObject);
			Destroy(gameObject);
		}
	}

	private void OnMouseDrag()
	{
        cardInfo.gameObject.SetActive(true);
        cardInfo.text = namae + "\n" + des;
	}
	private void OnMouseUp()
	{
        cardInfo.gameObject.SetActive(false);
        cardInfo.text = "";
	}

	private void OnMouseOver()
	{
		if (fireCard || motiveCard)
		{
			foreach (var line in ClueBoard.me.lines)
			{
				LineScript ls = line.GetComponent<LineScript>();
				if (!line.GetComponent<LineScript>().released)
				{
					//whatImAttachedTo = ls.memoAttached;
					//ls.bT = transform;
					ls.cardAttached = gameObject;
				}
			}
		}
	}

	private void OnMouseExit()
	{
		if (fireCard || motiveCard)
		{
			foreach (var line in ClueBoard.me.lines)
			{
				LineScript ls = line.GetComponent<LineScript>();
				if (!line.GetComponent<LineScript>().released)
				{
					//whatImAttachedTo = ls.memoAttached;
					//ls.bT = transform;
					ls.cardAttached = null;
				}
			}
		}
		
	}

}
