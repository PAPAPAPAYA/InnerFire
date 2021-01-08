using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
	public GameObject startScreen;
	public GameObject cam;
	public GameObject tut;
    public void StartGame()
	{
		if (tut.activeSelf)
		{
			tut.SetActive(false);
		}
		cam.transform.position = new Vector3(60, 0, -10);
		Transition.me.FadeOut(0);
		startScreen.SetActive(false);
		SoundManager.me.PlayClick();
		//SoundManager.me.PlayBGM1();
	}
}
