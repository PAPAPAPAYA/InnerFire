using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitClueBoard : MonoBehaviour
{
	public GameObject clueBoard;
	public GameObject enterClueBoardButton;
	public GameObject cutModeButton;
	public GameObject endGameButton;
	public GameObject cam;
    public void BackToInterview()
	{
		SoundManager.me.PlayClick();
		enterClueBoardButton.SetActive(true);
		cutModeButton.SetActive(false);
		cam.transform.position = new Vector3(0, 0, -10);
		//clueBoard.transform.position = new Vector3(30, 0, 0);
		endGameButton.SetActive(false);
		gameObject.SetActive(false);
		//clueBoard.SetActive(false);
	}
}
