using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
	public TextMeshProUGUI charaEndingText;
	public GameObject noInfoText;
	// 1 - only game over title
	public GameObject firstGameOverText;
	public GameObject gameOverTitle;
	// 2 - farmer
	public GameObject farmer;
	public GameObject cb_farmer_motive;
	public GameObject cb_farmer_fire;
	public List<GameObject> farmer_endings;
	public GameObject farmer_fireEnding;
	public List<GameObject> farmer_motiveEndings;
	// 3 - worker
	public GameObject worker;
	public GameObject cb_worker_motive;
	public GameObject cb_worker_fire;
	public List<GameObject> worker_endings;
	public GameObject worker_fireEnding;
	public List<GameObject> worker_motiveEndings;
	// 4 - mayor
	public GameObject mayor;
	public GameObject cb_mayor_motive;
	public GameObject cb_mayor_fire;
	public List<GameObject> mayor_endings;
	public GameObject mayor_fireEnding;
	public List<GameObject> mayor_motiveEndings;
	// 5 - young
	public GameObject young;
	public GameObject cb_young_motive;
	public GameObject cb_young_fire;
	public List<GameObject> young_endings;
	public GameObject young_fireEnding;
	public List<GameObject> young_motiveEndings;
	// text before poem
	public GameObject textBeforePoem;
	// Inner Fire by vl
	public GameObject poem1;
	public GameObject poem2;
	// thank you for playing + restart + close game
	public GameObject thankYouNote;
	public GameObject restartButton;
	public GameObject closeGameButton;

	public bool showingCharaEnding;

	private int slideNum = 1;

	public int index = 0;

	private void OnMouseDown()
	{
		if (slideNum > 0 && slideNum < 5)
		{
			CheckEndingUnlocking(slideNum);
			//charaEndingText.gameObject.SetActive(true);
		}
		//slideNum++;
		switch (slideNum)
		{
			case 1: // farmer
				gameOverTitle.SetActive(false);
				firstGameOverText.SetActive(false);
				farmer.SetActive(true);
				if (index < farmer_endings.Count)
				{
					foreach (var ending in farmer_endings)
					{
						ending.SetActive(false);
					}
					farmer_endings[index].SetActive(true);
				}
				index++;
				if (index > farmer_endings.Count)
				{
					foreach (var ending in farmer_endings)
					{
						ending.SetActive(false);
					}
					farmer.SetActive(false);
					slideNum++;
					index = 0;
				}
				break;
			case 2: // worker
				worker.SetActive(true);
				if (index < worker_endings.Count)
				{
					foreach (var ending in worker_endings)
					{
						ending.SetActive(false);
					}
					worker_endings[index].SetActive(true);
				}
				index++;
				if (index > worker_endings.Count)
				{
					foreach (var ending in worker_endings)
					{
						ending.SetActive(false);
					}
					worker.SetActive(false);
					slideNum++;
					index = 0;
				}
				break;
			case 3: // mayor
				mayor.SetActive(true);
				if (index < mayor_endings.Count)
				{
					foreach (var ending in mayor_endings)
					{
						ending.SetActive(false);
					}
					mayor_endings[index].SetActive(true);
				}
				index++;
				if (index > mayor_endings.Count)
				{
					foreach (var ending in mayor_endings)
					{
						ending.SetActive(false);
					}
					mayor.SetActive(false);
					slideNum++;
					index = 0;
				}
				break;
			case 4: // young
				young.SetActive(true);
				if (index < young_endings.Count)
				{
					foreach (var ending in young_endings)
					{
						ending.SetActive(false);
					}
					young_endings[index].SetActive(true);
				}
				index++;
				if (index > young_endings.Count)
				{
					foreach (var ending in young_endings)
					{
						ending.SetActive(false);
					}
					young.SetActive(false);
					slideNum++;
					index = 0;
				}
				break;
			case 5:
				charaEndingText.gameObject.SetActive(false);
				textBeforePoem.SetActive(true);
				slideNum++;
				break;
			case 6:
				textBeforePoem.SetActive(false);
				poem1.SetActive(true);
				poem2.SetActive(true);
				slideNum++;
				break;
			case 7: // thank you for playing and shit
				poem1.SetActive(false);
				poem2.SetActive(false);
				thankYouNote.SetActive(true);
				restartButton.SetActive(true);
				closeGameButton.SetActive(true);
				break;
		}
		
	}

	private void CheckEndingUnlocking(int slideNum)
	{
		// 1: farmer
		if (slideNum == 1)
		{
			//print("farmer motive: "+farmer.GetComponent<EndingCharacter>().amountOf_motiveInfo / 2);
			//print("farmer fire: " + farmer.GetComponent<EndingCharacter>().amountOf_fireInfo / 2);
			bool motiveEndingObtained = false;
			bool fireEndingObtained = false;
			// check unlocked amount of motive
			if (cb_farmer_motive.GetComponent<CreateLine>().motive_matchTimes > farmer.GetComponent<EndingCharacter>().amountOf_motiveInfo / 2)
			{
				print("farmer motive info enough");
				motiveEndingObtained = true;
			}
			// check unlocked amount of fire
			if (cb_farmer_fire.GetComponent<CreateLine>().fire_matchTimes > farmer.GetComponent<EndingCharacter>().amountOf_fireInfo / 2)
			{
				print("farmer fire info enough");
				fireEndingObtained = true;
			}
			// add endings
			if (farmer_endings.Count == 0)
			{
				if (fireEndingObtained) // ending fire
				{
					farmer_endings.Add(farmer_fireEnding);
				}
				if (motiveEndingObtained) // ending motive
				{
					foreach (var ending in farmer_motiveEndings)
					{
						farmer_endings.Add(ending);
					}
				}
				if (!motiveEndingObtained && !fireEndingObtained) // ending nothing
				{
					// add a general ending object that says no info was obtained for this chara
					farmer_endings.Add(noInfoText);
				}
			}
		}
		// 2: worker
		else if (slideNum == 2)
		{
			//print("worker motive amount needed: " + worker.GetComponent<EndingCharacter>().amountOf_motiveInfo / 2);
			//print("worker fire amount needed: " + worker.GetComponent<EndingCharacter>().amountOf_fireInfo / 2);
			//print("worker motive amount: " + cb_worker_motive.GetComponent<CreateLine>().motive_matchTimes);
			bool motiveEndingObtained = false;
			bool fireEndingObtained = false;
			// check unlocked amount of motive
			if (cb_worker_motive.GetComponent<CreateLine>().motive_matchTimes > worker.GetComponent<EndingCharacter>().amountOf_motiveInfo / 2)
			{
				print("worker motive info enough");
				motiveEndingObtained = true;
			}
			// check unlocked amount of fire
			if (cb_worker_fire.GetComponent<CreateLine>().fire_matchTimes > worker.GetComponent<EndingCharacter>().amountOf_fireInfo / 2)
			{
				print("worker fire info enough");
				fireEndingObtained = true;
			}
			// add endings
			if (worker_endings.Count == 0)
			{
				if (fireEndingObtained) // ending fire
				{
					worker_endings.Add(worker_fireEnding);
				}
				if (motiveEndingObtained) // ending motive
				{
					foreach (var ending in worker_motiveEndings)
					{
						worker_endings.Add(ending);
					}
				}
				if (!motiveEndingObtained && !fireEndingObtained) // ending nothing
				{
					// add a general ending object that says no info was obtained for this chara
					worker_endings.Add(noInfoText);
				}
			}
		}
		// 3: mayor
		else if (slideNum == 3)
		{
			//print("mayor motive: " + mayor.GetComponent<EndingCharacter>().amountOf_motiveInfo / 2);
			//print("mayor fire: " + mayor.GetComponent<EndingCharacter>().amountOf_fireInfo / 2);
			bool motiveEndingObtained = false;
			bool fireEndingObtained = false;
			// check unlocked amount of motive
			if (cb_mayor_motive.GetComponent<CreateLine>().motive_matchTimes > mayor.GetComponent<EndingCharacter>().amountOf_motiveInfo / 2)
			{
				print("mayor motive info enough");
				motiveEndingObtained = true;
			}
			// check unlocked amount of fire
			if (cb_mayor_fire.GetComponent<CreateLine>().fire_matchTimes > mayor.GetComponent<EndingCharacter>().amountOf_fireInfo / 2)
			{
				print("mayor fire info enough");
				fireEndingObtained = true;
			}
			// add endings
			if (mayor_endings.Count == 0)
			{
				if (fireEndingObtained) // ending fire
				{
					mayor_endings.Add(mayor_fireEnding);
				}
				if (motiveEndingObtained) // ending motive
				{
					foreach (var ending in mayor_motiveEndings)
					{
						mayor_endings.Add(ending);
					}
				}
				if (!motiveEndingObtained && !fireEndingObtained) // ending nothing
				{
					// add a general ending object that says no info was obtained for this chara
					mayor_endings.Add(noInfoText);
				}
			}
		}
		// 4: young
		else if (slideNum == 4)
		{
			//print("young motive: " + young.GetComponent<EndingCharacter>().amountOf_motiveInfo / 2);
			//print("young fire: " + young.GetComponent<EndingCharacter>().amountOf_fireInfo / 2);
			bool motiveEndingObtained = false;
			bool fireEndingObtained = false;
			// check unlocked amount of motive
			if (cb_young_motive.GetComponent<CreateLine>().motive_matchTimes > young.GetComponent<EndingCharacter>().amountOf_motiveInfo / 2)
			{
				print("young motive info enough");
				motiveEndingObtained = true;
			}
			// check unlocked amount of fire
			if (cb_young_fire.GetComponent<CreateLine>().fire_matchTimes > young.GetComponent<EndingCharacter>().amountOf_fireInfo / 2)
			{
				print("young fire info enough");
				fireEndingObtained = true;
			}
			// add endings
			if (young_endings.Count == 0)
			{
				if (fireEndingObtained) // ending fire
				{
					young_endings.Add(young_fireEnding);
				}
				if (motiveEndingObtained) // ending motive
				{
					foreach (var ending in young_motiveEndings)
					{
						young_endings.Add(ending);
					}
				}
				if (!motiveEndingObtained && !fireEndingObtained) // ending nothing
				{
					// add a general ending object that says no info was obtained for this chara
					young_endings.Add(noInfoText);
				}
			}
		}
	}
}
