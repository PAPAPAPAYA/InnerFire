using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameButton : MonoBehaviour
{
	public GameObject gameOverScreen;
	public GameObject button1;
	public GameObject button2;
	public GameObject button3;
	public GameObject button4;
	public GameObject button5;
	public GameObject cam;
    public void EndGame()
	{
		SoundManager.me.PlayClick();
		SoundManager.me.PlayBGM2();
		//SoundManager.me.PlayBGM1();
		cam.transform.position = new Vector3(-30, 0, -10);
		//gameOverScreen.transform.position = new Vector3(0, 0, 0);
		button1.SetActive(false);
		button2.SetActive(false);
		button3.SetActive(true);
		button4.SetActive(true);
		foreach (var card in ClueBoard.me.clueboard_Cards)
		{
			card.SetActive(false);
		}
		ClueBoard.me.mayor.SetActive(false);
		ClueBoard.me.farmer.SetActive(false);
		ClueBoard.me.young.SetActive(false);
		ClueBoard.me.worker.SetActive(false);
		gameObject.SetActive(false);
	}
}
