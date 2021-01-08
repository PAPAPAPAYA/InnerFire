using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueBoardButton : MonoBehaviour
{
	public GameObject cutModeButton;
	public GameObject backToInterviewButton;
	public GameObject endGameButton;
	public GameObject cam;
    public void EnterClueBoard()
	{
		SoundManager.me.PlayClick();
		//clueBoard.SetActive(true);
		cam.transform.position = new Vector3(200, 0, -10);
		//clueBoard.transform.position = new Vector3(0, 0, 0);
		cutModeButton.SetActive(true);
		backToInterviewButton.SetActive(true);
		endGameButton.SetActive(true);
		gameObject.SetActive(false);
	}
}
