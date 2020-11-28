﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManagerScript : MonoBehaviour
{
	static public StateManagerScript me;
	public enum States
	{
		pregame, dayOne, dayTwo, ending
	}

	public enum Charas
	{
		none, grandma, mayor1, mayor2, farmer1, farmer2, worker1, worker2, young1, young2, factory1, factory2, doc1, doc2
	}

	public States state = States.dayOne;

	public List<GameObject> dayOne_charas;
	public List<GameObject> dayTwo_charas;
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

	public GameObject gM;
	private GameManager gMS;

	private void Awake()
	{
		me = this;
	}

	private void Start()
	{
		state = States.dayOne;
		gMS = gM.GetComponent<GameManager>();
		EnterDayOne();
	}

	private void Update()
	{
		if (state == States.pregame)
		{

		}
		else if (state == States.dayOne)
		{

		}
		else if (state == States.dayTwo)
		{

		}
		else if (state == States.ending)
		{

		}
	}

	public void EnterDayOne()
	{
		// set game manager chara prefabs
		gMS.characterPrefabs.Clear();
		for (int i = 0; i < dayOne_charas.Count; i++)
		{
			gMS.characterPrefabs.Add(dayOne_charas[i]);
		}
		UnlockChara(Charas.mayor1);
		gMS.state = gMS.choose;
	}

	public void EnterDayTwo()
	{
		gMS.characterPrefabs.Clear();
		for (int i = 0; i < dayOne_charas.Count; i++)
		{
			gMS.characterPrefabs.Add(dayTwo_charas[i]);
		}
		gMS.state = gMS.choose;
	}

	public void UnlockChara(Charas chara)
	{
		switch (chara)
		{
			case Charas.grandma:
				gMS.characterPrefabs.Remove(grandmaP);
				gMS.unlockedCharaPrefabs.Add(grandmaP);
				break;
			case Charas.mayor1:
				gMS.characterPrefabs.Remove(mayor1P);
				gMS.unlockedCharaPrefabs.Add(mayor1P);
				break;
			case Charas.mayor2:
				GameManager.me.characterPrefabs.Add(mayor2P);
				break;
			case Charas.farmer1:
				gMS.characterPrefabs.Remove(farmer1P);
				gMS.unlockedCharaPrefabs.Add(farmer1P);
				break;
			case Charas.farmer2:
				GameManager.me.characterPrefabs.Add(farmer2P);
				break;
			case Charas.worker1:
				GameManager.me.characterPrefabs.Add(worker1P);
				break;
			case Charas.worker2:
				GameManager.me.characterPrefabs.Add(worker2P);
				break;
			case Charas.young1:
				GameManager.me.characterPrefabs.Add(young1P);
				break;
			case Charas.young2:
				GameManager.me.characterPrefabs.Add(young2P);
				break;
			case Charas.factory1:
				GameManager.me.characterPrefabs.Add(factory1P);
				break;
			case Charas.factory2:
				GameManager.me.characterPrefabs.Add(factory2P);
				break;
			case Charas.doc1:
				GameManager.me.characterPrefabs.Add(doc1P);
				break;
			case Charas.doc2:
				GameManager.me.characterPrefabs.Add(doc2P);
				break;

		}
	}
}