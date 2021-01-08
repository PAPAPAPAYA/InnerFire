using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
	public static Transition me;
	Image thisImage;
	public float alpha;
	public bool fade = false;
	private int cutsceneTo;
	private GameObject cutsceneToEnd;

	private void Awake()
	{
		me = this;
	}

	private void Start()
	{
		thisImage = GetComponent<Image>();
		thisImage.GetComponent<CanvasRenderer>().SetAlpha(0);
		fade = false;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.I))
		{
			FadeIn();
		}
		else if (Input.GetKeyDown(KeyCode.O))
		{
			FadeOut();
		}
		if (Mathf.Approximately(thisImage.GetComponent<CanvasRenderer>().GetAlpha(),1) && fade)
		{
			fade = false;
			
			if (cutsceneToEnd != null)
			{
				// deactivate last scene
				cutsceneToEnd.SetActive(false);
			}
			if (cutsceneTo != 9 &&
				cutsceneTo != 10) // if cut scene to not 9 nor 10
			{
				// activate next scene
				CutSceneManager.me.ShowCutscene(cutsceneTo);
			}
			else if (cutsceneTo == 10)
			{
				// enter day 2
				StateManagerScript.me.state = StateManagerScript.States.dayTwo;
				StateManagerScript.me.EnterDayTwo();
			}
			else
			{
				CutSceneManager.me.MoveCamBackToInterview();
			}
			FadeIn();
		}
	}


	public void FadeIn()
	{
		thisImage.CrossFadeAlpha(0f, 1f, false);
	}

	public void FadeOut(int CutSceneIndex = 9, GameObject sceneToDeactivate = null) // cut scene index 9 meaning don't activate any new cut scene; cut scene index 10 meaning enter day 2
	{
		thisImage.CrossFadeAlpha(1f, 1f, false);
		fade = true;
		cutsceneTo = CutSceneIndex;
		cutsceneToEnd = sceneToDeactivate;
	}
}
