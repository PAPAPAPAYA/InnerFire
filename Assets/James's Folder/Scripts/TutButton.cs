using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutButton : MonoBehaviour
{
    public GameObject tut;

    public void ToggleTut()
	{
		SoundManager.me.PlayClick();
		if (tut.activeSelf)
		{
			tut.SetActive(false);
		}
		else
		{
			tut.SetActive(true);
		}
	}
}
