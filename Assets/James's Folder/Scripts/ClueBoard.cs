using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueBoard : MonoBehaviour
{
    public static ClueBoard me;
    public List<GameObject> clueboard_Cards;
	public bool cutMode;
	public List<GameObject> lines;
	public GameObject mayor;
	public GameObject farmer;
	public GameObject worker;
	public GameObject young;
	public GameObject clueboardCardPrefab;
	//public GameObject linePlayersHolding;

	private void Awake()
	{
		me = this;
	}
	public void UnlockMayor()
	{
		mayor.SetActive(true);
	}
	public void UnlockWorker()
	{
		worker.SetActive(true);
	}
	public void UnlockFarmer()
	{
		farmer.SetActive(true);
	}
	public void UnlockYoung()
	{
		young.SetActive(true);
	}

	public void UnlockClueBoardCard(string name, string des, string motiveText, string fireText, StateManagerScript.Charas who, bool motive, bool fire, GameObject ogObject)
	{
		GameObject newCard = Instantiate(clueboardCardPrefab, gameObject.transform);
		if (clueboard_Cards.Contains(newCard)) // if this card already exists in clue board
		{
			// don't create again
			Destroy(newCard);
		}
		else
		{
			ClueBoard_Card cbc = newCard.GetComponent<ClueBoard_Card>();
			newCard.transform.position = new Vector3(Random.Range(200 - 1, 200 + 1), Random.Range(-1f, +1f), -0.1f);
			cbc.namae = name;
			cbc.des = des;
			cbc.motive = motiveText;
			cbc.fire = fireText;
			cbc.whoICanBeUsedOn = who;
			cbc.motiveCard = motive;
			cbc.fireCard = fire;
			cbc.myOG = ogObject;
			clueboard_Cards.Add(newCard);
		}
	}
}
