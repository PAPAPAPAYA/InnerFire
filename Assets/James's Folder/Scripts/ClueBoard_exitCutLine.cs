using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClueBoard_exitCutLine : MonoBehaviour
{
    public GameObject cutLineMode;

    public void ExitCutMode()
	{
		SoundManager.me.PlayClick();
		ClueBoard.me.cutMode = false;
		cutLineMode.SetActive(true);
		gameObject.SetActive(false);
	}
}
