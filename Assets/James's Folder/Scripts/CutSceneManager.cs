using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour
{
    public static CutSceneManager me;
    public List<GameObject> cutscenes;
    public int cutsceneIndex;
	public GameObject cam;
	public GameObject blackCover;

	private void Awake()
	{
		me = this;
	}

	public void ShowCutscene(int cutsceneIndex)
	{
		cutscenes[cutsceneIndex].SetActive(true);

	}

	public void MoveCamToCutScene()
	{
		cam.transform.position = new Vector3(60, 0, -10);
	}

	public void MoveCamBackToInterview()
	{
		cam.transform.position = new Vector3(0, 0, -10);
	}
}
